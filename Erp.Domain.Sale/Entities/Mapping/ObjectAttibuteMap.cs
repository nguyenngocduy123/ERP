using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Erp.Domain.Sale.Entities.Mapping
{
    public class ObjectAttributeMap : EntityTypeConfiguration<ObjectAttribute>
    {
        public ObjectAttributeMap()
        {
            // Primary Key
            this.HasKey(t => t.Id);

            // Properties
            this.Property(t => t.Name).HasMaxLength(150);
                        this.Property(t => t.DataType).HasMaxLength(20);
            this.Property(t => t.ModuleType).HasMaxLength(50);
            this.Property(t => t.ModuleCategoryType).HasMaxLength(50);


            // Table & Column Mappings
            this.ToTable("Sale_ObjectAttribute");
            this.Property(t => t.Id).HasColumnName("Id");
            this.Property(t => t.CreatedUserId).HasColumnName("CreatedUserId");
            this.Property(t => t.CreatedDate).HasColumnName("CreatedDate");
            this.Property(t => t.ModifiedUserId).HasColumnName("ModifiedUserId");
            this.Property(t => t.ModifiedDate).HasColumnName("ModifiedDate");
            this.Property(t => t.Name).HasColumnName("Name");

            this.Property(t => t.DataType).HasColumnName("DataType");
            this.Property(t => t.ModuleType).HasColumnName("ModuleType");
            this.Property(t => t.ModuleCategoryType).HasColumnName("ModuleCategoryType");
            this.Property(t => t.ModuleId).HasColumnName("ModuleId");
            this.Property(t => t.OrderNo).HasColumnName("OrderNo");
            this.Property(t => t.IsSelected).HasColumnName("IsSelected");

        }
    }
}
