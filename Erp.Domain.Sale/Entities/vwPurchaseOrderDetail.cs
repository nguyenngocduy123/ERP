using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Erp.Domain.Sale.Entities
{
    public class vwPurchaseOrderDetail
    {
        public vwPurchaseOrderDetail()
        {
            
        }

        public int Id { get; set; }
        public Nullable<bool> IsDeleted { get; set; }
        public Nullable<int> CreatedUserId { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public Nullable<int> ModifiedUserId { get; set; }
        public Nullable<System.DateTime> ModifiedDate { get; set; }

        public Nullable<int> PurchaseOrderId { get; set; }
        public Nullable<int> ProductId { get; set; }
        public decimal? Price { get; set; }
        public Nullable<int> Quantity { get; set; }
        public string Unit { get; set; }
        public string ProductName { get; set; }
        public string ProductCode { get; set; }
        public string CategoryCode { get; set; }
        public string ProductGroup { get; set; }
        public string PurchaseOrderCode { get; set; }
        //public int? CustomerId { get; set; }
        public int? DisCount { get; set; }
        public int? DisCountAmount { get; set; }
        public bool IsArchive { get; set; }
        public System.DateTime PurchaseOrderDate { get; set; }
        public decimal Amount { get; set; }
        public int SupplierId { get; set; }
        public string SupplierName { get; set; }
        public string Manufacturer { get; set; }
        public string Description { get; set; }
    }
}
