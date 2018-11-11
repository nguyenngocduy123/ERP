using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Erp.Domain.Staff.Entities.Mapping
{
    public class WorkSchedulesMap : EntityTypeConfiguration<WorkSchedules>
    {
        public WorkSchedulesMap()
        {
            // Primary Key
            this.HasKey(t => t.Id);

            // Properties
            

            // Table & Column Mappings
            this.ToTable("Staff_WorkSchedules");
            this.Property(t => t.Id).HasColumnName("Id");
            this.Property(t => t.CreatedUserId).HasColumnName("CreatedUserId");
            this.Property(t => t.CreatedDate).HasColumnName("CreatedDate");
            this.Property(t => t.ModifiedUserId).HasColumnName("ModifiedUserId");
            this.Property(t => t.ModifiedDate).HasColumnName("ModifiedDate");

            this.Property(t => t.StaffId).HasColumnName("StaffId");
            this.Property(t => t.Day).HasColumnName("Day");
            this.Property(t => t.ShiftsId).HasColumnName("ShiftsId");

            this.Property(t => t.HoursIn).HasColumnName("HoursIn");
            this.Property(t => t.HoursOut).HasColumnName("HoursOut");
            this.Property(t => t.Symbol).HasColumnName("Symbol");
            this.Property(t => t.Total_minute_work_late).HasColumnName("Total_minute_work_late");
            this.Property(t => t.Total_minute_work_early).HasColumnName("Total_minute_work_early");
            this.Property(t => t.Total_minute_work_overtime).HasColumnName("Total_minute_work_overtime");
            this.Property(t => t.Total_minute_work).HasColumnName("Total_minute_work");
            this.Property(t => t.DayOffId).HasColumnName("DayOffId");
            this.Property(t => t.TimekeepingListId).HasColumnName("TimekeepingListId");
            this.Property(t => t.FPMachineId).HasColumnName("FPMachineId");
        }
    }
}
