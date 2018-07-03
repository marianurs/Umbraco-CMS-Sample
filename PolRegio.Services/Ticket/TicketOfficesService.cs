using PolRegio.Domain.Services.Tickets;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PolRegio.Domain.Models.View.Tickets;
using PolRegio.Domain.Models.Components.Ticket;
using Umbraco.Web;
using System.Threading;
using PolRegio.Domain.Models.UmbracoCreate;

namespace PolRegio.Services.Ticket
{
    /// <summary>
    /// Klasa implementująca interface
    /// </summary>
    public class TicketOfficesService : ITicketOfficesService
    {
        /// <summary>
        /// Obiekt typu UmbracoHelper
        /// </summary>
        private UmbracoHelper _umbracoHelper;
        /// <summary>
        /// Konstruktor klasy
        /// </summary>
        public TicketOfficesService()
        {
            _umbracoHelper = new UmbracoHelper(UmbracoContext.Current);
        }
        /// <summary>
        /// Metorda zwracająca elementy wyświetlane na stronie wszytskich kas biletowych
        /// </summary>
        /// <param name="currentUmbracoPageId">id strony umbraco na której się znajdujemy</param>
        /// <returns>Model zawierający elementy strony ze wszytskimi kasami biletowymi</returns>
        public TicketOfficesPageViewModel GetTicketOfficesModel(int currentUmbracoPageId)
        {
            var _model = new TicketOfficesPageViewModel();
            _model.CurrentUmbracoPageId = currentUmbracoPageId;
            _model.CurrentPageCulture = Thread.CurrentThread.CurrentCulture;

            return GetTicketOfficesModel(_model);
        }
        /// <summary>
        /// Metorda zwracająca elementy wyświetlane na stronie wszytskich kas biletowych
        /// </summary>
        /// <param name="model">model TicketOfficesPageViewModel</param>
        /// <returns>Model zawierający elementy strony ze wszytskimi kasami biletowymi</returns>
        public TicketOfficesPageViewModel GetTicketOfficesModel(TicketOfficesPageViewModel model)
        {
            var _ticketOfficesNode = _umbracoHelper.TypedContent(model.CurrentUmbracoPageId);
            var _officesList = _ticketOfficesNode.Children.Where("Visible").Select(q => new TicketOffice(q));

            model.DisplayCount = new ContractNotices(_ticketOfficesNode).DisplayItemCount;

            if (!string.IsNullOrEmpty(model.OfficeName))
            {
                _officesList = _officesList.Where(q => q.Name.Trim().ToLower().Contains(model.OfficeName.Trim().ToLower()));
            }
            
            model.AllNewsCount = _officesList.Count();
            //_officesList = _officesList.OrderBy(x => x.OfficeLocalization.TrimStart());
            model.OfficesList = _officesList.OrderBy(x => x.OfficeLocalization.TrimStart()).Take(model.DisplayCount).Select(q => new TicketOfficeBoxModel(q));
            return model;
        }
        /// <summary>
        /// Metoda ładująca kolejene elementy na liście wszytskich kas biletowych
        /// </summary>
        /// <param name="officeName">nazwa kasy biletowej</param>
        /// <param name="skipCount">ilość elementów jaka jest na stronie</param>
        /// <param name="displayCount">ilość elementów jaką trzeba pobrać</param>
        /// <param name="currentPageId">id strony umbraco na której się znajdujemy</param>
        /// <returns>List obiektów TicketOfficeBoxModel</returns>
        public IEnumerable<TicketOfficeBoxModel> GetMoreTicketOffices(string officeName, int skipCount, int displayCount, int currentPageId)
        {
            var _ticketOfficesNode = _umbracoHelper.TypedContent(currentPageId);
            var _officesList = _ticketOfficesNode.Children.Where("Visible").Select(q => new TicketOffice(q));

            if (!string.IsNullOrEmpty(officeName))
            {
                _officesList = _officesList.Where(q => q.Name.Trim().ToLower().Contains(officeName.Trim().ToLower()));
            }

           // _officesList = _officesList.OrderBy(q => q.OfficeLocalization.TrimStart());
            return _officesList.OrderBy(x => x.OfficeLocalization.TrimStart()).Skip(skipCount).Take(displayCount).Select(q => new TicketOfficeBoxModel(q));
        }

    }
}
