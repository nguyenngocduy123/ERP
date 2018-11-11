using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Erp.Domain.Staff.Entities.Mapping
{
    public class KPIItemMap : EntityTypeConfiguration<KPIItem>
    {
        public KPIItemMap()
        {
            // Primary Key
            this.HasKey(t => t.Id);

            // Properties
            this.Property(t => t.Name).HasMaxLength(150);
                        this.Property(t => t.Measure).HasMaxLength(150);
            this.Property(t => t.Description).HasMaxLength(300);


            // Table & Column Mappings
            this.ToTable("Staff_KPIItem");
            this.Property(t => t.Id).HasColumnName("Id");
            this.Property(t => t.CreatedUserId).HasColumnName("CreatedUserId");
            this.Property(t => t.CreatedDate).HasColumnName("CreatedDate");
            this.Property(t => t.ModifiedUserId).HasColumnName("ModifiedUserId");
            this.Property(t => t.ModifiedDate).HasColumnName("ModifiedDate");
            this.Property(t => t.Name).HasColumnName("Name");

            this.Property(t => t.KPICatalogId).HasColumnName("KPICatalogId");
            this.Property(t => t.Measure).HasColumnName("Measure");
            this.Property(t => t.TargetScore_From).HasColumnName("TargetScore_From");
            this.Property(t => t.TargetScore_To).HasColumnName("TargetScore_To");
            this.Property(t => t.KPIWeight).HasColumnName("KPIWeight");
            this.Property(t => t.Description).HasColumnName("Description");
        }
    }
}
