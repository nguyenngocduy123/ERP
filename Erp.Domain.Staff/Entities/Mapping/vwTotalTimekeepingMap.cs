using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Erp.Domain.Staff.Entities.Mapping
{
    public class vwTotalTimekeepingMap : EntityTypeConfiguration<vwTotalTimekeeping>
    {
        public vwTotalTimekeepingMap()
        {

            this.HasKey(t => t.Id);
            // Properties
            //this.Property(t => t.Name).HasMaxLength(150);
            

            // Table & Column Mappings
            this.ToTable("vwStaff_TotalTimekeeping");
            this.Property(t => t.Id).HasColumnName("Id");
            this.Property(t => t.Name).HasColumnName("Name");
            this.Property(t => t.Code).HasColumnName("Code");
            this.Property(t => t.BranchDepartmentId).HasColumnName("BranchDepartmentId");
            this.Property(t => t.Sale_BranchId).HasColumnName("Sale_BranchId");
            this.Property(t => t.Month).HasColumnName("MonthName");
            this.Property(t => t.Year).HasColumnName("YearName");
            this.Property(t => t.StaffId).HasColumnName("StaffId");
            this.Property(t => t.NgayCongThucTe).HasColumnName("Num_Day_Work");
            this.Property(t => t.TrongGioNgayNghi).HasColumnName("Total_hours_work_day_off");
            this.Property(t => t.TrongGioNgayLe).HasColumnName("Total_hours_work_day_holiday");
            this.Property(t => t.TangCaNgayLe).HasColumnName("Total_hours_work_overtime_holiday");
            this.Property(t => t.TangCaNgayNghi).HasColumnName("Total_hours_work_overtime_day_off");
            this.Property(t => t.TongGioTangCa).HasColumnName("Total_hours_work_overtime");
            this.Property(t => t.TongGioLamTheoCa).HasColumnName("Total_hours_work");
            this.Property(t => t.GioDiTre).HasColumnName("Total_hours_work_late");
            this.Property(t => t.GioVeSom).HasColumnName("Total_hours_work_early");
            this.Property(t => t.NgayNghiCoPhep).HasColumnName("Total_Day_Off");
            this.Property(t => t.TongNgayNghi).HasColumnName("Num_Day_Non_Work");
            this.Property(t => t.TimekeepingListId).HasColumnName("TimekeepingListId");
            //this.Property(t => t.SoNgayNghiBu).HasColumnName("MonthName");
            //this.Property(t => t.SoNgayNghiLe).HasColumnName("YearName");

        }
    }
}
