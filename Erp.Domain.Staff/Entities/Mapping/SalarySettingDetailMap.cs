using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Erp.Domain.Staff.Entities.Mapping
{
    public class SalarySettingDetailMap : EntityTypeConfiguration<SalarySettingDetail>
    {
        public SalarySettingDetailMap()
        {
            // Primary Key
            this.HasKey(t => t.Id);

            // Properties
            this.Property(t => t.Name).HasMaxLength(150);

            // Table & Column Mappings
            this.ToTable("Staff_SalarySettingDetail");
            this.Property(t => t.Id).HasColumnName("Id");
            this.Property(t => t.CreatedUserId).HasColumnName("CreatedUserId");
            this.Property(t => t.CreatedDate).HasColumnName("CreatedDate");
            this.Property(t => t.ModifiedUserId).HasColumnName("ModifiedUserId");
            this.Property(t => t.ModifiedDate).HasColumnName("ModifiedDate");
            this.Property(t => t.AssignedUserId).HasColumnName("AssignedUserId");
            this.Property(t => t.Name).HasColumnName("Name");

            this.Property(t => t.SalarySettingId).HasColumnName("SalarySettingId");
            this.Property(t => t.OrderNo).HasColumnName("OrderNo");
            this.Property(t => t.DefaultValue).HasColumnName("DefaultValue");
            this.Property(t => t.IsDefaultValue).HasColumnName("IsDefaultValue");
            this.Property(t => t.Formula).HasColumnName("Formula");
            this.Property(t => t.ParentId).HasColumnName("ParentId");
            this.Property(t => t.FormulaType).HasColumnName("FormulaType");
            this.Property(t => t.GroupName).HasColumnName("GroupName");
            this.Property(t => t.IsGroup).HasColumnName("IsGroup");
            this.Property(t => t.IsDisplay).HasColumnName("IsDisplay");
            this.Property(t => t.DataType).HasColumnName("DataType");
            this.Property(t => t.IsSum).HasColumnName("IsSum");
            this.Property(t => t.IsChange).HasColumnName("IsChange");
        }
    }
}
