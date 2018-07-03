using PolRegio.Domain.Models.View.BipPage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PolRegio.Domain.Services.BipModels
{
    /// <summary>
    /// Interfejs zawierający obsługę BIP
    /// </summary>
    public interface IBIPService
    {
        /// <summary>
        /// Metoda zwracająca obiekt typu BIPFormViewModel zawierający 
        /// elementy wyświetlane na stronie formularza BIP
        /// </summary>
        /// <param name="currentUmbracoPageId">id strony umbraco na której się znajdujemy</param>
        /// <returns></returns>
        BIPFormViewModel GetBipFormView(int currentUmbracoPageId);

        BIPArticleViewModel BIPArticleViewModel(int currentUmbracoPageId);
        /// <summary>
        /// Metoda zwracająca klasę zawierającą elementy wyświetlane na stronie głównej BIP
        /// </summary>
        /// <param name="currentUmbracoPageId">id strony umbraco na której się znajdujemy</param>
        /// <returns>Model zawierający elementy strony głównej BIP</returns>
        BIPPageViewModel BIPPageViewModel(int currentUmbracoPageId);
        /// <summary>
        /// Metoda zwracająca klasę zawierającą elementy wyświetlane na stronie głównej BIP
        /// </summary>
        /// <param name="model">model BIPPageViewModel</param>
        /// <returns>Model zawierający elementy strony głównej BIP</returns>
        BIPPageViewModel BIPPageViewModel(BIPPageViewModel model);
    }
}
