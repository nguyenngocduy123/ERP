using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Erp.BackOffice.Sale.Models
{
    public class InboundLocationItemsViewModel
    {
        public int? ProductInboundId { get; set; }

        public int? WarehouseId { get; set; }

        public List<WarehouseLocationItemViewModel> LocationItemList { get; set; }

    }
}