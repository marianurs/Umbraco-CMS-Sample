using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PolRegio.Domain.Models.Components
{
    /// <summary>
    /// Klasa zawierająca obiekty filtra np na liście checkbox
    /// </summary>
    public class CheckBoxFilterItem : FilterItem
    {
        /// <summary>
        /// Flaga zwracająca czy element jest zaznaczony
        /// </summary>
        public bool IsChecked { get; set; }
    }
}
