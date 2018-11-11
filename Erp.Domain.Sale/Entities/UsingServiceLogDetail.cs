using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Erp.Domain.Sale.Entities
{
    public class UsingServiceLogDetail
    {
        public UsingServiceLogDetail()
        {
            
        }

        public int Id { get; set; }
        public Nullable<bool> IsDeleted { get; set; }
        public Nullable<int> CreatedUserId { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public Nullable<int> ModifiedUserId { get; set; }
        public Nullable<System.DateTime> ModifiedDate { get; set; }
        public Nullable<int> AssignedUserId { get; set; }

        public Nullable<int> UsingServiceId { get; set; }
        public Nullable<int> StaffId { get; set; }
        public string Type { get; set; }
        public string Status { get; set; }
        public Nullable<bool> IsVote { get; set; }
    }
}
