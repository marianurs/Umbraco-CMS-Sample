using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PolRegio.Domain.Services.CacheService
{
    /// <summary>
    /// Interfejs zawierający metody obsługujące cache
    /// </summary>
    public interface ICacheService
    {
        /// <summary>
        /// Metoda zwracająca obiekt typu T z cache
        /// </summary>
        /// <typeparam name="T">T type</typeparam>
        /// <param name="cacheID">cache id</param>
        /// <param name="getItemCallback">funkcja wywoływana jeżeli nie znaleziono obiektu w cache</param>
        /// <returns>T object</returns>
        T Get<T>(string cacheID, Func<T> getItemCallback) where T : class;
        /// <summary>
        /// Metoda zwracająca obiekt typu T z cache
        /// </summary>
        /// <typeparam name="T">T type</typeparam>
        /// <param name="cacheID">cache id</param>
        /// <param name="expirationMinute">ilość minut po jakich wygasa cache</param>
        /// <param name="getItemCallback">funkcja wywoływana jeżeli nie znaleziono obiektu w cache</param>
        /// <returns>T object</returns>
        T Get<T>(string cacheID, int expirationMinute, Func<T> getItemCallback) where T : class;
        /// <summary>
        /// Metoda usuwająca cache
        /// </summary>
        /// <param name="cacheID">cache id</param>
        void Remove(string cacheID);
    }
}
