using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PolRegio.Domain.Models.View.FilterState
{
    public class ContractsFilterStateModel : AdvertisingFilterStateModel
    {
        public int SelectedTypeOfContractId { get; set; }
        public int SelectedLawActId { get; set; }
    }
}
