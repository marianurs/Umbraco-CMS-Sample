﻿using PolRegio.Domain.Models.Components;
using PolRegio.Domain.Models.Components.Notice;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PolRegio.Domain.Models.View.ContractNoticeModel
{
    /// <summary>
    /// Klasa zawierająca obiekty wyświetlane na stronie ze wszystkimi zamówieniami publicznymi
    /// </summary>
    public class ContractNoticesViewModel
    {
        /// <summary>
        /// Data rozpoczęcia postępowania od - użyta przy filtrowaniu
        /// </summary>
        public DateTime? StartDate { get; set; }
        /// <summary>
        /// Data rozpoczęcia postępowania do - użyta przy filtrowaniu
        /// </summary>
        public DateTime? EndDate { get; set; }
        /// <summary>
        /// Enumerator zawierający filtr jednostki administracyjnej
        /// </summary>
        public List<FilterItem> AdministrativeFilter { get; set; }
        /// <summary>
        /// Id wybranej jednostki administracyjnej
        /// </summary>
        public int SelectedAdministrativeId { get; set; }
        /// <summary>
        /// Enumerator zawierający filtr statusu postępowania
        /// </summary>
        public List<FilterItem> StatusFilter { get; set; }
        /// <summary>
        /// Id wybranego statusu obiektu
        /// </summary>
        public int SelectedStatusId { get; set; }
        /// <summary>
        /// Enumerator zawierający filtr statusu postępowania
        /// </summary>
        public List<FilterItem> TypeOfContractFilter { get; set; }
        /// <summary>
        /// Id wybranego statusu obiektu
        /// </summary>
        public int SelectedTypeOfContractId { get; set; }
        /// <summary>
        /// Enumerator zawierający filtr statusu postępowania
        /// </summary>
        public List<FilterItem> LawActFilter { get; set; }
        /// <summary>
        /// Id wybranego statusu obiektu
        /// </summary>
        public int SelectedLawActId { get; set; }
        /// <summary>
        /// Nazwa szukanego postępowania
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// Number szukanego postępowania
        /// </summary>
        public string Number { get; set; }
        /// <summary>
        /// Lista obiektów do wyświetlenia jako zamówienie
        /// </summary>
        public IEnumerable<NoticeBoxModel> NoticesList { get; set; }

        /// <summary>
        /// Liczba wszystkich dostępnych postępowań do wyświetlenia
        /// </summary>
        public int AllNewsCount { get; set; }
        /// <summary>
        /// Liczba wyświetlanych elementów po LoadMore lub po zmianie filtrów
        /// </summary>
        public int DisplayCount { get; set; }
        /// <summary>
        /// Aktualne id strony
        /// </summary>
        public int CurrentUmbracoPageId { get; set; }
        /// <summary>
        /// Culture info obecnej strony
        /// </summary>
        public CultureInfo CurrentPageCulture { get; set; }
        /// <summary>
        /// Adres url do ofert archiwalnych
        /// </summary>
        public string ArchiveUrl { get; set; }
    }
}
