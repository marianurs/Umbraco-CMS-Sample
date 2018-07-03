using System.Runtime.Serialization;
using Umbraco.Core.Persistence;
using Umbraco.Core.Persistence.DatabaseAnnotations;

namespace PolRegio.Domain.Models.Database
{
    [TableName("PolRegioStations")]
    [DataContract(Name = "polregiostations")]
    [PrimaryKey("Id", autoIncrement = false)]
    [ExplicitColumns]
    public class StationDB
    {
        [Column("Id")]
        public int Id { get; set; }

        [Column("Name")]
        public string Name { get; set; }

        [Column("Latitude")]
        [NullSetting(NullSetting = NullSettings.Null)]
        public double? Latitude { get; set; }

        [Column("Longitude")]
        [NullSetting(NullSetting = NullSettings.Null)]
        public double? Longitude { get; set; }
    }
}