using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Erp.Domain.Sale.Entities
{
    public class vwRequestInboundDetail
    {
        public vwRequestInboundDetail()
        {
            
        }

        public int Id { get; set; }
        public Nullable<bool> IsDeleted { get; set; }
        public Nullable<int> CreatedUserId { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public Nullable<int> ModifiedUserId { get; set; }
        public Nullable<System.DateTime> ModifiedDate { get; set; }
        public Nullable<int> AssignedUserId { get; set; }

        public Nullable<int> RequestInboundId { get; set; }
        public Nullable<int> ProductId { get; set; }
        public Nullable<int> Quantity { get; set; }
        public decimal? Price { get; set; }
        public string Unit { get; set; }
        public Nullable<int> QuantityRemaining { get; set; }
        public string ProductName { get; set; }
        public string ProductCode { get; set; }
        public string ProductBarCode { get; set; }
        public string CategoryCode { get; set; }
        public string ProductGroup { get; set; }
        public string ProductGroupName { get; set; }
        public string Manufacturer { get; set; }
        public string Image_Name { get; set; }
    }
}
