using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Erp.Domain.Staff.Entities.Mapping
{
    public class SalarySettingDetail_StaffMap : EntityTypeConfiguration<SalarySettingDetail_Staff>
    {
        public SalarySettingDetail_StaffMap()
        {
            // Primary Key
            this.HasKey(t => t.Id);

            // Properties

            

            // Table & Column Mappings
            this.ToTable("Staff_SalarySettingDetail_Staff");
            this.Property(t => t.Id).HasColumnName("Id");
            this.Property(t => t.CreatedUserId).HasColumnName("CreatedUserId");
            this.Property(t => t.CreatedDate).HasColumnName("CreatedDate");
            this.Property(t => t.ModifiedUserId).HasColumnName("ModifiedUserId");
            this.Property(t => t.ModifiedDate).HasColumnName("ModifiedDate");
            this.Property(t => t.AssignedUserId).HasColumnName("AssignedUserId");

            this.Property(t => t.StaffId).HasColumnName("StaffId");
            this.Property(t => t.SalarySettingId).HasColumnName("SalarySettingId");
            this.Property(t => t.SalarySettingDetailId).HasColumnName("SalarySettingDetailId");
            this.Property(t => t.DefaultValue).HasColumnName("DefaultValue");

        }
    }
}
