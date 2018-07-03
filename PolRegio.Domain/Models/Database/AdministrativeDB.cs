using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using Umbraco.Core.Persistence;
using Umbraco.Core.Persistence.DatabaseAnnotations;

namespace PolRegio.Domain.Models.Database
{
    /// <summary>
    /// Klasa opisująca dodatkową tablę w bazie danych
    /// Klasa używana w customowej sekcji w CMS
    /// </summary>
    [TableName("PolRegioAdministrative")]
    [DataContract(Name = "polregioadministrative")]
    public class AdministrativeDB
    {
        /// <summary>
        /// Konstruktor klasy
        /// </summary>
        public AdministrativeDB() { }
        /// <summary>
        /// Klucz główny tabeli
        /// </summary>
        [PrimaryKeyColumn(AutoIncrement = true)]
        [DataMember(Name = "id")]
        public int Id { get; set; }
        /// <summary>
        /// Kolumna zawierająca nazwę obiektu
        /// </summary>
        [DataMember(Name = "name")]
        public string Name { get; set; }
        /// <summary>
        /// Kolumna zawierająca flagę czy obiekt jest aktywny
        /// </summary>
        [DataMember(Name = "isenabled")]
        public bool IsEnabled { get; set; }
        /// <summary>
        /// Kolumna zawierająca klucz słownika obiektu
        /// </summary>
        [DataMember(Name = "dictionarykey")]
        public string DictionaryKey { get; set; }
        /// <summary>
        /// Nadpisanie metody ToString
        /// </summary>
        /// <returns>object Name</returns>
        public override string ToString()
        {
            return Name;
        }
    }
}
