using Umbraco.Web;

namespace PolRegio.Web.Helpers
{
    public static class MediaUrlHelper
    {
        public static string GetMediaUrl(string id,int width, int height)
        {
            
            var umbracoHelper = new UmbracoHelper(UmbracoContext.Current);
            var url = umbracoHelper.TypedMedia(id).Url;
            return string.Format("{0}?width={1}&height={2}&mode=crop", url, width, height);
        }

        public static string GetMediaUrl(string id)
        {
            var umbracoHelper = new UmbracoHelper(UmbracoContext.Current);
            return umbracoHelper.TypedMedia(id).Url;
        }
    }
}
