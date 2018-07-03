using Umbraco.Core.Persistence;
using Umbraco.Core.Persistence.DatabaseAnnotations;

namespace PolRegio.Domain.Models.Database
{
    [TableName("PolRegioAgreement")]
    [PrimaryKey("Id", autoIncrement = true)]
    public class AgreementDB
    {
        [PrimaryKeyColumn(AutoIncrement = true)]
        public int Id { get; set; }

        public string Text { get; set; }

        public string ShortName { get; set; }

        public bool IsActive { get; set; }

        public bool IsRequired { get; set; }

        public bool IsPreference { get; set; }
    }
}