using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Erp.Domain.Staff.Entities.Mapping
{
    public class vwCheckInOutMap : EntityTypeConfiguration<vwCheckInOut>
    {
        public vwCheckInOutMap()
        {
            // Primary Key
            this.HasKey(t => t.Id);

            // Properties
            this.Property(t => t.TimeType).HasMaxLength(5);
            this.Property(t => t.TimeSource).HasMaxLength(2);
            this.Property(t => t.CardNo).HasMaxLength(30);


            // Table & Column Mappings
            this.ToTable("vwStaff_CheckInOut");

            this.Property(t => t.Id).HasColumnName("Id");
            this.Property(t => t.CreatedDate).HasColumnName("CreatedDate");
            this.Property(t => t.UserId).HasColumnName("UserId");
            this.Property(t => t.TimeDate).HasColumnName("TimeDate");
            this.Property(t => t.TimeStr).HasColumnName("TimeStr");
            this.Property(t => t.TimeType).HasColumnName("TimeType");
            this.Property(t => t.TimeSource).HasColumnName("TimeSource");
            this.Property(t => t.MachineNo).HasColumnName("MachineNo");
            this.Property(t => t.CardNo).HasColumnName("CardNo");

            this.Property(t => t.Name).HasColumnName("Name");
            this.Property(t => t.BranchDepartmentId).HasColumnName("BranchDepartmentId");
            this.Property(t => t.Sale_BranchId).HasColumnName("Sale_BranchId");
            this.Property(t => t.StaffId).HasColumnName("StaffId");
            this.Property(t => t.Code).HasColumnName("Code");
            this.Property(t => t.FPMachineId).HasColumnName("FPMachineId");
        }
    }
}
