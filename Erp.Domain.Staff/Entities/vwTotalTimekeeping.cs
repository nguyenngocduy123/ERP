using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Erp.Domain.Staff.Entities
{
    public class vwTotalTimekeeping
    {
        public vwTotalTimekeeping()
        {
            
        }
        public int Id { get; set; }
        public Nullable<int> NgayCongThucTe { get; set; }
        public Nullable<int> TongGioLamTheoCa { get; set; }
        public Nullable<int> GioDiTre { get; set; }
        public Nullable<int> GioVeSom { get; set; }
        public Nullable<int> TongNgayNghi { get; set; }
        public Nullable<int> NgayNghiCoPhep { get; set; }
        public Nullable<int> Month { get; set; }
        public Nullable<int> Year { get; set; }
        public Nullable<int> StaffId { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
        public Nullable<int> BranchDepartmentId { get; set; }
        public Nullable<int> Sale_BranchId { get; set; }
        public Nullable<int> TangCaNgayNghi { get; set; }
        public Nullable<int> TangCaNgayLe { get; set; }
        public Nullable<int> TongGioTangCa { get; set; }
        public Nullable<int> TimekeepingListId { get; set; }
        //public Nullable<int> SoNgayNghiBu { get; set; }
        //public Nullable<int> SoNgayNghiLe { get; set; }
        public Nullable<int> TrongGioNgayNghi { get; set; }
        public Nullable<int> TrongGioNgayLe { get; set; }
    }
}
