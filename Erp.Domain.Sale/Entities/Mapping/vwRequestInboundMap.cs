using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Erp.Domain.Sale.Entities.Mapping
{
    public class vwRequestInboundMap : EntityTypeConfiguration<vwRequestInbound>
    {
        public vwRequestInboundMap()
        {
            // Primary Key
            this.HasKey(t => t.Id);

            // Properties
            this.Property(t => t.Code).HasMaxLength(50);
            this.Property(t => t.Status).HasMaxLength(20);
            this.Property(t => t.BarCode).HasMaxLength(50);
            this.Property(t => t.CancelReason).HasMaxLength(300);
            this.Property(t => t.Note).HasMaxLength(300);
            this.Property(t => t.ShipName).HasMaxLength(150);
            this.Property(t => t.ShipPhone).HasMaxLength(15);


            // Table & Column Mappings
            this.ToTable("vwSale_RequestInbound");
            this.Property(t => t.Id).HasColumnName("Id");
            this.Property(t => t.CreatedUserId).HasColumnName("CreatedUserId");
            this.Property(t => t.CreatedDate).HasColumnName("CreatedDate");
            this.Property(t => t.ModifiedUserId).HasColumnName("ModifiedUserId");
            this.Property(t => t.ModifiedDate).HasColumnName("ModifiedDate");
            this.Property(t => t.AssignedUserId).HasColumnName("AssignedUserId");

            this.Property(t => t.Code).HasColumnName("Code");
            this.Property(t => t.TotalAmount).HasColumnName("TotalAmount");
            this.Property(t => t.WarehouseSourceId).HasColumnName("WarehouseSourceId");
            this.Property(t => t.WarehouseDestinationId).HasColumnName("WarehouseDestinationId");
            this.Property(t => t.Status).HasColumnName("Status");
            this.Property(t => t.BarCode).HasColumnName("BarCode");
            this.Property(t => t.CancelReason).HasColumnName("CancelReason");
            this.Property(t => t.Note).HasColumnName("Note");
            this.Property(t => t.InboundId).HasColumnName("InboundId");
            this.Property(t => t.OutboundId).HasColumnName("OutboundId");
            this.Property(t => t.ShipName).HasColumnName("ShipName");
            this.Property(t => t.ShipPhone).HasColumnName("ShipPhone");
            this.Property(t => t.BranchId).HasColumnName("BranchId");
            this.Property(t => t.StaffId).HasColumnName("StaffId");
            this.Property(t => t.Error).HasColumnName("Error");
            this.Property(t => t.ErrorQuantity).HasColumnName("ErrorQuantity");

            this.Property(t => t.BranchName).HasColumnName("BranchName");
            this.Property(t => t.WarehouseSourceName).HasColumnName("WarehouseSourceName");
            this.Property(t => t.WarehouseDestinationName).HasColumnName("WarehouseDestinationName");

            this.Property(t => t.CityId).HasColumnName("CityId");
            this.Property(t => t.DistrictId).HasColumnName("DistrictId");
            this.Property(t => t.ProvinceName).HasColumnName("ProvinceName");
            this.Property(t => t.DistrictName).HasColumnName("DistrictName");
        }
    }
}
