using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using PolRegio.Domain.Models.Database;
using PolRegio.Domain.Services.Account;
using PolRegio.Domain.Services.Account.Events;
using PolRegio.Domain.Services.Config;
using PolRegio.Domain.Services.SalesManago;
using PolRegio.Services.SalesManago.Model;
using RestSharp;

namespace PolRegio.Services.SalesManago
{
    /// <summary>
    ///     Klasa implementująca interfejs ISalesManagoService
    /// </summary>
    public class SalesManagoService : ISalesManagoService
    {
        public const string BaseUrl = "http://www.salesmanago.pl/api";
        private readonly IConfigService _configService;
        private readonly IUserRepository _userRepository;

        public SalesManagoService(IConfigService configService, IUserRepository userRepository)
        {
            _configService = configService;
            _userRepository = userRepository;
        }

        public void Handle(NewAccountRegisteredEvent @event)
        {
            var userDB = _userRepository.GetUserBy(user => user.Id == @event.UserId);
            userDB.SalesmanagoContactId = Upsert(userDB);
            _userRepository.Update(userDB);
        }

        public void Handle(AccountModifiedEvent @event)
        {
            var userDB = _userRepository.GetUserBy(user => user.Id == @event.UserId);
            userDB.SalesmanagoContactId = Upsert(userDB,@event.UserOldEmail);
            _userRepository.Update(userDB);
        }

        public void Handle(AccountDeletedEvent @event)
        {
            var userDB = _userRepository.GetUserBy(user => user.Id == @event.UserId);
            userDB.SalesmanagoContactId = DeleteContact(userDB.UserEmail)
                ? null
                : userDB.SalesmanagoContactId;
            _userRepository.Update(userDB);
        }

        private Guid? Upsert(UserDB user, string oldEmail = null)
        {
            return user.SalesmanagoContactId != null
                ? UpdateContact(user, oldEmail)
                : AddUserAsAContact(user);
        }

        private bool DeleteContact(string userEmil)
        {
            string ownerMail;
            if (!_configService.Custom.TryGetValue("salesmanago_owner_mail", out ownerMail))
                return false;

            var client = new RestClient(BaseUrl);
            var request = new RestRequest(@"/contact/delete", Method.POST);
            var body = new DeleteContactModel
            {
                owner = ownerMail,
                email = userEmil,
                permanently = false //todo 
            };
            if (!TryInitializeRequestData(body))
                return false;

            request.AddJsonBody(body);

            var response = client.Execute<dynamic>(request);

            if (response.Data["success"] != true)
                return false;

            return true;
        }

        private Guid? AddUserAsAContact(UserDB user)
        {
            string ownerMail;
            if (!_configService.Custom.TryGetValue("salesmanago_owner_mail", out ownerMail)
                || string.IsNullOrWhiteSpace(ownerMail))
                return null;

            var client = new RestClient(BaseUrl);
            var request = new RestRequest(@"/contact/insert", Method.POST);
            var body = new AddContactModel
            {
                owner = ownerMail,
                lang = user.Locale,
                tags = GetUserTags(user),
                contact = new ContactModel
                {
                    name = string.Format("{0} {1}", user.UserName, user.UserSurname),
                    phone = user.UserPhone,
                    email = user.UserEmail,
                    state = "CUSTOMER"
                }
            };

            if (!TryInitializeRequestData(body))
                return null;


            request.AddJsonBody(body);
            var response = client.Execute<dynamic>(request);

            if (response.Data["success"] != true)
                return null;

            return Guid.Parse(response.Data["contactId"]);
        }

