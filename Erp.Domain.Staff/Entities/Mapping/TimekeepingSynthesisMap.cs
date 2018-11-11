using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Erp.Domain.Staff.Entities.Mapping
{
    public class TimekeepingSynthesisMap : EntityTypeConfiguration<TimekeepingSynthesis>
    {
        public TimekeepingSynthesisMap()
        {
            // Primary Key
            this.HasKey(t => t.Id);

            // Properties

            

            // Table & Column Mappings
            this.ToTable("Staff_TimekeepingSynthesis");
            this.Property(t => t.Id).HasColumnName("Id");
            this.Property(t => t.CreatedUserId).HasColumnName("CreatedUserId");
            this.Property(t => t.CreatedDate).HasColumnName("CreatedDate");
            this.Property(t => t.ModifiedUserId).HasColumnName("ModifiedUserId");
            this.Property(t => t.ModifiedDate).HasColumnName("ModifiedDate");
            this.Property(t => t.AssignedUserId).HasColumnName("AssignedUserId");

            this.Property(t => t.StaffId).HasColumnName("StaffId");
            this.Property(t => t.Month).HasColumnName("Month");
            this.Property(t => t.Year).HasColumnName("Year");
            this.Property(t => t.NgayCongThucTe).HasColumnName("NgayCongThucTe");
            this.Property(t => t.NgayNghiCoPhep).HasColumnName("NgayNghiCoPhep");
            this.Property(t => t.SoNgayNghiBu).HasColumnName("SoNgayNghiBu");
            this.Property(t => t.SoNgayNghiLe).HasColumnName("SoNgayNghiLe");
            this.Property(t => t.TrongGioNgayThuong).HasColumnName("TrongGioNgayThuong");
            this.Property(t => t.TangCaNgayThuong).HasColumnName("TangCaNgayThuong");
            this.Property(t => t.TrongGioNgayNghi).HasColumnName("TrongGioNgayNghi");
            this.Property(t => t.TangCaNgayNghi).HasColumnName("TangCaNgayNghi");
            this.Property(t => t.TrongGioNgayLe).HasColumnName("TrongGioNgayLe");
            this.Property(t => t.TangCaNgayLe).HasColumnName("TangCaNgayLe");
            this.Property(t => t.GioDiTre).HasColumnName("GioDiTre");
            this.Property(t => t.GioVeSom).HasColumnName("GioVeSom");
            this.Property(t => t.GioCaDem).HasColumnName("GioCaDem");
            this.Property(t => t.TimekeepingListId).HasColumnName("TimekeepingListId");
            this.Property(t => t.NgayDiTre).HasColumnName("NgayDiTre");
            this.Property(t => t.NgayVeSom).HasColumnName("NgayVeSom");
        }
    }
}
