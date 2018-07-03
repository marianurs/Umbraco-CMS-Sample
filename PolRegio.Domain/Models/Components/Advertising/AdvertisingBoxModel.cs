using Newtonsoft.Json;
using PolRegio.Domain.Models.UmbracoCreate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Umbraco.Core.Models;

namespace PolRegio.Domain.Models.Components.Advertising
{
    /// <summary>
    /// Klasa zawierająca obiekt boxa z postępowaniem
    /// </summary>
    public class AdvertisingBoxModel : NoticesAndSaleFiltr
    {
        /// <summary>
        /// Konstruktor klasy
        /// </summary>
        /// <param name="content"></param>
        public AdvertisingBoxModel(IPublishedContent content) : base(content)
        {
        }

        /// <summary>
        /// Url na który ma kierować box
        /// </summary>
        public string BoxUrl { get { return Content.Url; } }

        public string AdministrativeKey
        {
            get
            {
                if (this.Administrative != null)
                {
                    var _administrative = JsonConvert.DeserializeObject<IEnumerable<NuPickersSqlDropDownPicker>>(this.Administrative.SavedValue.ToString()).FirstOrDefault();
                    return _administrative.Label;
                }

                return string.Empty;
            }
        }
    }
}
