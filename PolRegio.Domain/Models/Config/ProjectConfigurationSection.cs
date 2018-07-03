using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;

namespace PolRegio.Domain.Models.Config
{
    /// <summary>
    /// Class describe custom config section
    /// </summary>
    public class ProjectConfigurationSection : ConfigurationSection
    {
        /// <summary>
        /// Returns configuration items
        /// </summary>
        [ConfigurationProperty("conifiguration")]
        public BaseConfigurationCollection<EnvironmentElement> Items
        {
            get
            {
                BaseConfigurationCollection<EnvironmentElement> env =
                (BaseConfigurationCollection<EnvironmentElement>)base["conifiguration"];
                return env;
            }
        }
    }
}
