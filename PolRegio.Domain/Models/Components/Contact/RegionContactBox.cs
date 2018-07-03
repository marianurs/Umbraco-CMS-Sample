using Archetype.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace PolRegio.Domain.Models.Components.Contact
{
    /// <summary>
    /// Klasa opisująca kontakt do regionu
    /// Pobierana z Archetype ze Strony z oddziałów
    /// </summary>
    public class RegionContactBox
    {
        private readonly ArchetypeFieldsetModel _model;
        public RegionContactBox(ArchetypeFieldsetModel model)
        {
            _model = model;
        }
        #region Dane podstawowe
        /// <summary>
        /// Dane podstawowe - Nazwa oddziału
        /// </summary>
        public string Name { get { return _model.GetValue<string>("officeName"); } }
        /// <summary>
        /// Dane podstawowe - opis oddziału
        /// </summary>
        public IHtmlString Description { get { return _model.GetValue<IHtmlString>("officeDescription"); } }
        /// <summary>
        /// Dane podstawowe - telefon oddziału
        /// </summary>
        public string Phone { get { return _model.GetValue<string>("phoneNumber"); } }
        /// <summary>
        /// Dane podstawowe - fax oddziału
        /// </summary>
        public string Fax { get { return _model.GetValue<string>("faxNumber"); } }
        /// <summary>
        /// Dane podstawowe - email oddziału
        /// </summary>
        public string Email { get { return _model.GetValue<string>("emailAdress"); } }
        /// <summary>
        /// Dane podstawowe - region oddziału
        /// </summary>
        public NuPickersSqlDropDownPicker Region
        {
            get
            {
                if(_model.HasValue("regionName") && !string.IsNullOrWhiteSpace(_model.GetValue<string>("regionName")))
                    return JsonConvert.DeserializeObject<IEnumerable<NuPickersSqlDropDownPicker>>(_model.GetValue<string>("regionName")).FirstOrDefault();

                return null;
            }
        }
        #endregion

        #region Zgłoszenia przejazdu osób niepełnosprawnych
        /// <summary>
        /// Zgłoszenia przejazdu osób niepełnosprawnych - telefon
        /// </summary>
        public string ApplicationPersonPhone { get { return _model.GetValue<string>("applicationPerson-phoneNumber"); } }
        /// <summary>
        /// Zgłoszenia przejazdu osób niepełnosprawnych - dodatkowe info
        /// </summary>
        public IHtmlString ApplicationPersonAdditionalInfo { get { return _model.GetValue<IHtmlString>("applicationPerson-additionalInfo"); } }
        /// <summary>
        /// Zgłoszenia przejazdu osób niepełnosprawnych - email
        /// </summary>
        public string ApplicationPersonEmail { get { return _model.GetValue<string>("applicationPerson-Email"); } }
        #endregion

        #region Reklamacje, skargi i wnioski
        /// <summary>
        /// Reklamacje, skargi i wnioski - telefon
        /// </summary>
        public string ComplaintPhone { get { return _model.GetValue<string>("complaint-PhoneNumber"); } }
        /// <summary>
        /// Reklamacje, skargi i wnioski - email
        /// </summary>
        public string ComplaintEmail { get { return _model.GetValue<string>("complaint-Email"); } }
        #endregion

        #region Informacje o organizacji przejazdów grupowych
        /// <summary>
        /// Informacje o organizacji przejazdów grupowych - telefon
        /// </summary>
        public string GroupTripsPhone { get { return _model.GetValue<string>("groupTrips-PhoneNumber"); } }
        /// <summary>
        /// Informacje o organizacji przejazdów grupowych - email
        /// </summary>
        public string GroupTripsEmail { get { return _model.GetValue<string>("groupTrips-Email"); } }
        #endregion

        #region Informacje o utrudnieniach
        /// <summary>
        /// Informacje o utrudnieniach - tytuł
        /// </summary>
        public IHtmlString TrafficInformationTitle { get { return _model.GetValue<IHtmlString>("trafficInformation-Title"); } }
        /// <summary>
        /// Informacje o utrudnieniach - text
        /// </summary>
        public IHtmlString TrafficInformationText { get { return _model.GetValue<IHtmlString>("trafficInformation-Text"); } }
        #endregion

        #region Kontakt dla mediów
        /// <summary>
        /// Kontakt dla mediów - telefon
        /// </summary>
        public string MediaContactPhone { get { return _model.GetValue<string>("mediaContact-PhoneNumber"); } }
        /// <summary>
        /// Kontakt dla mediów - email
        /// </summary>
        public string MediaContactEmail { get { return _model.GetValue<string>("mediaContact-Email"); } }
        #endregion
    }
}
