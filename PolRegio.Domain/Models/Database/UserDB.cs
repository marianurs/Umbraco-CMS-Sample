using System;
using Umbraco.Core.Persistence;
using Umbraco.Core.Persistence.DatabaseAnnotations;

namespace PolRegio.Domain.Models.Database
{
    [TableName("PolRegioUser")]
    [PrimaryKey("Id", autoIncrement = true)]
    public class UserDB
    {
        [PrimaryKeyColumn(AutoIncrement = true)]
        public int Id { get; set; }

        public string UserName { get; set; }

        public string UserSurname { get; set; }

        public string UserEmail { get; set; }

        public string UserPassword { get; set; }

        public string UserPhone { get; set; }

        public bool AcceptEmails { get; set; } //todo pole nie używane

        public bool AcceptNews { get; set; } //todo pole nie używane

        public bool AcceptNewsletter { get; set; } //todo pole nie używane

        public bool AcceptTerms { get; set; } //todo pole nie używane

        public bool AcceptData { get; set; } //todo pole nie używane

        public bool IsActive { get; set; }

        public string ActivationToken { get; set; }

        public string Locale { get; set; }

        public Guid? SalesmanagoContactId { get; set; }
    }
}