        private List<string> GetContactTags(Guid contactId)
        {
            string ownerMail;
            if (!_configService.Custom.TryGetValue("salesmanago_owner_mail", out ownerMail)
                || string.IsNullOrWhiteSpace(ownerMail))
                return new List<string>();

            var client = new RestClient(BaseUrl);
            var request = new RestRequest(@"/contact/listById", Method.POST);
            var body = new GetContactModel
            {
                owner = ownerMail,
                contactId = new List<string> {contactId.ToString()}
            };

            if (!TryInitializeRequestData(body))
                return new List<string>();
            ;


            request.AddJsonBody(body);
            var response = client.Execute<dynamic>(request);

            if (response.Data["success"] != true)
                return null;


            return ((IEnumerable<dynamic>) response.Data["contacts"][0]["contactTags"])
                .Select(tag => tag["tagName"])
                .Cast<string>()
                .ToList();
        }

        private List<string> GetUserTags(UserDB user)
        {
            var tags = _userRepository.GetAgreedAgreementsForUser(user)
                .Select(a => a.ShortName)
                .Union(_userRepository
                    .GetRegionsForUser(user)
                    .Select(r => r.Name))
                .ToList();

            return tags;
        }

        private List<string> CalculateTagsToDelete(UserDB userDB, List<string> newTags)
        {
            var actualTags = GetContactTags(userDB.SalesmanagoContactId.Value);
            return actualTags
                .Where(at => !newTags.Any(nt => string.Equals(at, nt, StringComparison.InvariantCultureIgnoreCase)))
                .ToList();
        }

        private Guid? UpdateContact(UserDB user, string oldEmail = null)
        {
            string ownerMail;
            if (!_configService.Custom.TryGetValue("salesmanago_owner_mail", out ownerMail)
                || string.IsNullOrWhiteSpace(ownerMail))
                return user.SalesmanagoContactId;

            var client = new RestClient(BaseUrl);
            var request = new RestRequest(@"/contact/upsert", Method.POST);
            var body = new UpsertContactModel
            {
                async = false,
                owner = ownerMail,
                lang = user.Locale,
                contact = new ContactModel
                {
                    name = string.Format("{0} {1}", user.UserName, user.UserSurname),
                    phone = user.UserPhone,
                    email = oldEmail,
                    state = "CUSTOMER"
                },
                newEmail = oldEmail != user.UserEmail ? user.UserEmail : "",
            };

            body.tags = GetUserTags(user);
            body.removeTags = CalculateTagsToDelete(user, body.tags);

            if (!TryInitializeRequestData(body))
                return user.SalesmanagoContactId;


            request.AddJsonBody(body);
            var response = client.Execute<dynamic>(request);

            return response.Data["success"] != true
                ? Guid.Parse(response.Data["contactId"])
                : user.SalesmanagoContactId;
        }

        private static string GetShaSignature(string apiKey, string clientId, string apiSecret)
        {
            using (var sha1 = new SHA1Managed())
            {
                var hash = sha1.ComputeHash(Encoding.UTF8.GetBytes(apiKey + clientId + apiSecret));
                return hash.Select(b => b.ToString("x2")).Aggregate((s1, s2) => s1 + s2);
            }
        }

        private static int GetTimestamp()
        {
            return (int) DateTime.UtcNow.Subtract(new DateTime(1970, 1, 1)).TotalSeconds;
        }

        private bool TryInitializeRequestData(BaseSalesManagoRequestModel model)
        {
            string clientId;
            string apiKey;
            string apiSecret;

            _configService.Custom.TryGetValue("salesmanago_client_id", out clientId);
            _configService.Custom.TryGetValue("salesmanago_api_key", out apiKey);
            _configService.Custom.TryGetValue("salesmanago_api_secret", out apiSecret);

            if (string.IsNullOrWhiteSpace(clientId)
                || string.IsNullOrWhiteSpace(apiSecret)
                || string.IsNullOrWhiteSpace(apiSecret))
                return false;

            model.clientId = clientId;
            model.apiKey = apiKey;
            model.sha = GetShaSignature(apiKey, clientId, apiSecret);
            model.requestTime = GetTimestamp();
            return true;
        }
    }
}