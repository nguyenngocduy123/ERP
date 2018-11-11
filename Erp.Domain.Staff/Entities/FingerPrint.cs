using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Erp.Domain.Staff.Entities
{
    public class FingerPrint
    {
        public FingerPrint()
        {
            
        }

        public int Id { get; set; }
        public int? UserId { get; set; }
        public string Name { get; set; }
        public int? FingerIndex { get; set; }
        public string TmpData { get; set; }
        public int Privilege { get; set; }
        public string Password { get; set; }
        public bool Enabled { get; set; }
        public int? Flag { get; set; }
        public int? FPMachineId { get; set; }
        public DateTime? CreatedDate { get; set; }
        public DateTime? ModifiedDate { get; set; }
    }
}
