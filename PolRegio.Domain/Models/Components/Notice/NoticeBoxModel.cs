using Newtonsoft.Json;
using PolRegio.Domain.Models.UmbracoCreate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Umbraco.Core.Models;

namespace PolRegio.Domain.Models.Components.Notice
{
    /// <summary>
    /// Klasa zawierająca obiekt boxa z zamówieniem
    /// </summary>
    public class NoticeBoxModel : NoticesFiltr
    {
        /// <summary>
        /// Konstruktor klasy
        /// </summary>
        /// <param name="content"></param>
        public NoticeBoxModel(IPublishedContent content) : base(content)
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

        public string LawActKey
        {
            get
            {
                if (this.LawAct != null)
                {
                    var _lawAct = JsonConvert.DeserializeObject<IEnumerable<NuPickersSqlDropDownPicker>>(this.LawAct.SavedValue.ToString()).FirstOrDefault();
                    return _lawAct.Label;
                }

                return string.Empty;
            }
        }
    }
}
