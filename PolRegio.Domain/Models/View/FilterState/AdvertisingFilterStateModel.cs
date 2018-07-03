using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PolRegio.Domain.Models.View.FilterState
{
    public class AdvertisingFilterStateModel
    {
        public int SelectedAdministrativeId { get; set; }
        public int SelectedStatusId { get; set; }
        public string Name { get; set; }
        public string Number { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
    }
}
