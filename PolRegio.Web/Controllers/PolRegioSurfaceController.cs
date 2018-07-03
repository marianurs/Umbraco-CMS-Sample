using PolRegio.Domain.Services.Layout;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Umbraco.Web.Mvc;

namespace PolRegio.Web.Controllers
{
    /// <summary>
    /// Główny kontroler projektu, który dziedziczy po Umbraco SurfaceController
    /// Znajdują się w nim metody wykorzystywane na kontrolerach w projekcie
    /// </summary>
    public class PolRegioSurfaceController : SurfaceController
    {
        /// <summary>
        /// prywatne pole ILayoutService
        /// </summary>
        private readonly ILayoutService _layoutService;
        /// <summary>
        /// Konstruktor kontrolera PolRegioSurfaceController
        /// </summary>
        /// <param name="layoutService">wstrzyknięty obiekt ILayoutService z DependencyInjection</param>
        public PolRegioSurfaceController(ILayoutService layoutService)
        {
            _layoutService = layoutService;
        }
        /// <summary>
        /// Metoda przygotowująca obiekt HeaderViewModel
        /// i przekazująca go do wyrenderowania do widoku
        /// "Components/Header"
        /// </summary>
        /// <returns>Wyrenderowany HTML z częś</returns>
        [ChildActionOnly]
        public ActionResult RenderHeader()
        {
            var _model = _layoutService.GetHeaderViewModel(CurrentPage.Id);
            return PartialView("Components/Header", _model);
        }

        [ChildActionOnly]
        public ActionResult RenderFooter()
        {
            var _model = _layoutService.GetFooterViewModel(CurrentPage.Id);
            return PartialView("Components/Footer", _model);
        }
    }
}