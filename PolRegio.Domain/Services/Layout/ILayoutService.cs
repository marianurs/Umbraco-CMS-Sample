using PolRegio.Domain.Models.View.Layout;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Umbraco.Core.Models;

namespace PolRegio.Domain.Services.Layout
{
    /// <summary>
    /// Interfejs zawierający generowanie headera i footera na stronie
    /// </summary>
    public interface ILayoutService
    {
        /// <summary>
        /// Metoda zwracająca obiekt typu HeaderViewModel
        /// </summary>
        /// <param name="currentUmbracoPageId">id strony umbraco na której się znajdujemy</param>
        /// <returns>obiekt klasy HeaderViewModel</returns>
        HeaderViewModel GetHeaderViewModel(int currentUmbracoPageId);

        /// <summary>
        /// Metoda zwracająca obiekt typu HeaderViewModel
        /// </summary>
        /// <param name="currentUmbracoPageId">id strony umbraco na której się znajdujemy</param>
        /// <returns>obiekt klasy HeaderViewModel</returns>
        FooterViewModel GetFooterViewModel(int currentUmbracoPageId);
    }
}
