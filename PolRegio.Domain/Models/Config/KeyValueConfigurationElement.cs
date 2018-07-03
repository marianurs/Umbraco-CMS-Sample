using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;

namespace PolRegio.Domain.Models.Config
{
    /// <summary>
    /// Class describe custom KeyValueConfigurationElement
    /// </summary>
    public class KeyValueConfigurationElement : ValueConfigurationElement
    {
        /// <summary>
        /// Configuration key value property
        /// </summary>
        [ConfigurationProperty("name",
            DefaultValue = "",
            IsRequired = true,
            IsKey = true)]
        public string Name
        {
            get
            {
                return (string)this["name"];
            }
            set
            {
                this["name"] = value;
            }
        }
    }
}
