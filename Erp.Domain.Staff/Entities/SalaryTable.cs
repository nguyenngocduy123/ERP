using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Erp.Domain.Staff.Entities
{
    public class SalaryTable
    {
        public SalaryTable()
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

        public Nullable<int> TargetMonth { get; set; }
        public Nullable<int> TargetYear { get; set; }
       public Nullable<System.DateTime> PaymentDate { get; set; }
        public Nullable<int> SalarySettingId { get; set; }
        public string Status { get; set; }
        public byte[] ListSalarySettingDetail { get; set; }
        public int? BranchId { get; set; }
        public bool IsSend { get; set; }
        public bool Submitted { get; set; }
        public string SalaryApprovalType { get; set; }
        public decimal? TotalSalary { get; set; }
        public bool HiddenForMonth { get; set; }        
    }
}
