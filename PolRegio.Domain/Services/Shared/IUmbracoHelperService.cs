using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Umbraco.Core.Models;

namespace PolRegio.Domain.Services.Shared
{
    /// <summary>
    /// Interfejs zawierający metody pmocnicze do Umbraco
    /// </summary>
    public interface IUmbracoHelperService
    {
        /// <summary>
        /// Metoda zwracająca CUltureInfo dla danego noda
        /// </summary>
        /// <param name="content">Obiekt umbraco typu IPublishedContent</param>
        /// <returns></returns>
        CultureInfo GetCulture(IPublishedContent content);
    }
}
