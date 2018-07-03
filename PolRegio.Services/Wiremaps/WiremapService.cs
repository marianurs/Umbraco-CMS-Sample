using PolRegio.Domain.Services.Wiremaps;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PolRegio.Domain.Models.View.Wiremaps;
using Umbraco.Web;
using PolRegio.Helpers.Enums;
using PolRegio.Domain.Models.UmbracoCreate;
using PolRegio.Domain.Models.Components;

namespace PolRegio.Services.Wiremaps
{
    /// <summary>
    /// Klasa implementująca interfejs IWiremapService
    /// </summary>
    public class WiremapService : IWiremapService
    {
        /// <summary>
        /// Obiekt typu UmbracoHelper
        /// </summary>
        private UmbracoHelper _umbracoHelper;
        /// <summary>
        /// Konstruktor klasy
        /// </summary>
        public WiremapService()
        {
            _umbracoHelper = new UmbracoHelper(UmbracoContext.Current);
        }
        public WiremapPageViewModel GetWiremapPageViewModel(int currentUmbracoPageId)
        {
            var _model = new WiremapPageViewModel();

            //Aktualna strona, na której się znajdujemy
            var _currentPage = _umbracoHelper.TypedContent(currentUmbracoPageId);
            _model.WiremapContent = new Wiremap(_currentPage);

            if (_model.WiremapContent.AttachmentsList != null)
            {
                _model.DownloadDocuments = _model.WiremapContent.AttachmentsList.Fieldsets.Where(x => x != null && x.Properties.Any() && !x.Disabled).Select(q => new DownloadItem()
                {
                    DocumentUrl = q.GetValue<string>("addDoc"),
                    DocumentName = q.GetValue<string>("articleDocName"),
                    DocumentDate = q.GetValue<DateTime>("chooseDate")
                });
            }
            return _model;
        }
    }
}
