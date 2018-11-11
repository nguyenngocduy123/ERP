using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Erp.Domain.Staff.Entities
{
    public class CheckInOut
    {
        public CheckInOut()
        {
            
        }

        public int Id { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public int UserId { get; set; }
        public Nullable<System.DateTime> TimeDate { get; set; }
        public Nullable<System.DateTime> TimeStr { get; set; }
        public string TimeType { get; set; }
        public string TimeSource { get; set; }
        public int MachineNo { get; set; }
        public string CardNo { get; set; }
        public int FPMachineId { get; set; }
    }
}
