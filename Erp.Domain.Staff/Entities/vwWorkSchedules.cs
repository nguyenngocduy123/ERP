using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Erp.Domain.Staff.Entities
{
    public class vwWorkSchedules
    {
        public vwWorkSchedules()
        {
            
        }

        public int Id { get; set; }
        public Nullable<bool> IsDeleted { get; set; }
        public Nullable<int> CreatedUserId { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public Nullable<int> ModifiedUserId { get; set; }
        public Nullable<System.DateTime> ModifiedDate { get; set; }

        public Nullable<int> StaffId { get; set; }
        public Nullable<System.DateTime> Day { get; set; }
        public Nullable<int> ShiftsId { get; set; }
        public string Name { get; set; }

        public string Code { get; set; }
        public string NameShifts { get; set; }
        public string CodeShifts { get; set; }
        public string StartTime { get; set; }
        public string EndTime { get; set; }
        public string StartTimeOut { get; set; }
        public string StartTimeIn { get; set; }
        public string EndTimeIn { get; set; }
        public string EndTimeOut { get; set; }
        public Nullable<int> UserEnrollNumber { get; set; }

        public Nullable<System.DateTime> HoursIn { get; set; }
        public Nullable<System.DateTime> HoursOut { get; set; }
        public Nullable<int> Symbol { get; set; }
        public Nullable<int> Total_minute_work_late { get; set; }
        public Nullable<int> Total_minute_work_early { get; set; }
        public Nullable<int> Total_minute_work_overtime { get; set; }
        public Nullable<int> Total_minute_work { get; set; }
        public Nullable<int> BranchDepartmentId { get; set; }
        public Nullable<int> Sale_BranchId { get; set; }
        public Nullable<bool> Timekeeping { get; set; }
        public Nullable<bool> DayOff { get; set; }
        public Nullable<bool> Absent { get; set; }
        public Nullable<bool> NightShifts { get; set; }
        public string DayOffName { get; set; }
        public string DayOffCode { get; set; }
        public string Color { get; set; }
        public Nullable<int> Month { get; set; }
        public Nullable<int> Year { get; set; }
        public Nullable<int> DayOffId { get; set; }
        public Nullable<int> TimekeepingListId { get; set; }

        public string BranchName { get; set; }
        public Nullable<int> MinuteLate { get; set; }
        public Nullable<int> MinuteEarly { get; set; }
        public string ProfileImage { get; set; }
        public Nullable<int> FPMachineId { get; set; }
        public string Ten_may { get; set; }
    }
}
