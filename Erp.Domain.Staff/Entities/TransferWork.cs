using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Erp.Domain.Staff.Entities
{
    public class TransferWork
    {
        public TransferWork()
        {
            
        }

        public int Id { get; set; }
        public Nullable<bool> IsDeleted { get; set; }
        public Nullable<int> CreatedUserId { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public Nullable<int> ModifiedUserId { get; set; }
        public Nullable<System.DateTime> ModifiedDate { get; set; }

        public Nullable<int> StaffId { get; set; }
        public Nullable<int> BranchDepartmentOldId { get; set; }
        public Nullable<int> BranchDepartmentNewId { get; set; }
        public string PositionOld { get; set; }
        public string PositionNew { get; set; }
        public Nullable<System.DateTime> DayDecision { get; set; }
        public Nullable<int> UserId { get; set; }
        public string Status { get; set; }
        public string Reason { get; set; }
        public Nullable<System.DateTime> DayEffective { get; set; }
        public string Code { get; set; }
        public string CodeStaffOld { get; set; }
        public string CodeStaffNew { get; set; }
    }
}
