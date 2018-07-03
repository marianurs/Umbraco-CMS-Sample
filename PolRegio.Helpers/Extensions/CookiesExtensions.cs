using System;
using System.Web;

namespace PolRegio.Helpers.Extensions
{
    public static class CookiesExtensions
    {
        public static void CreateCookie(string name, string value)
        {
            HttpContext.Current.Response.SetCookie(new HttpCookie(name) { Value = value });
        }

        public static string GetCookieValue(string name)
        {
            var _cookie = HttpContext.Current.Request.Cookies[name];
            return _cookie == null ? null : _cookie.Value;
        }

        public static void DeleteCookie(string name)
        {
            var cookieToDelete = GetCookie(name);

            HttpContext.Current.Response.Cookies.Remove(name);
            cookieToDelete.Expires = DateTime.UtcNow.AddDays(-10);
            cookieToDelete.Value = null;

            HttpContext.Current.Response.SetCookie(cookieToDelete);
        }

        public static HttpCookie GetCookie(string name)
        {
            return HttpContext.Current.Request.Cookies[name];
        }
    }
}