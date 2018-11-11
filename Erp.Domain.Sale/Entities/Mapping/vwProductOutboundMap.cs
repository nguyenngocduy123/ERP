using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Erp.Domain.Sale.Entities.Mapping
{
    public class vwProductOutboundMap : EntityTypeConfiguration<vwProductOutbound>
    {
        public vwProductOutboundMap()
        {
            // Primary Key
            this.HasKey(t => t.Id);

            // Properties
            this.Property(t => t.Note).HasMaxLength(300);
            this.Property(t => t.Type).HasMaxLength(20);


            // Table & Column Mappings
            this.ToTable("vwSale_ProductOutbound");
            this.Property(t => t.Id).HasColumnName("Id");
            this.Property(t => t.CreatedUserId).HasColumnName("CreatedUserId");
            this.Property(t => t.CreatedDate).HasColumnName("CreatedDate");
            this.Property(t => t.ModifiedUserId).HasColumnName("ModifiedUserId");
            this.Property(t => t.ModifiedDate).HasColumnName("ModifiedDate");

            this.Property(t => t.Code).HasColumnName("Code");
            this.Property(t => t.Note).HasColumnName("Note");
            this.Property(t => t.Type).HasColumnName("Type");
            this.Property(t => t.TotalAmount).HasColumnName("TotalAmount");
            this.Property(t => t.IsDone).HasColumnName("IsDone");

            this.Property(t => t.WarehouseSourceId).HasColumnName("WarehouseSourceId");
            this.Property(t => t.WarehouseKeeperId).HasColumnName("WarehouseKeeperId");
            this.Property(t => t.InvoiceId).HasColumnName("InvoiceId");
            this.Property(t => t.WarehouseDestinationId).HasColumnName("WarehouseDestinationId");
            this.Property(t => t.PurchaseOrderId).HasColumnName("PurchaseOrderId");
            this.Property(t => t.BranchId).HasColumnName("BranchId");

            this.Property(t => t.PurchaseOrderCode).HasColumnName("PurchaseOrderCode");
            this.Property(t => t.WarehouseSourceName).HasColumnName("WarehouseSourceName");
            this.Property(t => t.InvoiceCode).HasColumnName("InvoiceCode");
            this.Property(t => t.WarehouseDestinationName).HasColumnName("WarehouseDestinationName");
            this.Property(t => t.CustomerName).HasColumnName("CustomerName");
            this.Property(t => t.PhysicalInventoryId).HasColumnName("PhysicalInventoryId");
            this.Property(t => t.BranchName).HasColumnName("BranchName");
            this.Property(t => t.IsArchive).HasColumnName("IsArchive");
            this.Property(t => t.InvoiceIsDeleted).HasColumnName("InvoiceIsDeleted");
            this.Property(t => t.CreatedStaffId).HasColumnName("CreatedStaffId");
            this.Property(t => t.CreatedStaffName).HasColumnName("CreatedStaffName");

            this.Property(t => t.CityId).HasColumnName("CityId");
            this.Property(t => t.DistrictId).HasColumnName("DistrictId");
            this.Property(t => t.ProvinceName).HasColumnName("ProvinceName");
            this.Property(t => t.DistrictName).HasColumnName("DistrictName");
        }
    }
}
