using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Erp.Domain.Staff.Entities
{
    public class FPMachine
    {
        public FPMachine()
        {
            
        }

        public int Id { get; set; }
        public Nullable<bool> IsDeleted { get; set; }
        public Nullable<int> CreatedUserId { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public Nullable<int> ModifiedUserId { get; set; }
        public Nullable<System.DateTime> ModifiedDate { get; set; }
        public Nullable<int> AssignedUserId { get; set; }
        public Nullable<System.DateTime> UpdatedDate { get; set; }

        public string Ten_may_tinh { get; set; }
        public string Ten_may { get; set; }
        public string Loai_ket_noi { get; set; }
        public Nullable<int> Ma_loai_ket_noi { get; set; }
        public string ID_Ket_noi_COM { get; set; }
        public string ID_Ket_noi_IP { get; set; }
        public Nullable<int> Cong_COM { get; set; }
        public string Dia_chi_IP { get; set; }
        public string Toc_do_truyen { get; set; }
        public Nullable<int> Port { get; set; }
        public Nullable<int> Loaimay { get; set; }
        public Nullable<int> Passwd { get; set; }
        public string url { get; set; }
        public bool? useurl { get; set; }
        public int? AutoID { get; set; }
        public string GetDataSchedule { get; set; }
        public Nullable<int> BranchId { get; set; }

        public string TeamviewerID { get; set; }
        public string TeamviewerPassword { get; set; }
        public string Note { get; set; }
    }
}
