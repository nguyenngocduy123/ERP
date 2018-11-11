using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Erp.Domain.Staff.Entities
{
    public class vwCheckInOut
    {
        public vwCheckInOut()
        {
            
        }

        public int Id { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public int UserId { get; set; }
        public Nullable<System.DateTime> TimeDate { get; set; }
        public Nullable<System.DateTime> TimeStr { get; set; }
        public string TimeType { get; set; }
        public string TimeSource { get; set; }
        public Nullable<int> MachineNo { get; set; }
        public string CardNo { get; set; }
        public string Name { get; set; }
        public Nullable<int> BranchDepartmentId { get; set; }
        public Nullable<int> Sale_BranchId { get; set; }
        public Nullable<int> StaffId { get; set; }
        public string Code { get; set; }
        public Nullable<int> FPMachineId { get; set; }
    }
}
