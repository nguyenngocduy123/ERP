using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Erp.Domain.Sale.Entities
{
    public class vwReportProduct
    {
        public int Id { get; set; }

        public bool? IsDeleted { get; set; }

        public DateTime? CreatedDate { get; set; }

        public int? CreatedUserId { get; set; }

        public DateTime? ModifiedDate { get; set; }

        public int? ModifiedUserId { get; set; }

        public int? AssignedUserId { get; set; }

        public string Code { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public string Unit { get; set; }

        public decimal? PriceInbound { get; set; }

        public decimal? PriceOutbound { get; set; }
        
        public string Type { get; set; }

        public string CategoryCode { get; set; }

        public int? MinInventory { get; set; }

        public string ServicesChild { get; set; }

        public bool? IsServicePackage { get; set; }
	

        public int? QtySaleOrder { get; set; }

        public int? TotalQtySaleOrder { get; set; }

        public int? QtyInvoice { get; set; }

        public int? TotalQtyInvoice { get; set; }
    }
}
