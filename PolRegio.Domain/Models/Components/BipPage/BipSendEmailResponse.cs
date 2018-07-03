using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PolRegio.Domain.Models.Components.BipPage
{
    /// <summary>
    /// Klasa zwierająca zwrotkę z wysyłki emaila
    /// </summary>
    public class BipSendEmailResponse
    {
        /// <summary>
        /// Czy ma być wyświetlony komunikat
        /// </summary>
        public bool Display { get; set; }
        /// <summary>
        /// Czy wysyłka zwróciła error
        /// </summary>
        public bool IsError { get; set; }
        /// <summary>
        /// Wiadomość błędu
        /// </summary>
        public string Message { get; set; }
    }
}
