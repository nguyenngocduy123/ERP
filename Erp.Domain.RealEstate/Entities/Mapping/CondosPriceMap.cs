using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Erp.Domain.RealEstate.Entities.Mapping
{
    public class CondosPriceMap : EntityTypeConfiguration<CondosPrice>
    {
        public CondosPriceMap()
        {
            // Primary Key
            this.HasKey(t => t.Id);

            // Properties
            this.Property(t => t.Name).HasMaxLength(150);
                        this.Property(t => t.Note).HasMaxLength(300);


            // Table & Column Mappings
            this.ToTable("RE_CondosPrice");
            this.Property(t => t.Id).HasColumnName("Id");
            this.Property(t => t.CreatedUserId).HasColumnName("CreatedUserId");
            this.Property(t => t.CreatedDate).HasColumnName("CreatedDate");
            this.Property(t => t.ModifiedUserId).HasColumnName("ModifiedUserId");
            this.Property(t => t.ModifiedDate).HasColumnName("ModifiedDate");
            this.Property(t => t.AssignedUserId).HasColumnName("AssignedUserId");
            this.Property(t => t.Name).HasColumnName("Name");

            this.Property(t => t.CondosId).HasColumnName("CondosId");
            this.Property(t => t.PriceBefore).HasColumnName("PriceBefore");
            this.Property(t => t.PriceAfter).HasColumnName("PriceAfter");
            this.Property(t => t.Difference).HasColumnName("Difference");
            this.Property(t => t.Note).HasColumnName("Note");

        }
    }
}
