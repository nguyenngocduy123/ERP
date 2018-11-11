using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Erp.Domain.Staff.Entities
{
    public class Shifts
    {
        public Shifts()
        {
            
        }

        public int Id { get; set; }
        public Nullable<bool> IsDeleted { get; set; }
        public Nullable<int> CreatedUserId { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public Nullable<int> ModifiedUserId { get; set; }
        public Nullable<System.DateTime> ModifiedDate { get; set; }
        public string Name { get; set; }

        public string Code { get; set; }
        public string StartTimeIn { get; set; }
        public string StartTime { get; set; }
        public string StartTimeOut { get; set; }
        public string EndTimeIn { get; set; }
        public string EndTime { get; set; }
        public string EndTimeOut { get; set; }
        public Nullable<bool> NightShifts { get; set; }
        public string CategoryShifts { get; set; }
        public Nullable<int> MinuteLate { get; set; }
        public Nullable<int> MinuteEarly { get; set; }
    }
}
