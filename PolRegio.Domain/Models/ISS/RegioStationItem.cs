using System;

namespace PolRegio.Services.ISS
{
    public class RegioStationItem
    {
        public string PCS_country_id { get; set; }
        public int? PCS_enee_id { get; set; }
        public int PCS_object_id { get; set; }
        public int id { get; set; }
        public int id_webservice { get; set; }
        public double? latitude { get; set; }
        public double? longitude { get; set; }
        public bool objectFreightImportant { get; set; }
        public bool objectFreightTrainBegin { get; set; }
        public bool objectFreightTrainEnd { get; set; }
        public string objectGraphName { get; set; }
        public int objectHostBranchArea { get; set; }
        public int objectID { get; set; }
        public string objectLongName { get; set; }
        public bool objectMaintenanceAndRepairImportant { get; set; }
        public bool objectMaintenanceAndRepairTrainBegin { get; set; }
        public bool objectMaintenanceAndRepairTrainEnd { get; set; }
        public bool objectPassengerImportant { get; set; }
        public bool objectPassengerTrainBegin { get; set; }
        public bool objectPassengerTrainEnd { get; set; }
        public string objectShortName { get; set; }
        public string objectType { get; set; }
        public DateTime objectValidFrom { get; set; }
        public DateTime objectValidTo { get; set; }
    }
}