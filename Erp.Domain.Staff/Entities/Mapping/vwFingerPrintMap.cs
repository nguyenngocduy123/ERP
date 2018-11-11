using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Erp.Domain.Staff.Entities.Mapping
{
    public class vwFingerPrintMap : EntityTypeConfiguration<vwFingerPrint>
    {
        public vwFingerPrintMap()
        {
            // Primary Key
            this.HasKey(t => t.Id);

            // Table & Column Mappings
            this.ToTable("vwStaff_FingerPrint");
            this.Property(t => t.Id).HasColumnName("Id");
            this.Property(t => t.UserId).HasColumnName("UserId");
            this.Property(t => t.Name).HasColumnName("Name");
            this.Property(t => t.FingerIndex).HasColumnName("FingerIndex");
            this.Property(t => t.TmpData).HasColumnName("TmpData");
            this.Property(t => t.Privilege).HasColumnName("Privilege");
            this.Property(t => t.Password).HasColumnName("Password");
            this.Property(t => t.Enabled).HasColumnName("Enabled");
            this.Property(t => t.Flag).HasColumnName("Flag");
            this.Property(t => t.FPMachineId).HasColumnName("FPMachineId");
            this.Property(t => t.CreatedDate).HasColumnName("CreatedDate");
            this.Property(t => t.ModifiedDate).HasColumnName("ModifiedDate");

            this.Property(t => t.StaffCode).HasColumnName("StaffCode");
            this.Property(t => t.StaffName).HasColumnName("StaffName");
            this.Property(t => t.Ten_may).HasColumnName("Ten_may");
        }
    }
}
