using PolRegio.Domain.Models.View.Article;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PolRegio.Domain.Services.Article
{
    /// <summary>
    /// Interfejs zawierający metody do obsługi strony z artykułem
    /// </summary>
    public interface IArticleService
    {
        /// <summary>
        /// Metoda zwracająca obiekt typu ArticlePageViewModel zawierający 
        /// elementy wyświetlane na stronie artykułu
        /// </summary>
        /// <param name="currentUmbracoPageId">id strony umbraco na której się znajdujemy</param>
        /// <returns>Model zawierający elementy strony z artykułem</returns>
        ArticlePageViewModel GetArticlePageViewModel(int currentUmbracoPageId);
    }
}
