using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Erp.Domain.Sale.Entities.Mapping
{
    public class vwWarehouseLocationItemMap : EntityTypeConfiguration<vwWarehouseLocationItem>
    {
        public vwWarehouseLocationItemMap()
        {
            // Primary Key
            this.HasKey(t => t.Id);

            // Properties
            this.Property(t => t.SN).HasMaxLength(50);
            this.Property(t => t.Shelf).HasMaxLength(10);
            this.Property(t => t.Floor).HasMaxLength(10);
            this.Property(t => t.Position).HasMaxLength(10);


            // Table & Column Mappings
            this.ToTable("vwSale_WarehouseLocationItem");
            this.Property(t => t.Id).HasColumnName("Id");
            this.Property(t => t.CreatedUserId).HasColumnName("CreatedUserId");
            this.Property(t => t.CreatedDate).HasColumnName("CreatedDate");
            this.Property(t => t.ModifiedUserId).HasColumnName("ModifiedUserId");
            this.Property(t => t.ModifiedDate).HasColumnName("ModifiedDate");
            this.Property(t => t.AssignedUserId).HasColumnName("AssignedUserId");

            this.Property(t => t.SN).HasColumnName("SN");
            this.Property(t => t.Shelf).HasColumnName("Shelf");
            this.Property(t => t.Floor).HasColumnName("Floor");
            this.Property(t => t.Position).HasColumnName("Position");
            this.Property(t => t.ExpiryDate).HasColumnName("ExpiryDate");
            this.Property(t => t.IsOut).HasColumnName("IsOut");
            this.Property(t => t.WarehouseId).HasColumnName("WarehouseId");
            this.Property(t => t.ProductId).HasColumnName("ProductId");
            this.Property(t => t.ProductInboundId).HasColumnName("ProductInboundId");
            this.Property(t => t.ProductInboundDetailId).HasColumnName("ProductInboundDetailId");
            this.Property(t => t.ProductOutboundId).HasColumnName("ProductOutboundId");
            this.Property(t => t.ProductOutboundDetailId).HasColumnName("ProductOutboundDetailId");

            this.Property(t => t.WarehouseCode).HasColumnName("WarehouseCode");
            this.Property(t => t.ProductCode).HasColumnName("ProductCode");
            this.Property(t => t.WarehouseName).HasColumnName("WarehouseName");
            this.Property(t => t.ProductName).HasColumnName("ProductName");
            this.Property(t => t.ProductInboundCode).HasColumnName("ProductInboundCode");
            this.Property(t => t.BranchId).HasColumnName("BranchId");
            this.Property(t => t.LoCode).HasColumnName("LoCode");
            this.Property(t => t.IsSale).HasColumnName("IsSale");
        }
    }
}
