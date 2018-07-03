using PolRegio.Domain.Models.ISS;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PolRegio.Domain.Models.View.SearchTicket
{
    /// <summary>
    /// Klasa zwierająca obiekty występujące na wyszukiwarce biletów
    /// </summary>
    public class SearchTicketFormView
    {
        /// <summary>
        /// Stacja początkowa
        /// </summary>
        [Required]
        [StringLength(100,MinimumLength =3)]
        public string StartStation { get; set; }
        /// <summary>
        /// Stacja końcowa
        /// </summary>
        [Required]
        [StringLength(100, MinimumLength = 3)]
        public string EndStation { get; set; }
        /// <summary>
        /// Data wyjazdu
        /// </summary>
        [Required]
        public DateTime? Date { get; set; }
        /// <summary>
        /// Czas wyjazdu
        /// </summary>
        [Required]
        public DateTime? Time { get; set; }
        /// <summary>
        /// Culture info obecnej strony
        /// </summary>
        public CultureInfo CurrentPageCulture { get; set; }
        /// <summary>
        /// Odpowiedź z API przy odpytaniu się o answer key
        /// </summary>
        public ISSResponseModel AnswerKeyResponse { get; set; }
        /// <summary>
        /// Adres url do przekierowania na stronę przewozów regionalnych
        /// </summary>
        public string PolRegioRedirectUrl { get; set; }
        /// <summary>
        /// Adres url do przekierowania na stronę e-podroznik.pl
        /// </summary>
        public string EPodroznikRedirectUrl { get; set; }
        /// <summary>
        /// Adres url do przekierowania na stronę koleo.pl
        /// </summary>
        public string KoleoRedirectUrl { get; set; }
    }
}
