using log4net;
using PolRegio.Domain.Models.Components.Account;
using PolRegio.Domain.Models.Database;
using PolRegio.Domain.Models.View.Account;
using PolRegio.Domain.Services.Account;
using PolRegio.Domain.Services.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using PolRegio.Domain.Services.Account.Events;
using PolRegio.Domain.Services.EventBus;
using Umbraco.Core.Services;
using Umbraco.Web;

namespace PolRegio.Services.Account
{
    public class AccountService : IAccountService
    {
        private static readonly ILog Log =
            LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        private readonly UmbracoHelper _umbracoHelper;
        private readonly ILocalizationService _localizationService;
        private readonly IMappingService _mappingService;
        private readonly IHashingService _hashingService;
        private readonly IEmailService _emailService;
        private readonly IUserRepository _userRepository;
        private readonly IEventBus _eventBus;


        public AccountService(
            IMappingService mappingService,
            IHashingService hashingService,
            IEmailService emailService,
            IUserRepository userRepository, IEventBus eventBus)
        {
            _umbracoHelper = new UmbracoHelper(UmbracoContext.Current);
            _localizationService = UmbracoContext.Current.Application.Services.LocalizationService;
            _mappingService = mappingService;
            _hashingService = hashingService;
            _emailService = emailService;
            _userRepository = userRepository;
            _eventBus = eventBus;
        }

        public bool IsAuthenticated()
        {
            return HttpContext.Current.Request.IsAuthenticated
                && HttpContext.Current.User.Identity.AuthenticationType == "Forms";
        }

        public string CurrentUserEmail()
        {
            return HttpContext.Current.User.Identity.Name;
        }

        public ProfileViewModel GetProfileView(
            int currentPageId,
            ProfileViewModel model)
        {
            var currentUserEmail = CurrentUserEmail();
            var user = _userRepository.GetUserBy(x => x.UserEmail == currentUserEmail);
            model = _mappingService.Map<UserDB, ProfileViewModel>(user);

            var regions = _userRepository.GetAllRegions();
            model.SelectedRegions = _userRepository.GetRegionsForUser(user).Select(x => x.Id.ToString()).ToArray();
            model.RegionSelectList = new MultiSelectList(regions, "Key", "Value", regions.Take(1));

            model.Agreements = _userRepository.GetAgreementsViewsForUser(user);
            model.CurrentUmbracoPageId = currentPageId;
            model.CurrentPageCulture = Thread.CurrentThread.CurrentCulture;

            return model;
        }

        public ProfileViewModel GetProfileViewById(int userId)
        {
            var user = _userRepository.GetUserBy(x => x.Id == userId);
            var model = _mappingService.Map<UserDB, ProfileViewModel>(user);

            var regions = _userRepository.GetAllRegions();
            model.SelectedRegions = _userRepository.GetRegionsForUser(user).Select(x => x.Id.ToString()).ToArray();
            model.RegionSelectList = new MultiSelectList(regions, "Key", "Value", regions.Take(1));
            model.Agreements = _userRepository.GetAgreementsViewsForUser(user);

            return model;
        }

        public ProfileResponse SubmitProfile(ProfileViewModel model)
        {
            try
            {
                var currentUserEmail = CurrentUserEmail();
                var errors = ValidateUser(currentUserEmail, model.SelectedRegions, model.Agreements, isNew: false);
                if (errors.Any())
                    return new ProfileResponse
                    {
                        IsError = true,
                        ShouldDisplayMessage = true,
                        Message = _umbracoHelper.GetDictionaryValue("Profile.Submit.Failure"),
                        ValidationErrors = errors
                    };

                var user = _userRepository.GetUserBy(x => x.UserEmail == currentUserEmail);
                _mappingService.Map(model, user);

                if (!string.IsNullOrWhiteSpace(model.UserPassword))
                {
                    user.UserPassword = _hashingService.Hash(model.UserPassword);
                }

                _userRepository.Update(user, model.SelectedRegions, model.Agreements);
                _eventBus.Send(new AccountModifiedEvent {UserId = user.Id, UserOldEmail= currentUserEmail });

                return new ProfileResponse
                {
                    IsError = false,
                    Message = _umbracoHelper.GetDictionaryValue("Profile.Submit.Success"),
                    ShouldDisplayMessage = true
                };
            }
            catch (Exception exception)
            {
                Log.Error("Error during submitting profile", exception);

                return new ProfileResponse
                {
                    IsError = true,
                    Message = exception.Message,
                    ShouldDisplayMessage = true
                };
            }
        }

