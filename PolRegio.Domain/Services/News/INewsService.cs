using PolRegio.Domain.Models.Components.Home;
using PolRegio.Domain.Models.View.News;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PolRegio.Domain.Services.News
{
    /// <summary>
    /// Interfejs zawierający metody do obsługi informacji(newsów)
    /// </summary>
    public interface INewsService
    {
        /// <summary>
        /// Metorda zwracająca klasę zawierającą elementy wyświetlane na stronie wszytskich artykułów
        /// </summary>
        /// <param name="currentUmbracoPageId">id strony umbraco na której się znajdujemy</param>
        /// <param name="typeFromUrl"></param>
        /// <returns>Model zawierający elementy strony ze wszytskimi artykułami</returns>
        NewsPageViewModel GetNewsBoxesModel(int currentUmbracoPageId, string typeFromUrl);
        /// <summary>
        /// Metorda zwracająca klasę zawierającą elementy wyświetlane na stronie wszytskich artykułów
        /// </summary>
        /// <param name="model">Obiekt klasy NewsPageViewModel</param>
        /// <returns>Gotowy model do wyświetlenia na stronie wszystkich artykułów</returns>
        NewsPageViewModel GetNewsBoxesModel(NewsPageViewModel model);
        /// <summary>
        /// Metoda ładująca kolejene elementy na liście informacji
        /// </summary>
        /// <param name="selectedRegionId">wybrany region z dropdown</param>
        /// <param name="selectedTypeIds">wybrane typy informacji</param>
        /// <param name="skipCount">ilość elementów jaka jest na stronie</param>
        /// <param name="displayCount">ilość elementów jaką trzeba pobrać</param>
        /// <param name="currentPageId">id strony umbraco na której się znajdujemy</param>
        /// <returns>List obiektów NewsBoxModel</returns>
        IEnumerable<NewsBoxModel> GetMoreNews(int selectedRegionId, List<int> selectedTypeIds, int skipCount, int displayCount, int currentPageId);
    }
}
