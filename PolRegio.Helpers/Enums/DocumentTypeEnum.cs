namespace PolRegio.Helpers.Enums
{
    /// <summary>
    /// Enum z dokument type stworzonymi w CMS
    /// </summary>
    public enum DocumentTypeEnum
    {
        /// <summary>
        /// Główny dokument w strukturze CMS
        /// </summary>
        domain = 1,
        /// <summary>
        /// Dokument type zawierający lokalizację językową
        /// </summary>
        location = 2,
        /// <summary>
        /// Dokument type zawierający meta tagi strony
        /// Każda strona, która ma template powinna zawierać ten typ
        /// </summary>
        seo = 3,
        /// <summary>
        /// Dokument type zawierający 
        /// </summary>
        accordion = 4,
        /// <summary>
        /// Dokument type zawierający artykuł
        /// </summary>
        article = 5,
        /// <summary>
        /// Dokument type zawierający artykuł z możliwością sortowania po regionach
        /// </summary>
        articleWithDoubleFiltr = 6,
        /// <summary>
        /// Dokument type zawierający artykuł z możliwością sortowania po regionach oraz po kategorii artykułu
        /// </summary>
        articleWithFilter = 7,
        /// <summary>
        /// Dokument type zawierający 
        /// </summary>
        forBusiness = 8,
        /// <summary>
        /// Dokument type zawierający komunikat cookies
        /// </summary>
        ForTravelers = 9,
        /// <summary>
        /// Dokument type zawierający artykuły
        /// </summary>
        WhereBuyTicket = 10,
        /// <summary>
        /// Dokument type zawierający artykuły
        /// </summary>
        honoringTickets = 11,
        /// <summary>
        /// Dokument type zawierający artykuły możliwością sortowania po regionach oraz po kategorii artykułu
        /// </summary>
        Information = 12,
        /// <summary>
        /// Dokument type zawierający artykuł oraz akordeon
        /// </summary>
        Contact = 13,
        /// <summary>
        /// Dokument type zawierający treść strony mapa połączeń
        /// </summary>
        wiremap = 14,
        /// <summary>
        /// Dokument type zawierający 
        /// </summary>
        aboutUs = 15,
        /// <summary>
        /// Dokument type zawierający 
        /// </summary>
        CustomerService = 16,
        /// <summary>
        /// Dokument type zawierający 
        /// </summary>
        disabilitiesSupport = 17,
        /// <summary>
        /// Dokument type zawierający 
        /// </summary>
        OffersAndPromotions = 18,
        /// <summary>
        /// Dokument type zawierający 
        /// </summary>
        regionalOffers = 19,
        /// <summary>
        /// Dokument type zawierający 
        /// </summary>
        offersPromotions = 20,
        /// <summary>
        /// Dokument type zawierający 
        /// </summary>
        Help = 23,
        /// <summary>
        /// Dokument type zawierający 
        /// </summary>
        tendersAndAnnouncement = 24,
        /// <summary>
        /// Dokument type zawierający 
        /// </summary>
        Complaints = 25,
        /// <summary>
        /// Dokument type zawierający 
        /// </summary>
        TimetablesAndMapLinks = 26,
        /// <summary>
        /// Dokument type zawierający 
        /// </summary>
        search = 27,
        /// <summary>
        /// Dokument type zawierający 
        /// </summary>
        searchConnection = 28,
        /// <summary>
        /// Dokument type pozwalający wybrać czy dana sekcja pojawi się w munu czy w stopce strony
        /// </summary>
        selectMenuOrFooter = 29,
        /// <summary>
        /// Dokument type pozwalający dodać media społecznościowe do strony
        /// </summary>
        socialMedia = 30,
        /// <summary>
        /// Dokument type zawierający komunikat cookies
        /// </summary>
        cookies = 31,
        /// <summary>
        /// Dokument type zawierający kontaktu do oddziałów
        /// </summary>
        officeAccordion = 32,
        /// <summary>
        /// Dokument type zawierający stronę błędu 404
        /// </summary>
        error404 = 33,
        /// <summary>
        /// Dokument type zawierający stronę z ogłoszeniami o sprzedaży
        /// </summary>
        advertisingOfSales = 34,
        /// <summary>
        /// Dokument type zawierający stronę z ogłoszeniami o zamówieniach
        /// </summary>
        contractNotices = 35,
        /// <summary>
        /// Dokument type zawierający stronę z ogłoszeniem o sprzedaży
        /// </summary>
        announcementOfSale = 36,
        /// <summary>
        /// Dokument type zawierająćy dane podstawowe strony z listą elementów
        /// </summary>
        mainPageDataWithList = 37,
        /// <summary>
        /// Dokument type zawierający filtr z zamówień i sprzedaży
        /// </summary>
        noticesAndSaleFiltr = 38,
        /// <summary>
        /// Dokument type zawierający filtr z zamówień i sprzedaży
        /// </summary>
        noticesFiltr = 39,
        /// <summary>
        /// Dokument type zawierający filtr z zamówień i sprzedaży
        /// </summary>
        contractNotice = 40,
        /// <summary>
        /// Dokument type zawierający listę załączników 
        /// </summary>
        attachmentsControl = 41,
        /// <summary>
        /// Dokument type zawierający listę kas biletowych 
        /// </summary>
        ticketOffices = 42,
        /// <summary>
        /// Dokument type zawierający szczegóły kasy biletowej
        /// </summary>
        ticketOffice = 43,
        /// <summary>
        /// Dokument type zawierający stronę BIP
        /// </summary>
        bIP = 44,
        /// <summary>
        /// Dokument type zawierający stronę formularza BIP
        /// </summary>
        bIPForm = 45,
        /// <summary>
        /// Dokument type zawierający stronę artukułu BIP
        /// </summary>
        articleBip = 46,
        /// <summary>
        /// Dokument type zawierający listę artykułów a wybranym tagiem
        /// </summary>
        articleListWithTag = 47,

        login = 48,
        register = 49,
        account = 50
    }
}
