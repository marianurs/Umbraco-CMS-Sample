using System;
using System.Web;

namespace PolRegio.Helpers.Constants
{
    public class AccountRedirectUrls
    {
        public static string Home(string lang)
        {
            return ToAbsolute(string.Format("/{0}/", lang));
        }

        public static string LoginPage(string lang)
        {
            return ToAbsolute(string.Format("/{0}/konto/logowanie/", lang));
        }

        public static string ActivateAccountPage(string lang, string token)
        {
            return ToAbsolute(string.Format("/{0}/konto/aktywacja/{1}/", lang, token));
        }

        public static string ResetPasswordPage(string lang, string token)
        {
            return ToAbsolute(string.Format("/{0}/konto/reset-hasla/?token={1}", lang, token));
        }

        public static string ThankYouPage(string lang)
        {
            return ToAbsolute(string.Format("/{0}/konto/dziękujemy", lang));
        }

        public static string Profile(string lang)
        {
            return ToAbsolute(string.Format("/{0}/konto/profil", lang));
        }

        public static string SocialMediaRegistration(string lang)
        {
            return ToAbsolute(string.Format("/{0}/konto/rejestracja/media-spolecznosciowe", lang));
        }

        private static string ToAbsolute(string path)
        {
            var absolutePath = VirtualPathUtility.ToAbsolute(path);
            var uri = new Uri(HttpContext.Current.Request.Url, absolutePath);
            return uri.AbsoluteUri;
        }
    }
}