        public ProfileResponse EditProfile(int id, ProfileViewModel model)
        {
            try
            {
                var errors = ValidateUser(model.UserEmail, model.SelectedRegions, model.Agreements, isNew: false);
                if (errors.Any())
                    return new ProfileResponse
                    {
                        IsError = true,
                        ShouldDisplayMessage = true,
                        Message = _umbracoHelper.GetDictionaryValue("Profile.Submit.Failure"),
                        ValidationErrors = errors
                    };

                var user = _userRepository.GetUserBy(x => x.Id == id);
                var oldMail = user.UserEmail;
                _mappingService.Map(model, user);

                _userRepository.Update(user, model.SelectedRegions, model.Agreements);
                _eventBus.Send(new AccountModifiedEvent {UserId = user.Id, UserOldEmail = oldMail});

                return new ProfileResponse
                {
                    IsError = false,
                    Message = _umbracoHelper.GetDictionaryValue("Profile.Submit.Success"),
                    ShouldDisplayMessage = true
                };
            }
            catch (Exception exception)
            {
                Log.Error("Error during submitting profile", exception);

                return new ProfileResponse
                {
                    IsError = true,
                    Message = exception.Message,
                    ShouldDisplayMessage = true
                };
            }
        }

        public RegisterFormViewModel GetRegisterFormView(
            int currentPageId,
            RegisterFormViewModel model)
        {
            var regions = _userRepository.GetAllRegions();
            model.RegionSelectList = new MultiSelectList(regions, "Key", "Value", regions.Take(1));
            model.Agreements = _userRepository.GetAllActiveAgreements();
            model.CurrentUmbracoPageId = currentPageId;
            model.CurrentPageCulture = Thread.CurrentThread.CurrentCulture;

            return model;
        }

        public RegisterResponse Register(RegisterFormViewModel model)
        {
            try
            {
                var errors = ValidateUser(model.UserEmail, model.SelectedRegions, model.Agreements, isNew: true);
                if (errors.Any())
                    return new RegisterResponse
                    {
                        IsError = true,
                        ShouldDisplayMessage = true,
                        Message = _umbracoHelper.GetDictionaryValue("Register.SendEmail.Failure"),
                        ValidationErrors = errors
                    };

                var user = _mappingService.Map<RegisterFormViewModel, UserDB>(model);
                user.UserPassword = _hashingService.Hash(model.UserPassword);
                user.ActivationToken = GenerateActivationToken();
                user.Locale = model.CurrentPageCulture.Name;

                if (!errors.Any())
                {
                    _userRepository.Insert(user, model.SelectedRegions, model.Agreements);
                    _emailService.SendRegisterEmail(user);
                }

                return new RegisterResponse
                {
                    IsError = false,
                    Message = _umbracoHelper.GetDictionaryValue("Register.SendEmail.Success"),
                    ShouldDisplayMessage = true,
                    ValidationErrors = errors
                };
            }
            catch (Exception exception)
            {
                Log.Error("Error during registering new account", exception);

                return new RegisterResponse
                {
                    IsError = true,
                    Message = exception.Message,
                    ShouldDisplayMessage = true,
                    ValidationErrors = new List<KeyValuePair<string, string>>()
                };
            }
        }

        public LoginFormViewModel GetLoginFormView(int currentPageId, LoginFormViewModel model)
        {
            model.CurrentUmbracoPageId = currentPageId;
            model.CurrentPageCulture = Thread.CurrentThread.CurrentCulture;

            if (model.Response != null && !string.IsNullOrEmpty(model.Response.Message)) model.Response.Message = _umbracoHelper.GetDictionaryValue(model.Response.Message);

            return model;
        }

