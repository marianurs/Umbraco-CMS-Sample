using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace PolRegio.Domain.Services.Database
{
    /// <summary>
    /// Interfejs zawierający obsługę customowych tabel z bazy danych
    /// </summary>
    public interface IDBService
    {
        /// <summary>
        ///  Metoda zwracająca listę obiektów
        /// </summary>
        /// <typeparam name="T">Typ obiektu na liście</typeparam>
        /// <param name="tableName">nazwa tabeli</param>
        /// <returns>Enumerable T</returns>
        IEnumerable<T> GetAll<T>(string tableName) where T : class;
        /// <summary>
        /// Metoda zwracająca listę obiektów
        /// </summary>
        /// <typeparam name="T">Typ obiektu na liście</typeparam>
        /// <param name="tableName">nazwa tabeli</param>
        /// <param name="condition">warunki Linq dotyczące obiektu T</param>
        /// <returns>Enumerable T </returns>
        IEnumerable<T> GetAll<T>(string tableName, Expression<Func<T, bool>> condition) where T : class;
        /// <summary>
        /// Metoda zwracająca obiekt T
        /// </summary>
        /// <typeparam name="T">Typ obiektu na liście</typeparam>
        /// <param name="tableName">nazwa tabeli</param>
        /// <param name="condition">warunki Linq dotyczące obiektu T</param>
        /// <returns>T</returns>
        T GetByCondition<T>(string tableName, Expression<Func<T, bool>> condition) where T : class;
    }
}
