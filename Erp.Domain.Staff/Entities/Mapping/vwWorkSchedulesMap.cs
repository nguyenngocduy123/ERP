using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Erp.Domain.Staff.Entities.Mapping
{
    public class vwWorkSchedulesMap : EntityTypeConfiguration<vwWorkSchedules>
    {
        public vwWorkSchedulesMap()
        {
            // Primary Key
            this.HasKey(t => t.Id);

            // Properties
            

            // Table & Column Mappings
            this.ToTable("vwStaff_WorkSchedules");
            this.Property(t => t.Id).HasColumnName("Id");
            this.Property(t => t.CreatedUserId).HasColumnName("CreatedUserId");
            this.Property(t => t.CreatedDate).HasColumnName("CreatedDate");
            this.Property(t => t.ModifiedUserId).HasColumnName("ModifiedUserId");
            this.Property(t => t.ModifiedDate).HasColumnName("ModifiedDate");

            this.Property(t => t.StaffId).HasColumnName("StaffId");
            this.Property(t => t.Day).HasColumnName("Day");
            this.Property(t => t.ShiftsId).HasColumnName("ShiftsId");
            this.Property(t => t.Name).HasColumnName("Name");
            this.Property(t => t.Code).HasColumnName("Code");
            this.Property(t => t.CodeShifts).HasColumnName("CodeShifts");
            this.Property(t => t.NameShifts).HasColumnName("NameShifts");
            this.Property(t => t.StartTime).HasColumnName("StartTime");
            this.Property(t => t.EndTime).HasColumnName("EndTime");
            this.Property(t => t.StartTimeOut).HasColumnName("StartTimeOut");
            this.Property(t => t.EndTimeIn).HasColumnName("EndTimeIn");
            this.Property(t => t.UserEnrollNumber).HasColumnName("UserEnrollNumber");

            this.Property(t => t.HoursIn).HasColumnName("HoursIn");
            this.Property(t => t.HoursOut).HasColumnName("HoursOut");
            this.Property(t => t.Symbol).HasColumnName("Symbol");
            this.Property(t => t.Total_minute_work_late).HasColumnName("Total_minute_work_late");
            this.Property(t => t.Total_minute_work_early).HasColumnName("Total_minute_work_early");
            this.Property(t => t.Total_minute_work_overtime).HasColumnName("Total_minute_work_overtime");
            this.Property(t => t.Total_minute_work).HasColumnName("Total_minute_work");

            this.Property(t => t.BranchDepartmentId).HasColumnName("BranchDepartmentId");
            this.Property(t => t.Sale_BranchId).HasColumnName("Sale_BranchId");
            this.Property(t => t.Timekeeping).HasColumnName("Timekeeping");
            this.Property(t => t.DayOff).HasColumnName("DayOff");
            this.Property(t => t.DayOffName).HasColumnName("DayOffName");
            this.Property(t => t.DayOffCode).HasColumnName("DayOffCode");
            this.Property(t => t.Color).HasColumnName("Color");
            this.Property(t => t.Month).HasColumnName("Month");
            this.Property(t => t.Year).HasColumnName("Year");
            this.Property(t => t.DayOffId).HasColumnName("DayOffId");
            this.Property(t => t.TimekeepingListId).HasColumnName("TimekeepingListId");
            this.Property(t => t.Absent).HasColumnName("Absent");
            this.Property(t => t.NightShifts).HasColumnName("NightShifts");
            this.Property(t => t.StartTimeIn).HasColumnName("StartTimeIn");
            this.Property(t => t.EndTimeOut).HasColumnName("EndTimeOut");
            this.Property(t => t.BranchName).HasColumnName("BranchName");
            this.Property(t => t.MinuteEarly).HasColumnName("MinuteEarly");
            this.Property(t => t.MinuteLate).HasColumnName("MinuteLate");
            this.Property(t => t.ProfileImage).HasColumnName("ProfileImage");
            this.Property(t => t.FPMachineId).HasColumnName("FPMachineId");
            this.Property(t => t.Ten_may).HasColumnName("Ten_may");
        }
    }
}
