using Archetype.Models;
using PolRegio.Domain.Models.Components;
using PolRegio.Domain.Models.UmbracoCreate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using Umbraco.Core.Models;

namespace PolRegio.Domain.Models.View.AccordionPage
{
    /// <summary>
    /// Klasa zawierająca obiekt pojedynczego akordeonu wyświetlane na stronie szczegółów akordeonu
    /// </summary>
    public class AccordionViewModel
    {
        private readonly ArchetypeFieldsetModel _model;
        /// <summary>
        /// Konstruktor
        /// </summary>
        /// <param name="model"></param>
        public AccordionViewModel(ArchetypeFieldsetModel model)
        {
            _model = model;
        }
        /// <summary>
        /// Tytuł elementu
        /// </summary>
        public string Title { get { return _model.GetValue<string>("itemTitle"); } }
        /// <summary>
        /// Treść elementu
        /// </summary>
        public IHtmlString Description { get { return _model.GetValue<IHtmlString>("itemText"); } }
        /// <summary>
        /// Lista dokumentów do pobrania 
        /// </summary
        public IEnumerable<DownloadItem> DownloadDocuments
        {
            get
            {
                if (_model.HasValue("itemAttachments"))
                {
                    var _attachmentsList = _model.GetValue<ArchetypeModel>("itemAttachments");

                    return _attachmentsList.Fieldsets.Where(x => x != null && x.Properties.Any() && !x.Disabled).Select(q => new DownloadItem()
                    {
                        DocumentUrl = q.GetValue<string>("addDoc"),
                        DocumentName = q.GetValue<string>("articleDocName"),
                        DocumentDate = q.GetValue<DateTime>("chooseDate")
                    });
                }
                return null;
            }
        }
    }
}
