using System.Collections.Generic;
using System.Globalization;
using System.Threading;
using Umbraco.Web.Mvc;

namespace PolRegio.Web.Controllers
{
    /// <summary>
    /// Kontroler, który zawiera wspólne metody
    /// Każdy inny kontroller powinien dziedziczyć po tym kontrolerze
    /// </summary>
    public class PolRegioBaseController : SurfaceController
    {
        /// <summary>
        /// Metoda ustawia CultureInfo po akcji POST
        /// </summary>
        /// <param name="currentCulture">string z nazwą CultureInfo</param>
        public void SetCulture(string currentCulture)
        {
            Thread.CurrentThread.CurrentUICulture = new CultureInfo(currentCulture);
            Thread.CurrentThread.CurrentCulture = new CultureInfo(currentCulture);
        }
        /// <summary>
        /// Metoda ustawia CultureInfo po akcji POST
        /// </summary>
        /// <param name="currentCulture">obiekt typu CultureInfo</param>
        public void SetCulture(CultureInfo currentCulture)
        {
            Thread.CurrentThread.CurrentUICulture = currentCulture;
            Thread.CurrentThread.CurrentCulture = currentCulture;
        }

        public void MapErrorsToModelState(List<KeyValuePair<string, string>> errors)
        {
            foreach (var error in errors)
            {
                ModelState.AddModelError(error.Key, error.Value);
            }
        }
    }
}