using System.Collections.Generic;
using PolRegio.Domain.Models.Components.Account;
using PolRegio.Domain.Models.View.Account;

namespace PolRegio.Domain.Services.Account
{
    public interface IAccountService
    {
        bool IsAuthenticated();

        ProfileViewModel GetProfileView(int currentPageId, ProfileViewModel model);
        ProfileViewModel GetProfileViewById(int userId);

        /// <summary>
        /// metoda do edycji profilu przez użytkownika
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        ProfileResponse SubmitProfile(ProfileViewModel model);

        /// <summary>
        /// metoda do edycji profilu użytkownika z backendu
        /// </summary>
        /// <param name="id"></param>
        /// <param name="user"></param>
        /// <returns></returns>
        ProfileResponse EditProfile(int id, ProfileViewModel user);


        RegisterFormViewModel GetRegisterFormView(int currentPageId, RegisterFormViewModel model);
        RegisterResponse Register(RegisterFormViewModel model);

        LoginFormViewModel GetLoginFormView(int currentPageId, LoginFormViewModel model);
        LoginResponse Login(LoginFormViewModel model);

        bool TryLoginUsingSocialMedia(SocialMediaFormViewModel model);
        SocialMediaFormViewModel GetSocialMediaFormView(int currentPageId, SocialMediaFormViewModel model);
        SocialMediaRegisterResponse RegisterUsingSocialMedia(SocialMediaFormViewModel model);

        void Logout();

        bool DeleteAccount();
        bool DeleteAccount(int id);

        ForgotPassFormViewModel GetForgotPassFormView(int currentPageId, ForgotPassFormViewModel model);
        ForgotPassResponse ForgotPass(ForgotPassFormViewModel model);

        ResetPassFormViewModel GetResetPassFormView(int currentPageId, ResetPassFormViewModel model);
        ResetPassResponse ResetPass(ResetPassFormViewModel model);

        bool ActivateAccount(string token);

        List<UserInfo> GetAllUsers();
        List<UserInfo> SearchUsers(string query);
        string CurrentUserEmail();
    }
}