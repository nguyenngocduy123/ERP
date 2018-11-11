using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Erp.Domain.Staff.Entities
{
    public class vwTransferWork
    {
        public vwTransferWork()
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
        public string Staff_DepartmentNew { get; set; }
        public string BranchNameNew { get; set; }
        public string Staff_DepartmentOld { get; set; }
        public string BranchNameOld { get; set; }
        public string CodeStaff { get; set; }
        public string NameStaff { get; set; }
        public string NameUser { get; set; }
        public Nullable<System.DateTime> Birthday { get; set; }
        public Nullable<bool> Gender { get; set; }
        public Nullable<int> BranchIdOld { get; set; }
        public Nullable<int> BranchIdNew { get; set; }
        public string CodeName { get; set; }
        public string ProfileImage { get; set; }
        public string PositionOldName { get; set; }
        public string PositionNewName { get; set; }
    }
}
