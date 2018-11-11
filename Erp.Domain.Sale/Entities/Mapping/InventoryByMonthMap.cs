using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Erp.Domain.Sale.Entities.Mapping
{
    public class InventoryByMonthMap : EntityTypeConfiguration<InventoryByMonth>
    {
        public InventoryByMonthMap()
        {
            // Primary Key
            this.HasKey(t => t.Id);

            // Properties
            

            // Table & Column Mappings
            this.ToTable("Sale_InventoryByMonth");
            this.Property(t => t.Id).HasColumnName("Id");
            this.Property(t => t.CreatedUserId).HasColumnName("CreatedUserId");
            this.Property(t => t.CreatedDate).HasColumnName("CreatedDate");
            this.Property(t => t.ModifiedUserId).HasColumnName("ModifiedUserId");
            this.Property(t => t.ModifiedDate).HasColumnName("ModifiedDate");

            this.Property(t => t.WarehouseId).HasColumnName("WarehouseId");
            this.Property(t => t.ProductId).HasColumnName("ProductId");

            this.Property(t => t.Month).HasColumnName("Month");
            this.Property(t => t.Year).HasColumnName("Year");
            this.Property(t => t.InventoryQuantityBeginNextMonth).HasColumnName("InventoryQuantityBeginNextMonth");
            this.Property(t => t.InventoryQuantityEndPreviousMonth).HasColumnName("InventoryQuantityEndPreviousMonth");
            this.Property(t => t.InventoryQuantityThisMonth).HasColumnName("InventoryQuantityThisMonth");
            this.Property(t => t.OutboundQuantityOfMonth).HasColumnName("OutboundQuantityOfMonth");
            this.Property(t => t.InboundQuantityOfMonth).HasColumnName("InboundQuantityOfMonth");

        }
    }
}
