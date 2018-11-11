using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Erp.Domain.Staff.Entities
{
    public class vwWelfareProgramsDetail
    {
        public vwWelfareProgramsDetail()
        {
            
        }

        public int Id { get; set; }
        public Nullable<bool> IsDeleted { get; set; }
        public Nullable<int> CreatedUserId { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public Nullable<int> ModifiedUserId { get; set; }
        public Nullable<System.DateTime> ModifiedDate { get; set; }
        public Nullable<int> AssignedUserId { get; set; }

        public Nullable<int> WelfareProgramsId { get; set; }
        public Nullable<int> StaffId { get; set; }
        public Nullable<System.DateTime> RegistrationDate { get; set; }
        public string Note { get; set; }
        public string StaffName { get; set; }
        public string BranchName { get; set; }
        public string Phone { get; set; }
        public string PositionName { get; set; }
        public string StaffCode { get; set; }
    }
}
