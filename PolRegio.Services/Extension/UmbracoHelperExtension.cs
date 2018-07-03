using PolRegio.Domain.Services.Config;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using Umbraco.Web;

namespace PolRegio.Services.Extension
{
    /// <summary>
    /// Klasa rozszerzająća Umbraco helper aby pobierać adresy obiektów media z CDN
    /// </summary>
    public static class UmbracoHelperExtension
    {
        /// <summary>
        /// Obiekt IConfigService z  Dependency Injection
        /// </summary>
        private static IConfigService ConfigService
        {
            // Zwraca obiekt z Dependency Injection
            get { return DependencyResolver.Current.GetService<IConfigService>(); }
        }
        /// <summary>
        /// Metoda zwracająca adres url do media z CDN
        /// </summary>
        /// <param name="helper">Umbraco Helper</param>
        /// <param name="id">id obiektu z media</param>
        /// <returns>adres do media z CDN</returns>
        public static string GetImageCDNUrlById(this UmbracoHelper helper, string id)
        {
            var _useCDNKey = ConfigService.Custom["useCDN"];
            bool _useCDN = false;
            var _resultUrl = string.Empty;
            if (!string.IsNullOrEmpty(id))
            {
                if (bool.TryParse(_useCDNKey, out _useCDN) && _useCDN)
                {
                    var _cloudfrontUrl = ConfigService.Custom["cdnDomain"];
                    var _media = helper.TypedMedia(id);
                    if (_media != null)
                    {
                        _resultUrl = _cloudfrontUrl + _media.Url;
                    }
                }
                else
                {
                    var _media = helper.TypedMedia(id);
                    _resultUrl = _media == null ? null : _media.Url;
                }
            }
            return _resultUrl;
        }
        /// <summary>
        /// Metoda zwracająca kontent z CDN 
        /// </summary>
        /// <param name="helper">Html Helper</param>
        /// <param name="url">content url</param>
        /// <returns>Zwraca url do obiektu z CDN</returns>
        public static string GetContentFromCDN(this HtmlHelper helper, string url)
        {
            var _useLocalFilesKey = ConfigService.Custom["use_local_files"];
            var _useCDNKey = ConfigService.Custom["useCDN"];
            bool _useCDN = false;
            bool _useLocalFiles = false;
            bool.TryParse(_useLocalFilesKey, out _useLocalFiles);

            if (bool.TryParse(_useCDNKey, out _useCDN) && _useCDN && !_useLocalFiles)
            {
                var _cdnUrl = ConfigService.Custom["cdnDomain"];
                url = _cdnUrl + url;
            }

            return url;
        }
    }
}
