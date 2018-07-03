using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PolRegio.Domain.Models.ISS
{
    /// <summary>
    /// Model opisujący obiekt Stacji
    /// </summary>
    public class ISSStationItem
    {
        /// <summary>
        /// Identyfikator stacji
        /// </summary>
        public int stationId { get; set; }
        /// <summary>
        /// Nazwa stacji
        /// </summary>
        public string value { get; set; }

        public double? latitude { get; set; }
        public double? longitude { get; set; }
    }
}
