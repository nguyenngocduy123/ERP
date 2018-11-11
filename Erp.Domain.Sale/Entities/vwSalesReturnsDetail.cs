using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Erp.Domain.Sale.Entities
{
    public class vwSalesReturnsDetail
    {
        public vwSalesReturnsDetail()
        {
            
        }

        public int Id { get; set; }
        public Nullable<bool> IsDeleted { get; set; }
        public Nullable<int> CreatedUserId { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public Nullable<int> ModifiedUserId { get; set; }
        public Nullable<System.DateTime> ModifiedDate { get; set; }

        public Nullable<int> SalesReturnsId { get; set; }
        public Nullable<int> ProductId { get; set; }
        public decimal? Price { get; set; }
        public Nullable<int> Quantity { get; set; }
        public string UnitProduct { get; set; }
        public string ProductName { get; set; }
        public string ProductCode { get; set; }
        //public int? PromotionId { get; set; }
        //public int? PromotionDetailId { get; set; }
        //public double? PromotionValue { get; set; }
        public string ProductGroup { get; set; }
        public int? DisCount { get; set; }
        public int? DisCountAmount { get; set; }
        public Nullable<bool> CheckPromotion { get; set; }

        public Nullable<System.DateTime> SalesReturnDate { get; set; }
        public Nullable<int> ProductInvoiceId { get; set; }
        public Nullable<int> ProductInvoiceDetailId { get; set; }
        public string ProductInvoiceCode { get; set; }
        public string SalerInvoiceName { get; set; }
        public Nullable<System.DateTime>  ProductInvoiceDate{ get; set; }
    }
}
