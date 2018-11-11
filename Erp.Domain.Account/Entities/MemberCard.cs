using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Erp.Domain.Account.Entities
{
    public class MemberCard
    {
        public MemberCard()
        {
            
        }

        public int Id { get; set; }
        public Nullable<bool> IsDeleted { get; set; }
        public Nullable<int> CreatedUserId { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public Nullable<int> ModifiedUserId { get; set; }
        public Nullable<System.DateTime> ModifiedDate { get; set; }
        public Nullable<int> AssignedUserId { get; set; }
        public Nullable<System.DateTime> DateOfIssue { get; set; }
        public Nullable<int> CustomerId { get; set; }
        public string Type { get; set; }
        public string Status { get; set; }
        public string Code { get; set; }
        public Nullable<int> BranchId { get; set; }

    }
}
