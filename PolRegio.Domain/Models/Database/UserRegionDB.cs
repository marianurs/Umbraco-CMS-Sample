using Umbraco.Core.Persistence;
using Umbraco.Core.Persistence.DatabaseAnnotations;

namespace PolRegio.Domain.Models.Database
{
    [TableName("PolRegioUserRegion")]
    [PrimaryKey("Id", autoIncrement = true)]
    public class UserRegionDB
    {
        [PrimaryKeyColumn(AutoIncrement = true)]
        public int Id { get; set; }

        [ForeignKey(typeof(UserDB))]
        public int UserId { get; set; }

        [ForeignKey(typeof(RegionDB))]
        public int RegionId { get; set; }
    }
}
