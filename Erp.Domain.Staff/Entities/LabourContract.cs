using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Erp.Domain.Staff.Entities
{
    public class LabourContract
    {
        public LabourContract()
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

        public Nullable<System.DateTime> SignedDay { get; set; }
        public Nullable<int> StaffId { get; set; }
        public Nullable<int> Type { get; set; }
        public Nullable<System.DateTime> EffectiveDate { get; set; }
        public Nullable<System.DateTime> ExpiryDate { get; set; }
        public string Status { get; set; }
        public string Content { get; set; }
        public Nullable<int> ApprovedUserId { get; set; }
        public Nullable<int> WageAgreement { get; set; }
        public string FormWork { get; set; }
        public string Job { get; set; }
        public string Code { get; set; }
        public string PositionStaff { get; set; }
        public string PositionApproved { get; set; }
        public Nullable<int> DepartmentStaffId { get; set; }
        public Nullable<int> DepartmentApprovedId { get; set; }

    }
}
