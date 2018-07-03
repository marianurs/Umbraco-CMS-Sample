using System.Collections.Generic;
using PolRegio.Services.ISS;

namespace PolRegio.Domain.Services.ISS
{
    public interface IRegioApi
    {
        List<RegioStationItem> GetStations(int page, int pageSize);
        List<RegioStationItem> GetStations();
    }
}