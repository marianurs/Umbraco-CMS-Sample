using PolRegio.Domain.Services.CacheService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Caching;

namespace PolRegio.Services.CacheService
{
    /// <summary>
    /// Klasa implementująca interface ICacheService
    /// </summary>
    public class CacheService : ICacheService
    {
        /// <summary>
        /// Metoda zwracająca obiekt typu T z cache
        /// </summary>
        /// <typeparam name="T">T type</typeparam>
        /// <param name="cacheID">cache id</param>
        /// <param name="getItemCallback">funkcja wywoływana jeżeli nie znaleziono obiektu w cache</param>
        /// <returns>T object</returns>
        public T Get<T>(string cacheID, Func<T> getItemCallback) where T : class
        {
            T item = HttpRuntime.Cache.Get(cacheID) as T;
            if (item == null)
            {
                item = getItemCallback();
                if (item != null)
                    HttpContext.Current.Cache.Insert(cacheID, item);
            }
            return item;
        }
        /// <summary>
        /// Metoda zwracająca obiekt typu T z cache
        /// </summary>
        /// <typeparam name="T">T type</typeparam>
        /// <param name="cacheID">cache id</param>
        /// <param name="expirationMinute">ilość minut po jakich wygasa cache</param>
        /// <param name="getItemCallback">funkcja wywoływana jeżeli nie znaleziono obiektu w cache</param>
        /// <returns>T object</returns>
        public T Get<T>(string cacheID, int expirationMinute, Func<T> getItemCallback) where T : class
        {
            T item = HttpRuntime.Cache.Get(cacheID) as T;
            if (item == null)
            {
                var expirationDate = DateTime.Now.AddMinutes(expirationMinute);
                item = getItemCallback();
                if (item != null)
                    HttpContext.Current.Cache.Insert(cacheID, item, null, expirationDate, Cache.NoSlidingExpiration);
            }
            return item;
        }
        /// <summary>
        /// Metoda usuwająca cache
        /// </summary>
        /// <param name="cacheID">cache id</param>
        public void Remove(string cacheID)
        {
            var item = HttpRuntime.Cache.Get(cacheID);
            if (item != null)
            {
                HttpRuntime.Cache.Remove(cacheID);
            }
        }
    }
}
