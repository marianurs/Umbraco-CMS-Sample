using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PolRegio.Domain.Models.Components
{
    /// <summary>
    /// Klasa zawierająca obiekty filtra np z dropdown
    /// </summary>
    public class FilterItem
    {
        /// <summary>
        /// Tekst wyświetlany
        /// </summary>
        public string DisplayText { get; set; }
        /// <summary>
        /// Identyfikator elementu
        /// </summary>
        public int Id { get; set; }
    }
}
