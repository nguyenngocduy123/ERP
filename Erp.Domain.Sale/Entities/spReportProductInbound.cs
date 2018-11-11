using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Erp.Domain.Sale.Entities
{
    public class spReportProductInbound
    {
        public int? TotalQty { get; set; }
        public decimal? TotalPrice { get; set; }

        public int ProductId { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public string Unit { get; set; }
        public decimal? PriceInbound { get; set; }
        public decimal? PriceOutbound { get; set; }
        public string Type { get; set; }
        public string CategoryCode { get; set; }
        public int? MinInventory { get; set; }
        public int? Parent_Id { get; set; }
    }
}
