using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Erp.Domain.Entities.Mapping
{
    public class ModuleMap : EntityTypeConfiguration<Module>
    {
        public ModuleMap()
        {
            // Primary Key
            this.HasKey(t => t.Id);

            // Properties
            this.Property(t => t.Name).HasMaxLength(150);
                        this.Property(t => t.TableName).HasMaxLength(50);
            this.Property(t => t.DisplayName).HasMaxLength(150);
            this.Property(t => t.AreaName).HasMaxLength(50);


            // Table & Column Mappings
            this.ToTable("System_Module");
            this.Property(t => t.Id).HasColumnName("Id");
            this.Property(t => t.CreatedUserId).HasColumnName("CreatedUserId");
            this.Property(t => t.CreatedDate).HasColumnName("CreatedDate");
            this.Property(t => t.ModifiedUserId).HasColumnName("ModifiedUserId");
            this.Property(t => t.ModifiedDate).HasColumnName("ModifiedDate");
            this.Property(t => t.Name).HasColumnName("Name");

            this.Property(t => t.TableName).HasColumnName("TableName");
            this.Property(t => t.OrderNo).HasColumnName("OrderNo");
            this.Property(t => t.IsVisible).HasColumnName("IsVisible");
            this.Property(t => t.DisplayName).HasColumnName("DisplayName");
            this.Property(t => t.AreaName).HasColumnName("AreaName");

        }
    }
}
