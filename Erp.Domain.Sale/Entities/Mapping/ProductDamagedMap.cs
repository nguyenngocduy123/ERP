using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Erp.Domain.Sale.Entities.Mapping
{
    public class ProductDamagedMap : EntityTypeConfiguration<ProductDamaged>
    {
        public ProductDamagedMap()
        {
            // Primary Key
            this.HasKey(t => t.Id);

            // Properties
            this.Property(t => t.Status).HasMaxLength(50);
            this.Property(t => t.Reason).HasMaxLength(300);


            // Table & Column Mappings
            this.ToTable("Sale_ProductDamaged");
            this.Property(t => t.Id).HasColumnName("Id");
            this.Property(t => t.CreatedUserId).HasColumnName("CreatedUserId");
            this.Property(t => t.CreatedDate).HasColumnName("CreatedDate");
            this.Property(t => t.ModifiedUserId).HasColumnName("ModifiedUserId");
            this.Property(t => t.ModifiedDate).HasColumnName("ModifiedDate");
            this.Property(t => t.AssignedUserId).HasColumnName("AssignedUserId");

            this.Property(t => t.Status).HasColumnName("Status");
            this.Property(t => t.Quantity).HasColumnName("Quantity");
            this.Property(t => t.Reason).HasColumnName("Reason");
            this.Property(t => t.ProductInboundId).HasColumnName("ProductInboundId");
            this.Property(t => t.ProductInboundDetailId).HasColumnName("ProductInboundDetailId");

        }
    }
}
