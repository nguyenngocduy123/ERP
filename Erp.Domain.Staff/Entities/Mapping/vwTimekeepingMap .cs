using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Erp.Domain.Staff.Entities.Mapping
{
    public class vwTimekeepingMap : EntityTypeConfiguration<vwTimekeeping>
    {
        public vwTimekeepingMap()
        {
            // Primary Key
            this.HasKey(t => t.Id);
            // Table & Column Mappings
            this.ToTable("vwStaff_Timekeeping");
            this.Property(t => t.Id).HasColumnName("Id");
            this.Property(t => t.TimekeepingId).HasColumnName("TimekeepingId");
            this.Property(t => t.WorkSchedulesId).HasColumnName("WorkSchedulesId");
            this.Property(t => t.StaffId).HasColumnName("StaffId");
            this.Property(t => t.HoursIn).HasColumnName("HoursIn");
            this.Property(t => t.HoursOut).HasColumnName("HoursOut");
            this.Property(t => t.DayWork).HasColumnName("DayWork");
            this.Property(t => t.ShiftsId).HasColumnName("ShiftsId");
            this.Property(t => t.StartTime).HasColumnName("StartTime");
            this.Property(t => t.EndTime).HasColumnName("EndTime");
            this.Property(t => t.NameShifts).HasColumnName("NameShifts");
            this.Property(t => t.CodeShifts).HasColumnName("CodeShifts");
            this.Property(t => t.Name).HasColumnName("Name");
            this.Property(t => t.Code).HasColumnName("Code");
            this.Property(t => t.CodeName).HasColumnName("CodeName");
            this.Property(t => t.ProfileImage).HasColumnName("ProfileImage");
            this.Property(t => t.BranchDepartmentId).HasColumnName("BranchDepartmentId");
            this.Property(t => t.Sale_BranchId).HasColumnName("Sale_BranchId");
            //this.Property(t => t.Pay).HasColumnName("Pay");

            this.Property(t => t.Birthday).HasColumnName("Birthday");
            this.Property(t => t.Gender).HasColumnName("Gender");
            this.Property(t => t.DayOff).HasColumnName("DayOff");
            this.Property(t => t.Month).HasColumnName("Month");
            this.Property(t => t.Year).HasColumnName("Year");

            this.Property(t => t.TotalMinuteWorkLate).HasColumnName("Total_minute_work_late");
            this.Property(t => t.TotalMinuteWorkEarly).HasColumnName("Total_minute_work_early");
            this.Property(t => t.TotalMinuteWork).HasColumnName("Total_minute_work");
            this.Property(t => t.TotalMinuteWorkOvertime).HasColumnName("Total_minute_work_overtime");
        }
    }
}
