using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using PolRegio.Domain.Services.Config;
using PolRegio.Domain.Services.ISS;
using RestSharp;

namespace PolRegio.Services.ISS
{
    public class RegioApi : IRegioApi
    {
        private readonly IConfigService _configService;

        public RegioApi(IConfigService configService)
        {
            _configService = configService;
        }


        public List<RegioStationItem> GetStations(int page, int pageSize)
        {
            var client = new RestClient(_configService.Custom["mregioapi_url"]);
            var request = new RestRequest(string.Format("/Timetable/GetObjectsNamesDict/{1}/{0}", page, pageSize),
                Method.GET);

            return client.Execute<List<RegioStationItem>>(request).Data;
        }

        public List<RegioStationItem> GetStations()
        {
            var list = new List<RegioStationItem>();
            int i = 0;
            while (true)
            {
                var stations = GetStations(i++, 1000);
                if (!stations.Any())
                    break;
                list.AddRange(stations);
            }

            return list;
        }
    }
}