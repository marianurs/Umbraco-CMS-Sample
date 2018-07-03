using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;

namespace PolRegio.Domain.Models.Config
{
    /// <summary>
    /// Class describe custom configuration ValueConfigurationElement
    /// </summary>
    public class ValueConfigurationElement : ConfigurationElement
    {
        /// <summary>
        /// Constructor allowing name, url, and port to be specified. 
        /// </summary>
        /// <param name="newValue">string value</param>
        public ValueConfigurationElement(String newValue)
        {
            Value = newValue;
        }

        /// <summary>
        /// Default constructor, will use default values as defined 
        /// below. 
        /// </summary>
        public ValueConfigurationElement()
        {
        }
        /// <summary>
        /// Custom configuration element value
        /// </summary>
        [ConfigurationProperty("value",
            DefaultValue = "",
            IsRequired = true,
            IsKey = true)]
        public string Value
        {
            get
            {
                return (string)this["value"];
            }
            set
            {
                this["value"] = value;
            }
        }
        /// <summary>
        /// Override methods that deserialize configuration element
        /// </summary>
        protected override void DeserializeElement(
           System.Xml.XmlReader reader,
            bool serializeCollectionKey)
        {
            base.DeserializeElement(reader,
                serializeCollectionKey);
            // You can your custom processing code here.
        }

        /// <summary>
        /// Override methods that serialize configuration element
        /// </summary>
        protected override bool SerializeElement(
            System.Xml.XmlWriter writer,
            bool serializeCollectionKey)
        {
            bool ret = base.SerializeElement(writer,
                serializeCollectionKey);
            // You can enter your custom processing code here. 
            return ret;

        }

        /// <summary>
        /// Override methods that returns true if configuration element was modified
        /// </summary>
        protected override bool IsModified()
        {
            bool ret = base.IsModified();
            // You can enter your custom processing code here. 
            return ret;
        }
    }
}
