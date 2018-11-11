using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Erp.Domain.Staff.Entities
{
    public class TimekeepingSynthesis
    {
        public TimekeepingSynthesis()
        {
            
        }

        public int Id { get; set; }
        public Nullable<bool> IsDeleted { get; set; }
        public Nullable<int> CreatedUserId { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public Nullable<int> ModifiedUserId { get; set; }
        public Nullable<System.DateTime> ModifiedDate { get; set; }
        public Nullable<int> AssignedUserId { get; set; }

        public Nullable<int> StaffId { get; set; }
        public Nullable<int> Month { get; set; }
        public Nullable<int> Year { get; set; }
        public Nullable<int> NgayCongThucTe { get; set; }
        public Nullable<int> NgayNghiCoPhep { get; set; }
        public Nullable<int> SoNgayNghiBu { get; set; }
        public Nullable<int> SoNgayNghiLe { get; set; }
        public Nullable<double> TrongGioNgayThuong { get; set; }
        public Nullable<double> TangCaNgayThuong { get; set; }
        public Nullable<double> TrongGioNgayNghi { get; set; }
        public Nullable<double> TangCaNgayNghi { get; set; }
        public Nullable<double> TrongGioNgayLe { get; set; }
        public Nullable<double> TangCaNgayLe { get; set; }
        public Nullable<double> GioDiTre { get; set; }
        public Nullable<double> GioVeSom { get; set; }
        public Nullable<double> GioCaDem { get; set; }
        public Nullable<int> TimekeepingListId { get; set; }
        public Nullable<int> NgayDiTre { get; set; }
        public Nullable<int> NgayVeSom { get; set; }
    }
}
