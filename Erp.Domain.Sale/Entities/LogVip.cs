using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Erp.Domain.Sale.Entities
{
    public class LogVip
    {
        public LogVip()
        {
            
        }

        public int Id { get; set; }
        public Nullable<bool> IsDeleted { get; set; }
        public Nullable<int> CreatedUserId { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public Nullable<int> ModifiedUserId { get; set; }
        public Nullable<System.DateTime> ModifiedDate { get; set; }
        public Nullable<int> AssignedUserId { get; set; }
        public string Name { get; set; }

        public Nullable<int> CustomerId { get; set; }
        public string Status { get; set; }
        public string Ratings { get; set; }
        public Nullable<int> ApprovedUserId { get; set; }
        public Nullable<System.DateTime> ApprovedDate { get; set; }
        public decimal? TotalAmount { get; set; }

    }
}
