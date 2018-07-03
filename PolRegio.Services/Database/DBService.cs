using PolRegio.Domain.Services.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Linq.Expressions;
using Umbraco.Core.Persistence;
using Umbraco.Core;

namespace PolRegio.Services.Database
{
    /// <summary>
    /// Klasa implementująca interface IDBService
    /// </summary>
    public class DBService : IDBService
    {
        /// <summary>
        ///  Metoda zwracająca listę obiektów
        /// </summary>
        /// <typeparam name="T">Typ obiektu na liście</typeparam>
        /// <param name="tableName">nazwa tabeli</param>
        /// <returns>Enumerable T</returns>
        public IEnumerable<T> GetAll<T>(string tableName) where T : class
        {
            var query = new Sql().Select("*").From(tableName);
            return ApplicationContext.Current.DatabaseContext.Database.Fetch<T>(query);
        }
        /// <summary>
        /// Metoda zwracająca listę obiektów
        /// </summary>
        /// <typeparam name="T">Typ obiektu na liście</typeparam>
        /// <param name="tableName">nazwa tabeli</param>
        /// <param name="condition">warunki Linq dotyczące obiektu T</param>
        /// <returns>Enumerable T </returns>
        public IEnumerable<T> GetAll<T>(string tableName, Expression<Func<T, bool>> condition) where T : class
        {
            var query = new Sql().Select("*").From(tableName).Where(condition);
            return ApplicationContext.Current.DatabaseContext.Database.Fetch<T>(query);
        }
        /// <summary>
        /// Metoda zwracająca obiekt T
        /// </summary>
        /// <typeparam name="T">Typ obiektu na liście</typeparam>
        /// <param name="tableName">nazwa tabeli</param>
        /// <param name="condition">warunki Linq dotyczące obiektu T</param>
        /// <returns>T</returns>
        public T GetByCondition<T>(string tableName, Expression<Func<T, bool>> condition) where T : class
        {
            var query = new Sql().Select("*").From(tableName).Where(condition);
            return ApplicationContext.Current.DatabaseContext.Database.Fetch<T>(query).FirstOrDefault();
        }
    }
}
