using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace PolRegio.Domain.Models.ISS
{
    /// <summary>
    /// Klasa zawierająca customowy model do procesowania
    /// zwrotki z API ISS
    /// </summary>
    public class ISSResponseModel
    {
        /// <summary>
        /// Konstruktor bezparametrowy
        /// </summary>
        public ISSResponseModel()
        {

        }
        /// <summary>
        /// Konstruktor, który przetwarza dane z response
        /// </summary>
        /// <param name="webResponseResult"></param>
        public ISSResponseModel(string webResponseResult)
        {
            if (webResponseResult.Contains("ERROR="))
            {
                var _splitedMessage = webResponseResult.Split(':');
                if (_splitedMessage.Length > 0)
                {
                    ErrorCode = Regex.Replace(_splitedMessage[0], "ERROR=", "").Trim();
                    ErrorMessage = _splitedMessage.Length == 2 ? _splitedMessage[1].Trim() : string.Empty;
                }
                IsError = true;
            }
            ResponseMessage = webResponseResult;
        }
        /// <summary>
        /// Czy response zwrócił error
        /// </summary>
        public bool IsError { get; set; }
        /// <summary>
        /// Kod błędu
        /// </summary>
        public string ErrorCode { get; set; }
        /// <summary>
        /// Wiadomość błędu
        /// </summary>
        public string ErrorMessage { get; set; }
        /// <summary>
        /// Zwrotka z API
        /// </summary>
        public string ResponseMessage { get; set; }
    }
}
