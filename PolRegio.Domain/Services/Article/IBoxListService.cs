using PolRegio.Domain.Models.Components.Home;
using PolRegio.Domain.Models.View.Article;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PolRegio.Domain.Services.Article
{
    /// <summary>
    /// Interfejs zawierający metody do obsługi strony z listą ofert/artykułów
    /// </summary>
    public interface IBoxListService
    {
        /// <summary>
        /// Metorda zwracająca klasę zawierającą elementy wyświetlane na stronie wszytskich artykułów
        /// </summary>
        /// <param name="currentUmbracoPageId">id strony umbraco na której się znajdujemy</param>
        /// <returns>Model zawierający elementy strony ze wszytskimi artykułami</returns>
        ArticleBoxViewModel GetArticleBoxesModel(int currentUmbracoPageId);
        /// <summary>
        /// Metorda zwracająca klasę zawierającą elementy wyświetlane na stronie wszytskich artykułów
        /// </summary>
        /// <param name="model">Obiekt klasy ArticleBoxViewModel</param>
        /// <returns>Gotowy model do wyświetlenia na stronie wszystkich artykułów</returns>
        ArticleBoxViewModel GetArticleBoxesModel(ArticleBoxViewModel model);
        /// <summary>
        /// Metoda ładująca kolejene elementy na liście informacji
        /// </summary>
        /// <param name="skipCount">ilość elementów jaka jest na stronie</param>
        /// <param name="displayCount">ilość elementów jaką trzeba pobrać</param>
        /// <param name="currentPageId">id strony umbraco na której się znajdujemy</param>
        /// <returns>List obiektów OfferBoxModel</returns>
        IEnumerable<OfferBoxModel> GetMoreArticle(int skipCount, int displayCount, int currentPageId);

        /// <summary>
        /// Metorda zwracająca klasę zawierającą elementy wyświetlane na stronie wszytskich ofert regionalnych
        /// </summary>
        /// <param name="currentUmbracoPageId">id strony umbraco na której się znajdujemy</param>
        /// <param name="selectedRegionId">Wybrany region</param>
        /// <returns>Model zawierający elementy strony ze wszytskimi artykułami</returns>
        RegionalOfferBoxViewModel GetRegionalArticleBoxesModel(int currentUmbracoPageId);
        /// <summary>
        /// Metorda zwracająca klasę zawierającą elementy wyświetlane na stronie wszytskich ofert regionalnych
        /// </summary>
        /// <param name="model">Obiekt klasy RegionalOfferBoxViewModel</param>
        /// <returns>Gotowy model do wyświetlenia na stronie wszystkich artykułów</returns>
        RegionalOfferBoxViewModel GetRegionalArticleBoxesModel(RegionalOfferBoxViewModel model);
        /// <summary>
        /// Metoda ładująca kolejene elementy na liście ofert regionalnych
        /// </summary>
        /// <param name="selectedRegionId">wybrany region z dropdown</param>
        /// <param name="skipCount">ilość elementów jaka jest na stronie</param>
        /// <param name="displayCount">ilość elementów jaką trzeba pobrać</param>
        /// <param name="currentPageId">id strony umbraco na której się znajdujemy</param>
        /// <returns>List obiektów OfferBoxModel</returns>
        IEnumerable<OfferBoxModel> GetMoreRegionalArticle(int selectedRegionId, int skipCount, int displayCount, int currentPageId);
    }
}
