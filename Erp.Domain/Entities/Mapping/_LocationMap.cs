using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Erp.Domain.Entities.Mapping
{
    public class LocationMap : EntityTypeConfiguration<Location>
    {
        public LocationMap()
        {
            // Primary Key
            this.HasKey(t => t.Id);

            // Properties
            this.Property(t => t.Id)
                .HasMaxLength(10);
            this.Property(t => t.Name)
               .HasMaxLength(250);
            this.Property(t => t.Type)
               .HasMaxLength(250);
            this.Property(t => t.ParentId)
               .HasMaxLength(10);

            // Table & Column Mappings
            this.ToTable("vwSystem_Location");
            this.Property(t => t.Id).HasColumnName("Id");
            this.Property(t => t.Name).HasColumnName("Name");
            this.Property(t => t.Type).HasColumnName("Type");
            this.Property(t => t.ParentId).HasColumnName("ParentId");
            this.Property(t => t.Group).HasColumnName("Group");
        }
    }
}
