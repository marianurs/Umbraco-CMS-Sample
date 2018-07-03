using PolRegio.Domain.Models.ISS;
using PolRegio.Domain.Services.ISS;
using System;
using System.Collections.Generic;
using System.Linq;
using PolRegio.Helpers.Enums;
using PolRegio.Domain.Services.Config;
using PolRegio.Helpers.MD5;
using Newtonsoft.Json;
using PolRegio.Domain.Models.Database;
using PolRegio.Domain.Services.CacheService;
using PolRegio.Helpers.Constants;
using PolRegio.Domain.Models.View.SearchTicket;
using RestSharp;
using Umbraco.Core;
using Umbraco.Core.Persistence;
using Umbraco.Web;

namespace PolRegio.Services.ISS
{
    /// <summary>
    /// Klasa implementująca interface IISSApiService
    /// </summary>
    public class ISSApiService : IISSApiService
    {
        /// <summary>
        /// Obiekt typu IConfigService
        /// </summary>
        private readonly IConfigService _configService;
        private readonly IRegioApi _regioApi;

        /// <summary>
        /// Obiekt typu IConfigService
        /// </summary>
        private readonly ICacheService _cache;

        private DatabaseContext _dbContext;

        /// <summary>
        /// Konstruktor klasy
        /// </summary>
        public ISSApiService(IConfigService configService, ICacheService cache, IRegioApi regioApi)
        {
            _configService = configService;
            _cache = cache;
            _regioApi = regioApi;
            _dbContext = UmbracoContext.Current.Application.DatabaseContext;
        }

        /// <summary>
        /// Metoda zwracająca pobrane elementy z ISS Api jako Json
        /// </summary>
        /// <param name="type">typ elementu jaki chcemy pobrać</param>
        /// <returns>string zawierający Json</returns>
        public string GetAdditionalInformationForType()
        {
           var returnFromCache = _cache.Get(string.Concat(CacheVariables.iss_additional_info_api_result, ISSAdditionalDataEnum.station), 1440,
                () => JsonConvert.SerializeObject(GetStationsFromDB()));
            return returnFromCache;
        }

        public IEnumerable<T> GetAdditionalInformationForType<T>() where T : class
        {

            var returnFromCache = _cache.Get(string.Concat(CacheVariables.iss_additional_info_api_result, ISSAdditionalDataEnum.station), 1440,
                () => JsonConvert.SerializeObject(GetStationsFromDB()));
            return JsonConvert.DeserializeObject<IEnumerable<T>>(returnFromCache);
        }

        public void GetAnswerKeyToRedirectToBuyTicket(ref SearchTicketFormView model)
        {
            var client = new RestClient(_configService.Custom["iss_system_url"]);
            var request = new RestRequest(string.Format("redir/{0}", ISSFormTypeEnum.single), Method.POST);
            
            var cmpId = _configService.Custom["iss_system_cmp_id"]; //identyfikator firmy
            var cmpHash = _configService.Custom["iss_system_cmp_hash"];

            var dateFrom = model.Date.HasValue ? model.Date.Value.ToString("yyyy-MM-dd") : string.Empty; //data początkowego przejazdu

            var times = model.Time.HasValue ? model.Time.Value.ToString("HH:mm") : string.Empty; //czasy początkowych odjazdów dla każdego odcinka, oddzielone przecinkami

            var stations = string.Join(",",
                GetStationId(model.StartStation),
                GetStationId(model
                    .EndStation)); //lista identyfikatorów stacji, oddzielonych przecinkami. Zawsze będzie co najmniej stacja początkowa i końcowa.

            var sectionCount = "1"; //liczba odcinków. Zawsze co najmniej 1.

            var calculateHash =
                MD5Generator.CalculateMD5Hash(cmpId, cmpHash, dateFrom, sectionCount, times,
                    stations); //hash potwierdzający, że dane są rzeczywiście od podanej wyżej firmy

            request.AddParameter("cmp_id", cmpId);
            request.AddParameter("hash", calculateHash);
            request.AddParameter("kind_stid",
                "2"); //opcjonalny, ale zaleca się zawsze używać) – Typ identyfikatora stacji. Jeśli wartością jest 0 lub nie podano parametru, używa wartości domyślnej (hafas).
            request.AddParameter("stations", stations);
            request.AddParameter("section_count", sectionCount);
            request.AddParameter("date_from", dateFrom);
            request.AddParameter("times", times);
            
            model.AnswerKeyResponse = new ISSResponseModel(client.Execute(request).Content);

            if (!model.AnswerKeyResponse.IsError)
            {
                model.PolRegioRedirectUrl = string.Format("{0}redir?answer_key={1}", _configService.Custom["iss_system_url"], model.AnswerKeyResponse.ResponseMessage);
            }
        }

