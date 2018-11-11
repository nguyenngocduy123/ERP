using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Erp.Domain.Staff.Entities
{
    public class WorkSchedules
    {
        public WorkSchedules()
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
        public Nullable<System.DateTime> HoursIn { get; set; }
        public Nullable<System.DateTime> HoursOut { get; set; }
        public Nullable<int> Symbol { get; set; }
        public Nullable<int> Total_minute_work_late { get; set; }
        public Nullable<int> Total_minute_work_early { get; set; }
        public Nullable<int> Total_minute_work_overtime { get; set; }
        public Nullable<int> Total_minute_work { get; set; }
        public Nullable<int> DayOffId { get; set; }
        public Nullable<int> TimekeepingListId { get; set; }
        public Nullable<int> FPMachineId { get; set; }
    }
}
