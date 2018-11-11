using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Erp.Domain.Staff.Entities.Mapping
{
    public class vwKPILogDetailMap : EntityTypeConfiguration<vwKPILogDetail>
    {
        public vwKPILogDetailMap()
        {
            // Primary Key
            this.HasKey(t => t.Id);

            // Table & Column Mappings
            this.ToTable("vwStaff_KPILogDetail");
            this.Property(t => t.Id).HasColumnName("Id");
            this.Property(t => t.CreatedUserId).HasColumnName("CreatedUserId");
            this.Property(t => t.CreatedDate).HasColumnName("CreatedDate");
            this.Property(t => t.ModifiedUserId).HasColumnName("ModifiedUserId");
            this.Property(t => t.ModifiedDate).HasColumnName("ModifiedDate");

            this.Property(t => t.KPILogId).HasColumnName("KPILogId");
            this.Property(t => t.StaffId).HasColumnName("StaffId");
            this.Property(t => t.AchieveKPIWeight).HasColumnName("AchieveKPIWeight");
            this.Property(t => t.StaffName).HasColumnName("StaffName");
            this.Property(t => t.Completed).HasColumnName("Completed");
        }
    }
}