        public LoginResponse Login(LoginFormViewModel model)
        {
            try
            {
                var user = _userRepository.GetUserBy(x => x.UserEmail == model.UserEmail && x.IsActive == true);
                if (user != null && _hashingService.Compare(user.UserPassword, model.UserPassword))
                {
                    FormsAuthentication.SetAuthCookie(user.UserEmail, createPersistentCookie: false);

                    return new LoginResponse()
                    {
                        IsError = false,
                        Message = "Login.Submit.Success",
                    };
                }
            }
            catch (Exception exception)
            {
                Log.Error("Error during login", exception);
            }
            return new LoginResponse()
            {
                IsError = true,
                Message = "Login.Submit.Failure",
            };
        }

        public bool TryLoginUsingSocialMedia(SocialMediaFormViewModel model)
        {
            try
            {
                var user = _userRepository.GetUserBy(x => x.UserEmail == model.UserEmail);
                if (user == null)
                    return false;

                user.IsActive = true;
                user.ActivationToken = string.Empty;

                _userRepository.Update(user);

                FormsAuthentication.SetAuthCookie(user.UserEmail, createPersistentCookie: false);

                return true;
            }
            catch (Exception exception)
            {
                Log.Error("Error during social media login", exception);
                return false;
            }
        }

        public SocialMediaFormViewModel GetSocialMediaFormView(int currentPageId, SocialMediaFormViewModel model)
        {
            var regions = _userRepository.GetAllRegions();
            model.Agreements = _userRepository.GetAllActiveAgreements();
            model.RegionSelectList = new MultiSelectList(regions, "Key", "Value", regions.Take(1));
            model.CurrentUmbracoPageId = currentPageId;
            model.CurrentPageCulture = Thread.CurrentThread.CurrentCulture;

            return model;
        }

        public SocialMediaRegisterResponse RegisterUsingSocialMedia(SocialMediaFormViewModel model)
        {
            try
            {
                var errors = ValidateUser(model.UserEmail, model.SelectedRegions, model.Agreements, isNew: true);

                if (errors.Any())
                {
                    return new SocialMediaRegisterResponse
                    {
                        IsError = true,
                        Message = "SocialMedia.Register.Failure",
                        ValidationErrors = errors
                    };
                }
                
                var user = _mappingService.Map<SocialMediaFormViewModel, UserDB>(model);

                if (!string.IsNullOrWhiteSpace(model.UserPassword))
                    user.UserPassword = _hashingService.Hash(model.UserPassword);

                user.Locale = model.CurrentPageCulture.Name;

                _userRepository.Insert(user, model.SelectedRegions, model.Agreements);
                _eventBus.Send(new NewAccountRegisteredEvent { UserId = user.Id });


                FormsAuthentication.SetAuthCookie(user.UserEmail, createPersistentCookie: false);

                return new SocialMediaRegisterResponse
                {
                    IsError = false,
                    Message = "SocialMedia.Register.Success"
                };
            }
            catch (Exception exception)
            {
                Log.Error("Error during registering new account using social media", exception);

                return new SocialMediaRegisterResponse
                {
                    IsError = true,
                    Message = exception.Message
                };
            }
        }

        public void Logout()
        {
            FormsAuthentication.SignOut();
        }

        public bool DeleteAccount()
        {
            try
            {
                var currentUserEmail = CurrentUserEmail();
                var user = _userRepository.GetUserBy(x => x.UserEmail == currentUserEmail);

                FormsAuthentication.SignOut();

                _eventBus.Send(new AccountDeletedEvent {UserId = user.Id});
                _userRepository.Delete(user);

                return true;
            }
            catch (Exception exception)
            {
                Log.Error("Error during deleting account", exception);
                return false;
            }
        }

        public bool DeleteAccount(int id)
        {
            try
            {
                var user = _userRepository.GetUserBy(x => x.Id == id);

                _eventBus.Send(new AccountDeletedEvent {UserId = user.Id});
                _userRepository.Delete(user);

                return true;
            }
            catch (Exception exception)
            {
                Log.Error("Error during deleting account", exception);
                return false;
            }
        }

        public ForgotPassFormViewModel GetForgotPassFormView(int currentPageId, ForgotPassFormViewModel model)
        {
            model.CurrentUmbracoPageId = currentPageId;
            model.CurrentPageCulture = Thread.CurrentThread.CurrentCulture;

            return model;
        }

