using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Erp.Domain.Sale.Entities
{
    public class CommisionStaff
    {
        public CommisionStaff()
        {
            
        }

        public int Id { get; set; }
        public Nullable<bool> IsDeleted { get; set; }
        public Nullable<int> CreatedUserId { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public Nullable<int> ModifiedUserId { get; set; }
        public Nullable<System.DateTime> ModifiedDate { get; set; }

        public int StaffId { get; set; }
        public int BranchId { get; set; }
        public Nullable<int> PercentOfCommision { get; set; }
        public decimal? AmountOfCommision { get; set; }
        public int InvoiceId { get; set; }
        public int InvoiceDetailId { get; set; }
        public string Note { get; set; }
        public string InvoiceType { get; set; }
        public bool IsResolved { get; set; }

    }
}
