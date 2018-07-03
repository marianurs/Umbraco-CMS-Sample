using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PolRegio.Domain.Models.Components
{
    /// <summary>
    /// Klasa zawirająca elementy zwracane przez SqlDropDownPicker
    /// </summary>
    public class NuPickersSqlDropDownPicker
    {
        /// <summary>
        /// NuPicker dropdown key
        /// </summary>
        public string Key { get; set; }
        /// <summary>
        /// NuPicker dropdown label
        /// </summary>
        public string Label { get; set; }
    }
}
