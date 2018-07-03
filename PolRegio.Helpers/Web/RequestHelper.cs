using System.IO;
using System.Net;
using System.Text;

namespace PolRegio.Helpers.Web
{
    /// <summary>
    /// Klasa zawierająca metody pomocnicze przy requestach Url
    /// </summary>
    public class RequestHelper
    {
        /// <summary>
        /// Metoda zwracająca string z POST requesta
        /// </summary>
        /// <param name="url">url do wysłania</param>
        /// <param name="postData">data post</param>
        /// <returns></returns>
        public static string MakePOSTWebRequest(string url, string postData)
        {
            var _request = (HttpWebRequest)WebRequest.Create(url);
            var _data = Encoding.UTF8.GetBytes(postData);
            try
            {
                _request.Method = "POST";
                _request.ContentType = "application/x-www-form-urlencoded";
                _request.ContentLength = _data.Length;

                using (var stream = _request.GetRequestStream())
                {
                    stream.Write(_data, 0, _data.Length);
                }

                var response = (HttpWebResponse)_request.GetResponse();

                return new StreamReader(response.GetResponseStream()).ReadToEnd();
            }
            catch
            {
                return string.Empty;
            }
        }
    }
}