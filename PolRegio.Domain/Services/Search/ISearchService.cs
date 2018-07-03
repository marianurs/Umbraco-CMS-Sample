using PolRegio.Domain.Models.View.Search;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PolRegio.Domain.Services.Search
{
    /// <summary>
    /// Interfejs zawierający generowanie wyników searcha na stronie
    /// </summary>
    public interface ISearchService
    {
        /// <summary>
        /// Metoda zwracająca obiekt typu SearchViewModel
        /// </summary>
        /// <param name="currentUmbracoPageId">id strony umbraco na której się znajdujemy</param>
        /// <returns>obiekt klasy SearchViewModel</returns>
        SearchViewModel GetSearchViewModel(int currentUmbracoPageId);

        SearchViewModel SearchResult(SearchViewModel _searchModel);
        SearchViewModel GetMoreSearchResult(string name, int skipCount, int displayCount, int currentPageId);
    }
}
