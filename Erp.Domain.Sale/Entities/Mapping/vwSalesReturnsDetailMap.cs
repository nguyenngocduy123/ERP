using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Erp.Domain.Sale.Entities.Mapping
{
    public class vwSalesReturnsDetailMap : EntityTypeConfiguration<vwSalesReturnsDetail>
    {
        public vwSalesReturnsDetailMap()
        {
            // Primary Key
            this.HasKey(t => t.Id);

            // Table & Column Mappings
            this.ToTable("vwSale_SalesReturnsDetail");
            this.Property(t => t.Id).HasColumnName("Id");
            this.Property(t => t.CreatedUserId).HasColumnName("CreatedUserId");
            this.Property(t => t.CreatedDate).HasColumnName("CreatedDate");
            this.Property(t => t.ModifiedUserId).HasColumnName("ModifiedUserId");
            this.Property(t => t.ModifiedDate).HasColumnName("ModifiedDate");

            this.Property(t => t.SalesReturnsId).HasColumnName("SalesReturnsId");
            this.Property(t => t.ProductId).HasColumnName("ProductId");
            this.Property(t => t.ProductName).HasColumnName("ProductName");
            this.Property(t => t.ProductCode).HasColumnName("ProductCode");
            this.Property(t => t.Price).HasColumnName("Price");
            this.Property(t => t.Quantity).HasColumnName("Quantity");
            this.Property(t => t.UnitProduct).HasColumnName("UnitProduct");

            //this.Property(t => t.PromotionId).HasColumnName("PromotionId");
            //this.Property(t => t.PromotionDetailId).HasColumnName("PromotionDetailId");
            //this.Property(t => t.PromotionValue).HasColumnName("PromotionValue");
            this.Property(t => t.DisCount).HasColumnName("DisCount");
            this.Property(t => t.DisCountAmount).HasColumnName("DisCountAmount");
            this.Property(t => t.ProductGroup).HasColumnName("ProductGroup");
            this.Property(t => t.CheckPromotion).HasColumnName("CheckPromotion");

            this.Property(t => t.SalesReturnDate).HasColumnName("SalesReturnDate");
            this.Property(t => t.ProductInvoiceId).HasColumnName("ProductInvoiceId");
            this.Property(t => t.ProductInvoiceDetailId).HasColumnName("ProductInvoiceDetailId");
            this.Property(t => t.ProductInvoiceCode).HasColumnName("ProductInvoiceCode");
            this.Property(t => t.ProductInvoiceDate).HasColumnName("ProductInvoiceDate");
            this.Property(t => t.SalerInvoiceName).HasColumnName("SalerInvoiceName");
        }
    }
}
