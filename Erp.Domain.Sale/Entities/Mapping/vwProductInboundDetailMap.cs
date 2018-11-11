using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Erp.Domain.Sale.Entities.Mapping
{
    public class vwProductInboundDetailMap : EntityTypeConfiguration<vwProductInboundDetail>
    {
        public vwProductInboundDetailMap()
        {
            // Primary Key
            this.HasKey(t => t.Id);

            // Properties

            // Table & Column Mappings
            this.ToTable("vwSale_ProductInboundDetail");
            this.Property(t => t.Id).HasColumnName("Id");
            this.Property(t => t.CreatedUserId).HasColumnName("CreatedUserId");
            this.Property(t => t.CreatedDate).HasColumnName("CreatedDate");
            this.Property(t => t.ModifiedUserId).HasColumnName("ModifiedUserId");
            this.Property(t => t.ModifiedDate).HasColumnName("ModifiedDate");


            this.Property(t => t.ProductInboundId).HasColumnName("ProductInboundId");
            this.Property(t => t.ProductId).HasColumnName("ProductId");
            this.Property(t => t.ProductName).HasColumnName("ProductName");
            this.Property(t => t.ProductCode).HasColumnName("ProductCode");
            this.Property(t => t.ProductBarCode).HasColumnName("ProductBarCode");
            this.Property(t => t.Quantity).HasColumnName("Quantity");
            this.Property(t => t.Price).HasColumnName("Price");
            this.Property(t => t.Unit).HasColumnName("Unit");

            this.Property(t => t.ProductInboundCode).HasColumnName("ProductInboundCode");
            this.Property(t => t.ShipperName).HasColumnName("ShipperName");
            this.Property(t => t.PurchaseOrderId).HasColumnName("PurchaseOrderId");
            this.Property(t => t.TotalAmount).HasColumnName("TotalAmount");
            this.Property(t => t.Type).HasColumnName("Type");
            this.Property(t => t.WarehouseDestinationId).HasColumnName("WarehouseDestinationId");
            this.Property(t => t.WarehouseDestinationName).HasColumnName("WarehouseDestinationName");
            this.Property(t => t.BranchId).HasColumnName("BranchId");
            this.Property(t => t.LoCode).HasColumnName("LoCode");
            this.Property(t => t.ExpiryDate).HasColumnName("ExpiryDate");

            this.Property(t => t.ProductDamagedId).HasColumnName("ProductDamagedId");
            this.Property(t => t.Status).HasColumnName("Status");
            this.Property(t => t.NumberAmount).HasColumnName("NumberAmount");
            this.Property(t => t.Reason).HasColumnName("Reason");
            this.Property(t => t.IsArchive).HasColumnName("IsArchive");

            this.Property(t => t.SalesReturnsCode).HasColumnName("SalesReturnsCode");
            this.Property(t => t.CustomerName).HasColumnName("CustomerName");
        }
    }
}
