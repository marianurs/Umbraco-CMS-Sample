using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using umbraco.businesslogic;
using umbraco.interfaces;

namespace PolRegio.Cms.Application
{
    /// <summary>
    /// Customowa sekcja w CMS do obsługi regionów i filtrów na artykułach
    /// Atrybut Application opisuję sekcję:
    /// alias - additionalsettings, name - AdditionalSettings, icon - traysettings, sortOrder - 15
    /// </summary>
    [Application("additionalsettings", "AdditionalSettings", "traysettings", 15)]
    public class AdditionalSettingsSection : IApplication
    {
    }
}
