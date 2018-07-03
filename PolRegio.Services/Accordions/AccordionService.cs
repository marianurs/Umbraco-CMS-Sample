using PolRegio.Domain.Services.Accordions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PolRegio.Domain.Models.View.AccordionPage;
using Umbraco.Web;
using PolRegio.Domain.Models.UmbracoCreate;
using PolRegio.Domain.Models.Components.Contact;

namespace PolRegio.Services.Accordions
{
    /// <summary>
    /// Klasa implementująca interfejs IAccordionService
    /// </summary>
    public class AccordionService : IAccordionService
    {
        /// <summary>
        /// Obiekt typu UmbracoHelper
        /// </summary>
        private UmbracoHelper _umbracoHelper;

        /// <summary>
        /// Konstruktor klasy
        /// </summary>
        public AccordionService()
        {
            _umbracoHelper = new UmbracoHelper(UmbracoContext.Current);
        }
        /// <summary>
        /// Metoda zwracająca listę elementów z akordiona na stronie oddziałów
        /// </summary>
        /// <param name="currentUmbracoPageId">id strony umbraco na której się znajdujemy</param>
        /// <returns>Model zawierający listę elementów z akordiona na stronie oddziałów</returns>
        public IEnumerable<RegionContactBox> GetAccordionElementsForOfficeModel(int currentUmbracoPageId)
        {
            var _currentAccordionPage = _umbracoHelper.TypedContent(currentUmbracoPageId);

            var _departmentNode = new OfficeAccordion(_currentAccordionPage);

            if (_departmentNode != null && _departmentNode.AddOffice != null)
            {
                return _departmentNode.AddOffice.Fieldsets.Where(x => x != null && x.Properties.Any() && !x.Disabled).Select(q => new RegionContactBox(q));
            }

            return null;
        }

        /// <summary>
        /// Metoda zwracająca listę elementów z akordiona na stronie
        /// </summary>
        /// <param name="currentUmbracoPageId">id strony umbraco na której się znajdujemy</param>
        /// <returns>Model zawierający listę elementów z akordiona na stronie</returns>
        public IEnumerable<AccordionViewModel> GetAccordionElementsModel(int currentUmbracoPageId)
        {
            var _currentAccordionPage = _umbracoHelper.TypedContent(currentUmbracoPageId);

            var _accordionWithAttachments = new AccordionWithAttachments(_currentAccordionPage);

            if (_accordionWithAttachments.AccordionElements != null)
            {
                return _accordionWithAttachments.AccordionElements.Fieldsets.Where(x => x != null && x.Properties.Any() && !x.Disabled).Select(q => new AccordionViewModel(q));
            }
            return null;
        }
    }
}
