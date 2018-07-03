using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PolRegio.Domain.Models.Components
{
    /// <summary>
    /// Klasa pomocnicza opisująca language url link
    /// </summary>
    public class LangLink: Link
    {
        /// <summary>
        /// Kod języka
        /// </summary>
        public string LanguageCode { get; set; }
    }
}
