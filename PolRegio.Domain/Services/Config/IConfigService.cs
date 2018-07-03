using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PolRegio.Domain.Services.Config
{
    /// <summary>
    /// Interfejs opisujący konfigurację per serwer
    /// </summary>
    public interface IConfigService
    {
        /// <summary>
        /// Nazwa środowiska na jakim jest uruchamiana apliakcja
        /// </summary>
        string EnvironmentName { get; }
        /// <summary>
        /// Nazwy serwerów na jakich jest uruchamiana aplikacja
        /// </summary>
        string ServerName { get; }
        /// <summary>
        /// Connection string do bazy danych
        /// </summary>
        string ConnectionString { get; }
        /// <summary>
        /// Lista customowych kluczy konfiguracyjnych
        /// </summary>
        IDictionary<string, string> Custom { get; }
    }
}
