using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Erp.Domain.Staff.Entities
{
    public class vwTimekeeping
    {
        public vwTimekeeping()
        {

        }
        public int Id { get; set; }
        public Nullable<int> TimekeepingId { get; set; }
        public Nullable<int> WorkSchedulesId { get; set; }
        public Nullable<int> StaffId { get; set; }
        public Nullable<System.DateTime> HoursIn { get; set; }
        public Nullable<System.DateTime> HoursOut { get; set; }
        public string StartTime { get; set; }
        public string EndTime { get; set; }
        public string NameShifts { get; set; }
        public string CodeShifts { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
        public string ProfileImage { get; set; }
        public Nullable<int> BranchDepartmentId { get; set; }
        public Nullable<int> Sale_BranchId { get; set; }
        public Nullable<int> ShiftsId { get; set; }
        public Nullable<System.DateTime> DayWork { get; set; }
        public string CodeName { get; set; }
        //public Nullable<int> Pay { get; set; }
        public Nullable<System.DateTime> Birthday { get; set; }
        public Nullable<bool> Gender { get; set; }
        public string DayOff { get; set; }
        public Nullable<int> Month { get; set; }
        public Nullable<int> Year { get; set; }

        public Nullable<int> TotalMinuteWorkLate { get; set; }
        public Nullable<int> TotalMinuteWorkEarly { get; set; }
        public Nullable<int> TotalMinuteWork { get; set; }
        public Nullable<int> TotalMinuteWorkOvertime { get; set; }
    }
}
