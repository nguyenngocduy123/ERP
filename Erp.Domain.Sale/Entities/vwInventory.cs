using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Erp.Domain.Sale.Entities
{
    public class vwInventory
    {
        public vwInventory()
        {
            
        }

        public int WarehouseId { get; set; }
        public string WarehouseName { get; set; }
        public int ProductId { get; set; }
        public int Quantity { get; set; }

        public string ProductGroup { get; set; }
        public string CategoryCode { get; set; }
        public string ProductName { get; set; }
        public decimal ProductPriceInbound { get; set; }
        public decimal ProductPriceOutbound { get; set; }
        public string ProductUnit { get; set; }
        public string ProductCode { get; set; }
        public int ProductMinInventory { get; set; }
        public int? BranchId { get; set; }
        public string BranchName { get; set; }
        public string Image_Name { get; set; }
        public int? CBTK { get; set; }
        public bool? IsSale { get; set; }
        public string LoCode { get; set; }
        public Nullable<System.DateTime> ExpiryDate { get; set; }
    }
}
