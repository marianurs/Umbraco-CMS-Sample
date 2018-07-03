using PolRegio.Domain.Models.View.BipPage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PolRegio.Domain.Services.Bip
{
    /// <summary>
    /// Interfejs zawierający obsługę BIP
    /// </summary>
    public interface IBIPArticleService
    {
        /// <summary>
        /// Metoda zwracająca obiekt typu BIPFormViewModel zawierający 
        /// elementy wyświetlane na stronie formularza BIP
        /// </summary>
        /// <param name="currentUmbracoPageId">id strony umbraco na której się znajdujemy</param>
        /// <returns></returns>
        BIPArticleViewModel BIPArticleViewModel(int currentUmbracoPageId);
    }
}
