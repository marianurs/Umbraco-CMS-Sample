using Umbraco.Core.Persistence;
using Umbraco.Core.Persistence.DatabaseAnnotations;

namespace PolRegio.Domain.Models.Database
{
    [TableName("PolRegioUserAgreement")]
    [PrimaryKey("Id", autoIncrement = true)]
    public class UserAgreementDB
    {
        [PrimaryKeyColumn(AutoIncrement = true)]
        public int Id { get; set; }

        [ForeignKey(typeof(UserDB))]
        public int UserId { get; set; }

        [ForeignKey(typeof(AgreementDB))]
        public int AgreementId { get; set; }

        public bool Value { get; set; }
    }
}
