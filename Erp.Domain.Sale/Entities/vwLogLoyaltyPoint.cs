using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Erp.Domain.Sale.Entities
{
    public class vwLogLoyaltyPoint
    {
        public vwLogLoyaltyPoint()
        {
            
        }

        public int Id { get; set; }
        public Nullable<bool> IsDeleted { get; set; }
        public Nullable<int> CreatedUserId { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public Nullable<int> ModifiedUserId { get; set; }
        public Nullable<System.DateTime> ModifiedDate { get; set; }
        public Nullable<int> AssignedUserId { get; set; }

        public Nullable<int> CustomerId { get; set; }
        public Nullable<int> ProductInvoiceId { get; set; }
        public Nullable<int> PlusPoint { get; set; }
        public Nullable<int> TotalPoint { get; set; }
        //public Nullable<int> MemberCardId { get; set; }
        public string ProductInvoiceCode { get; set; }
        public string ProductInvoiceDate { get; set; }
        //public string MemberCardCode { get; set; }
        public decimal? TotalAmount { get; set; }
    }
}