        #region PrivateMethods


        private List<ISSStationItem> GetStationsFromApi()
        {
            var client = new RestClient(_configService.Custom["iss_system_url"]);
            var request = new RestRequest(ISSAdditionalDataEnum.station.ToString(), Method.POST);

            var cmpId = _configService.Custom["iss_system_cmp_id"];
            var cmpHash = _configService.Custom["iss_system_cmp_hash"];
            var currentTime = DateTime.Now.ToString("yyyyMMddHHmmss");
            var calculateHash = MD5Generator.CalculateMD5Hash(cmpId, currentTime, cmpHash);

            request.AddParameter("cmp_id", cmpId);
            request.AddParameter("datetime", currentTime);
            request.AddParameter("hash", calculateHash);
            var response = client.Execute(request);

            return response.Content
                .Split('\n')
                .Select(s =>
                {
                    var split = s.Split('|');
                    return new ISSStationItem
                    {
                        stationId = int.Parse(split[0]),
                        value = split[1],
                    };
                })
                .ToList();
        }

        public void UpdateStationsInDB()
        {
            var issStations = GetStationsFromApi();
            var regioStations = _regioApi.GetStations();

            var stations = new List<StationDB>();

            #region mergeApis
            foreach (var issStationItem in issStations)
            {
                var regioStationItems = regioStations
                    .Where(x => x.objectType == "ST" || x.objectType == "PO")
                    .Where(x => string.Equals(x.objectShortName, issStationItem.value,
                        StringComparison.InvariantCultureIgnoreCase))
                    .ToArray();

                if (!regioStationItems.Any())
                {
                    //nie można znaleźć odpowiadającej stacji
                    stations.Add(new StationDB()
                    {
                        Id = issStationItem.stationId,
                        Name = issStationItem.value,
                    });
                }
                else
                {
                    if (regioStationItems.Length == 1)
                    {
                        stations.Add(new StationDB()
                        {
                            Id = issStationItem.stationId,
                            Name = issStationItem.value,
                            Latitude = regioStationItems[0].latitude,
                            Longitude = regioStationItems[0].longitude,
                        });
                    }
                    else
                    {
                        //Znaleziono kilka obiektów o tej nazwie
                        stations.Add(new StationDB()
                        {
                            Id = issStationItem.stationId,
                            Name = issStationItem.value,
                        });
                    }
                }
            }
            #endregion

            _dbContext.Database.Delete<StationDB>(new Sql().Select("*").From<StationDB>(_dbContext.SqlSyntax));
            _dbContext.Database.BulkInsertRecords(stations); 
        }

        public IEnumerable<ISSStationItem> GetStationsFromDB()
        {
            var sql = new Sql().Select("*").From<StationDB>(_dbContext.SqlSyntax);
            return _dbContext.Database
                .Fetch<StationDB>(sql)
                .Select(s => new ISSStationItem()
                {
                    stationId = s.Id,
                    value = s.Name,
                    longitude = s.Longitude,
                    latitude = s.Latitude,
                })
                .ToList();
        }

        /// <summary>
        /// Metoda zwracająca 
        /// </summary>
        /// <param name="stationName"></param>
        /// <returns></returns>
        private string GetStationId(string stationName)
        {
            var stationList = GetAdditionalInformationForType<ISSStationItem>();
            var stationFromList =
                stationList.FirstOrDefault(q => q.value.Trim().ToLower() == stationName.Trim().ToLower());
            if (stationFromList != null)
            {
                return stationFromList.stationId.ToString();
            }
            return string.Empty;
        }

        #endregion
    }
}