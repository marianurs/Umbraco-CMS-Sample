using PolRegio.Domain.Models.UmbracoCreate;
using PolRegio.Domain.Models.View.Tags;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PolRegio.Domain.Services.Tags
{
    /// <summary>
    /// Interfejs zawierający generowanie wyników artykułów na stronie a listą artykułów wyzukanych o tagu
    /// </summary>
    public interface ITagsService
    {
        TagsViewModel SearchResult(int currentUmbracoPageId, string tagText);
        Error404 errorPage(int currentUmbracoPageId);
    }
}
