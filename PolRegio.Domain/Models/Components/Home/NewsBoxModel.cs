using Newtonsoft.Json;
using PolRegio.Domain.Models.UmbracoCreate;
using PolRegio.Helpers.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Umbraco.Core.Models;

namespace PolRegio.Domain.Models.Components.Home
{
    /// <summary>
    /// Klasa zawierająca obiekt boxa z aktualności
    /// </summary>
    public class NewsBoxModel : ArticleWithDoubleFiltr
    {
        /// <summary>
        /// Konstuktor klasy 
        /// </summary>
        /// <param name="content">Obiekt IPublishedContent (obiekt aktualności z CMS)</param>
        public NewsBoxModel(IPublishedContent content) : base(content)
        {
        }
        /// <summary>
        /// Url na który ma kierować box
        /// </summary>
        public string BoxUrl { get { return Content.Url; } }
        /// <summary>
        /// Kategoria boxa
        /// W zależności od kategorii jest przypisana ikona do boxa
        /// </summary>
        public string BoxType
        {
            get
            {
                if (this.ArticleCategory != null)
                {
                    var _articleType = JsonConvert.DeserializeObject<IEnumerable<NuPickersSqlDropDownPicker>>(this.ArticleCategory.SavedValue.ToString()).FirstOrDefault();
                    return Enum.Parse(typeof(ArticleTypeEnum), _articleType.Key).ToString();
                }

                return string.Empty;
            }
        }
    }
}
