using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;

namespace PolRegio.Domain.Models.Config
{
    /// <summary>
    /// EnvironmentElement class
    /// </summary>
    public class EnvironmentElement : ConfigurationElement
    {
        /// <summary>
        /// EnvironmentName property
        /// </summary>
        [ConfigurationProperty("environmentName")]
        public ValueConfigurationElement EnvironmentName
        {
            get
            {
                ValueConfigurationElement environmentName =
                (ValueConfigurationElement)base["environmentName"];
                return environmentName;
            }
            set
            {
                base["environmentName"] = value;
            }
        }
        /// <summary>
        /// ServerName property
        /// </summary>
        [ConfigurationProperty("serverNames")]
        public ValueConfigurationElement ServerName
        {
            get
            {
                ValueConfigurationElement serverNames =
                (ValueConfigurationElement)base["serverNames"];
                return serverNames;
            }
            set
            {
                base["serverNames"] = value;
            }
        }
        /// <summary>
        /// ConnectionString property
        /// </summary>
        [ConfigurationProperty("connectionString")]
        public ValueConfigurationElement ConnectionString
        {
            get
            {
                ValueConfigurationElement connectionString =
                (ValueConfigurationElement)base["connectionString"];
                return connectionString;
            }
            set
            {
                base["connectionString"] = value;
            }
        }
        /// <summary>
        /// Custom configuration key collection property
        /// </summary>
        [ConfigurationProperty("custom")]
        public BaseConfigurationCollection<KeyValueConfigurationElement> Custom
        {
            get
            {
                BaseConfigurationCollection<KeyValueConfigurationElement> custom =
                (BaseConfigurationCollection<KeyValueConfigurationElement>)base["custom"];
                return custom;
            }
            set
            {
                base["custom"] = value;
            }
        }
    }
}