        public ForgotPassResponse ForgotPass(ForgotPassFormViewModel model)
        {
            try
            {
                var user = _userRepository.GetUserBy(x => x.UserEmail == model.Email);
                if (user != null)
                {
                    user.IsActive = false;
                    user.ActivationToken = GenerateActivationToken();
                    _emailService.SendForgotPassEmail(user);

                    _userRepository.Update(user);
                }
                return new ForgotPassResponse()
                {
                    IsError = false,
                    Message = "ForgotPass.Submit.Success",
                };
            }
            catch (Exception exception)
            {
                Log.Error("Error during sending forgot pass email", exception);

                return new ForgotPassResponse()
                {
                    IsError = true,
                    Message = "ForgotPass.Submit.Failure",
                };
            }
        }

        public ResetPassFormViewModel GetResetPassFormView(int currentPageId, ResetPassFormViewModel model)
        {
            model.TokenExpired = _userRepository.GetUserBy(x => x.ActivationToken == model.Token) == null;
            model.CurrentUmbracoPageId = currentPageId;
            model.CurrentPageCulture = Thread.CurrentThread.CurrentCulture;

            return model;
        }

        public ResetPassResponse ResetPass(ResetPassFormViewModel model)
        {
            try
            {
                var user = _userRepository.GetUserBy(x => x.ActivationToken == model.Token);
                if (user != null)
                {
                    user.UserPassword = _hashingService.Hash(model.Password);
                    user.IsActive = true;
                    user.ActivationToken = string.Empty;

                    _userRepository.Update(user);

                    return new ResetPassResponse()
                    {
                        IsError = false,
                        Message = "ResetPass.Submit.Success",
                    };
                }
            }
            catch (Exception exception)
            {
                Log.Error("Error during resetting password", exception);
            }

            return new ResetPassResponse()
            {
                IsError = true,
                Message = "ResetPass.Submit.Failure",
            };
        }

        public bool ActivateAccount(string token)
        {
            try
            {
                var user = _userRepository.GetUserBy(x => x.ActivationToken == token && x.IsActive == false);
                if (user == null)
                    return false;

                user.ActivationToken = string.Empty;
                user.IsActive = true;

                _userRepository.Update(user);
                _eventBus.Send(new NewAccountRegisteredEvent { UserId = user.Id });

                FormsAuthentication.SetAuthCookie(user.UserEmail, createPersistentCookie: false);

                return true;
            }
            catch (Exception exception)
            {
                Log.Error("Error during account activation", exception);
                return true;
            }
        }

        public List<UserInfo> GetAllUsers()
        {
            return _userRepository
                .GetAllUsers()
                .Select(db => _mappingService.Map<UserDB, UserInfo>(db))
                .ToList();
        }

        public List<UserInfo> SearchUsers(string query)
        {
            return _userRepository
                .GetUsersBy(u => u.UserName.Contains(query) || u.UserSurname.Contains(query) || u.UserEmail.Contains(query))
                .Select(db => _mappingService.Map<UserDB, UserInfo>(db))
                .ToList();
        }

        private List<KeyValuePair<string, string>> ValidateUser(
            string email,
            string[] selectedRegions,
            List<AgreementViewModel> agreements,
            bool isNew)
        {
            var errors = new List<KeyValuePair<string, string>>();

            if (isNew && _userRepository.DoesUserExist(email))
                errors.Add(new KeyValuePair<string, string>(
                    "UserEmail",
                    _umbracoHelper.GetDictionaryValue("Register.Placeholder.UserEmail.Error")));

            if (selectedRegions == null || selectedRegions.Length < 1)
                errors.Add(new KeyValuePair<string, string>(
                    "SelectedRegions",
                    _umbracoHelper.GetDictionaryValue("Register.PlaceHolder.Region.Error")));

            var agreementErrors = agreements
                .Select((agreement, index) => new { Agreement = agreement, Index = index })
                .Where(x => x.Agreement.Required && !x.Agreement.Value)
                .Select(x => new KeyValuePair<string, string>(
                    "Agreements[" + x.Index + "].Value",
                    _umbracoHelper.GetDictionaryValue("Register.Placeholder.Agreement.Error")));

            errors.AddRange(agreementErrors);

            return errors;
        }

        private string GenerateActivationToken()
        {
            return Guid.NewGuid().ToString();
        }
    }
}
