using PolRegio.Domain.Models.Components.Ticket;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PolRegio.Domain.Models.View.Tickets
{
    /// <summary>
    /// Klasa zawierająca obiekty wyświetlane na stronie ze wszystkimi kasami biletowymi
    /// </summary>
    public class TicketOfficesPageViewModel
    {
        /// <summary>
        /// Nazwa szukanej kasy
        /// </summary>
        public string OfficeName { get; set; }
        /// <summary>
        /// Lista obiektów do wyświetlenia jako kasy biletowe
        /// </summary>
        public IEnumerable<TicketOfficeBoxModel> OfficesList { get; set; }

        /// <summary>
        /// Liczba wszystkich dostępnych kas do wyświetlenia
        /// </summary>
        public int AllNewsCount { get; set; }
        /// <summary>
        /// Liczba wyświetlanych elementów po LoadMore lub po zmianie filtrów
        /// </summary>
        public int DisplayCount { get; set; }
        /// <summary>
        /// Aktualne id strony
        /// </summary>
        public int CurrentUmbracoPageId { get; set; }
        /// <summary>
        /// Culture info obecnej strony
        /// </summary>
        public CultureInfo CurrentPageCulture { get; set; }
    }
}
