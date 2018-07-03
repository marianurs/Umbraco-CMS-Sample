using PolRegio.Domain.Models.Config;
using PolRegio.Domain.Services.Config;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PolRegio.Services.Config
{
    /// <summary>
    /// Klasa implementująca interfejs IConfigService, opisujący konfigurację per serwer
    /// </summary>
    public class ConfigService : IConfigService
    {
        /// <summary>
        /// Obiekt typy EnvironmentElement
        /// </summary>
        public static EnvironmentElement Environment { get; set; }
        /// <summary>
        /// Connection string do bazy danych
        /// </summary>
        public string ConnectionString
        {
            get
            {
                var connectionString = Environment.ConnectionString.Value;
                if (String.IsNullOrEmpty(connectionString))
                {
                    throw new Exception(String.Format("Connnection string cannot be null. Please set value of <connectionString value=\"\" /> in ProjectSettings.config file"));
                }
                else
                {
                    return connectionString;
                }
            }
        }
        /// <summary>
        /// Lista customowych kluczy konfiguracyjnych
        /// </summary>
        public IDictionary<string, string> Custom
        {
            get { return Environment.Custom.ToDictionary(x => x.Name, x => x.Value); }
        }
        /// <summary>
        /// Nazwa środowiska na jakim jest uruchamiana apliakcja
        /// </summary>
        public string EnvironmentName
        {
            get { return Environment.EnvironmentName.Value; }
        }
        /// <summary>
        /// Nazwy serwerów na jakich jest uruchamiana aplikacja
        /// </summary>
        public string ServerName
        {
            get { return Environment.ServerName.Value; }
        }
    }
    public class ConfigurationReader
    {
        /// <summary>
        /// Główna metoda zczytująca konfigurację dla danego serwera
        /// </summary>
        public static void ReadConfiguration()
        {
            string currentHost = System.Net.Dns.GetHostName().ToLower();
            var configuration = ConfigurationManager.GetSection("projectconfig") as ProjectConfigurationSection;
            if (configuration != null)
            {
                var currentConfiguration = configuration.Items.Where(x => x.ServerName.Value.ToLower().Contains(currentHost));

                if (currentConfiguration.Count() == 1)
                {
                    ConfigService.Environment = currentConfiguration.First();
                }
                else if (currentConfiguration.Count() == 0)
                {
                    throw new Exception(String.Format("Please set environment for current server {0} in ProjectSettings.config file.", currentHost));
                }
            }
        }
    }
}
