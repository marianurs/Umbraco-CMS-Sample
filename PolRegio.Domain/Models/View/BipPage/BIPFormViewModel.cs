using PolRegio.Domain.Models.Components.BipPage;
using PolRegio.Helpers.Enums;
using System.ComponentModel.DataAnnotations;
using System.Globalization;

namespace PolRegio.Domain.Models.View.BipPage
{
    /// <summary>
    /// Klasa zawierająca model formularza o udostępnienie
    /// informacji publicznej
    /// </summary>
    public class BIPFormViewModel
    {
        /// <summary>
        /// Imię i nazwisko lub nazwa podmiotu
        /// </summary>
        [Required]
        public string UserName { get; set; }
        /// <summary>
        /// Adres
        /// </summary>
        [Required]
        public string UserAddress { get; set; }
        /// <summary>
        /// Adres e-mail
        /// </summary>
        [RegularExpression(@"^([a-zA-Z0-9_\-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([a-zA-Z0-9\-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$")]
        public string UserEmail { get; set; }
        /// <summary>
        /// Telefon
        /// </summary>
        public string UserPhone { get; set; }
        /// <summary>
        /// Zakres tematyczny danych
        /// </summary>
        [Required]
        public string UserDescription { get; set; }
        /// <summary>
        /// Sposób udostępnienia informacji
        /// </summary>
        [Required]
        public BIPSharingInformationTypeEnum? SharingInformationType { get; set; }
        /// <summary>
        /// Rodzaj nośnika
        /// </summary>
        public string MediaType { get; set; }
        /// <summary>
        /// Zgoda na przetwarzanie danych osobowych
        /// </summary>
        [Range(typeof(bool), "true", "true")]
        public bool AcceptProcessingPersonalData { get; set; }

        public string SharingInformation { get; set; }
        /// <summary>
        /// Aktualne id strony
        /// </summary>
        public int CurrentUmbracoPageId { get; set; }
        /// <summary>
        /// Culture info obecnej strony
        /// </summary>
        public CultureInfo CurrentPageCulture { get; set; }
        /// <summary>
        /// Element zawierający zwrotkę z wysyłki emaila
        /// </summary>
        public BipSendEmailResponse SendResponse { get; set; }
    }
}
