using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Erp.Domain.Staff.Entities.Mapping
{
    public class SalarySettingMap : EntityTypeConfiguration<SalarySetting>
    {
        public SalarySettingMap()
        {
            // Primary Key
            this.HasKey(t => t.Id);

            // Properties
            this.Property(t => t.Name).HasMaxLength(150);
            this.Property(t => t.Note).HasMaxLength(300);

            // Table & Column Mappings
            this.ToTable("Staff_SalarySetting");
            this.Property(t => t.Id).HasColumnName("Id");
            this.Property(t => t.CreatedUserId).HasColumnName("CreatedUserId");
            this.Property(t => t.CreatedDate).HasColumnName("CreatedDate");
            this.Property(t => t.ModifiedUserId).HasColumnName("ModifiedUserId");
            this.Property(t => t.ModifiedDate).HasColumnName("ModifiedDate");
            this.Property(t => t.AssignedUserId).HasColumnName("AssignedUserId");
            this.Property(t => t.Name).HasColumnName("Name");

            this.Property(t => t.Note).HasColumnName("Note");
            this.Property(t => t.IsTemplate).HasColumnName("IsTemplate");
            this.Property(t => t.IsSend).HasColumnName("IsSend");
            this.Property(t => t.SalaryApprovalType).HasColumnName("SalaryApprovalType");
            this.Property(t => t.OrderNo).HasColumnName("OrderNo");
            this.Property(t => t.HiddenForMonth).HasColumnName("HiddenForMonth");
        }
    }
}
