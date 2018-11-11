using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Erp.Domain.Sale.Entities.Mapping
{
    public class PromotionDetailMap : EntityTypeConfiguration<PromotionDetail>
    {
        public PromotionDetailMap()
        {
            // Primary Key
            this.HasKey(t => t.Id);

            // Properties


            // Table & Column Mappings
            this.ToTable("Sale_PromotionDetail");
            this.Property(t => t.Id).HasColumnName("Id");

            this.Property(t => t.PromotionId).HasColumnName("PromotionId");
            this.Property(t => t.ProductId).HasColumnName("ProductId");
            this.Property(t => t.PercentValue).HasColumnName("PercentValue");
            this.Property(t => t.QuantityFor).HasColumnName("QuantityFor");
            this.Property(t => t.IsAll).HasColumnName("IsAll");
            this.Property(t => t.CategoryCode).HasColumnName("CategoryCode");
            this.Property(t => t.Type).HasColumnName("Type");
        }
    }
}
