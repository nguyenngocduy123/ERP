using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Erp.Domain.Sale.Entities.Mapping
{
    public class vwInventoryMap : EntityTypeConfiguration<vwInventory>
    {
        public vwInventoryMap()
        {
            // Primary Key
            this.HasKey(t => new { t.WarehouseId, t.ProductId });

            // Properties            

            // Table & Column Mappings
            this.ToTable("vwSale_Inventory");

            this.Property(t => t.WarehouseId).HasColumnName("WarehouseId");
            this.Property(t => t.WarehouseName).HasColumnName("WarehouseName");
            this.Property(t => t.ProductId).HasColumnName("ProductId");

            this.Property(t => t.ProductGroup).HasColumnName("ProductGroup");
            this.Property(t => t.CategoryCode).HasColumnName("CategoryCode");
            this.Property(t => t.ProductName).HasColumnName("ProductName");
            this.Property(t => t.Quantity).HasColumnName("Quantity");
            this.Property(t => t.ProductPriceInbound).HasColumnName("ProductPriceInbound");
            this.Property(t => t.ProductPriceOutbound).HasColumnName("ProductPriceOutbound");
            this.Property(t => t.ProductUnit).HasColumnName("ProductUnit");
            this.Property(t => t.ProductCode).HasColumnName("ProductCode");
            this.Property(t => t.ProductMinInventory).HasColumnName("ProductMinInventory");
            this.Property(t => t.BranchId).HasColumnName("BranchId");
            this.Property(t => t.BranchName).HasColumnName("BranchName");
            this.Property(t => t.Image_Name).HasColumnName("Image_Name");
            this.Property(t => t.IsSale).HasColumnName("IsSale");
            this.Property(t => t.LoCode).HasColumnName("LoCode");
            this.Property(t => t.ExpiryDate).HasColumnName("ExpiryDate");
        }
    }
}
