using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Erp.Domain.Entities.Mapping
{
    public class MetadataFieldMap : EntityTypeConfiguration<MetadataField>
    {
        public MetadataFieldMap()
        {
            // Primary Key
            this.HasKey(t => t.Id);

            // Properties
            this.Property(t => t.Name).HasMaxLength(150);
                        this.Property(t => t.Expression).HasMaxLength(300);


            // Table & Column Mappings
                        this.ToTable("System_MetadataField");
            this.Property(t => t.Id).HasColumnName("Id");
            this.Property(t => t.CreatedUserId).HasColumnName("CreatedUserId");
            this.Property(t => t.CreatedDate).HasColumnName("CreatedDate");
            this.Property(t => t.ModifiedUserId).HasColumnName("ModifiedUserId");
            this.Property(t => t.ModifiedDate).HasColumnName("ModifiedDate");
            this.Property(t => t.Name).HasColumnName("Name");

            this.Property(t => t.ModuleId).HasColumnName("ModuleId");
            this.Property(t => t.IsVisible).HasColumnName("IsVisible");
            this.Property(t => t.OrderNo).HasColumnName("OrderNo");
            this.Property(t => t.Expression).HasColumnName("Expression");

        }
    }
}
