using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Erp.Domain.Staff.Entities
{
    public class vwDayOff
    {
        public vwDayOff()
        {
            
        }

        public int Id { get; set; }
        public Nullable<bool> IsDeleted { get; set; }
        public Nullable<int> CreatedUserId { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public Nullable<int> ModifiedUserId { get; set; }
        public Nullable<System.DateTime> ModifiedDate { get; set; }
        public string NameSymbol { get; set; }

        public Nullable<System.DateTime> DayStart { get; set; }
        public Nullable<System.DateTime> DayEnd { get; set; }
        public Nullable<int> Quantity { get; set; }
        public Nullable<int> TypeDayOffId { get; set; }
        public Nullable<int> QuantityNotUsed { get; set; }
        public Nullable<int> StaffId { get; set; }
        public Nullable<int> TypeDayOffQuantity { get; set; }
        public string CodeSymbol { get; set; }
        public string Code { get; set; }
        public string NameStaff { get; set; }
        public string CodeStaff { get; set; }
        public Nullable<int> Sale_BranchId { get; set; }
        public Nullable<int> BranchDepartmentId { get; set; }
        public string BranchName { get; set; }
        public string DepartmentName { get; set; }
        public string ProfileImage { get; set; }
    }
}
