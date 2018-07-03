using PolRegio.Domain.Models.Components.Contact;
using PolRegio.Domain.Models.View.AccordionPage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PolRegio.Domain.Services.Accordions
{
    /// <summary>
    /// Interfejs zawierający metody do obsługi akordionów
    /// </summary>
    public interface IAccordionService
    {
        /// <summary>
        /// Metoda zwracająca listę elementów z akordiona na stronie
        /// </summary>
        /// <param name="currentUmbracoPageId">id strony umbraco na której się znajdujemy</param>
        /// <returns>Model zawierający listę elementów z akordiona na stronie</returns>
        IEnumerable<AccordionViewModel> GetAccordionElementsModel(int currentUmbracoPageId);
        /// <summary>
        /// Metoda zwracająca listę elementów z akordiona na stronie oddziałów
        /// </summary>
        /// <param name="currentUmbracoPageId">id strony umbraco na której się znajdujemy</param>
        /// <returns>Model zawierający listę elementów z akordiona na stronie oddziałów</returns>
        IEnumerable<RegionContactBox> GetAccordionElementsForOfficeModel(int currentUmbracoPageId);
    }
}
