using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Erp.Domain.Sale.Entities.Mapping
{
    public class vwProductInvoiceMap : EntityTypeConfiguration<vwProductInvoice>
    {
        public vwProductInvoiceMap()
        {
            // Primary Key
            this.HasKey(t => t.Id);

            // Properties
            this.Property(t => t.Code).HasMaxLength(20);
            this.Property(t => t.ShipName).HasMaxLength(50);
            this.Property(t => t.ShipAddress).HasMaxLength(200);
            this.Property(t => t.ShipWardId).HasMaxLength(50);
            this.Property(t => t.ShipDistrictId).HasMaxLength(50);
            this.Property(t => t.ShipCityId).HasMaxLength(50);
            this.Property(t => t.Status).HasMaxLength(50);
            this.Property(t => t.Note).HasMaxLength(300);


            // Table & Column Mappings
            this.ToTable("vwSale_ProductInvoice");
            this.Property(t => t.Id).HasColumnName("Id");
            this.Property(t => t.CreatedUserId).HasColumnName("CreatedUserId");
            this.Property(t => t.CreatedDate).HasColumnName("CreatedDate");
            this.Property(t => t.ModifiedUserId).HasColumnName("ModifiedUserId");
            this.Property(t => t.ModifiedDate).HasColumnName("ModifiedDate");

            this.Property(t => t.Code).HasColumnName("Code");
            this.Property(t => t.TotalAmount).HasColumnName("TotalAmount");
            this.Property(t => t.TaxFee).HasColumnName("TaxFee");
            this.Property(t => t.Discount).HasColumnName("Discount");
            this.Property(t => t.PaidAmount).HasColumnName("PaidAmount");
            this.Property(t => t.CustomerId).HasColumnName("CustomerId");
            this.Property(t => t.ShipName).HasColumnName("ShipName");
            this.Property(t => t.ShipPhone).HasColumnName("ShipPhone");
            this.Property(t => t.ShipAddress).HasColumnName("ShipAddress");
            this.Property(t => t.ShipWardId).HasColumnName("ShipWardId");
            this.Property(t => t.ShipDistrictId).HasColumnName("ShipDistrictId");
            this.Property(t => t.ShipCityId).HasColumnName("ShipCityId");
            this.Property(t => t.Status).HasColumnName("Status");
            this.Property(t => t.Note).HasColumnName("Note");
            this.Property(t => t.IsArchive).HasColumnName("IsArchive");

            this.Property(t => t.BranchId).HasColumnName("BranchId");
            this.Property(t => t.CancelReason).HasColumnName("CancelReason");
            this.Property(t => t.PaymentMethod).HasColumnName("PaymentMethod");
            this.Property(t => t.SalerId).HasColumnName("SalerId");

            this.Property(t => t.CustomerCode).HasColumnName("CustomerCode");
            this.Property(t => t.Email).HasColumnName("Email");
            this.Property(t => t.ShipWardName).HasColumnName("ShipWardName");
            this.Property(t => t.ShipDistrictName).HasColumnName("ShipDistrictName");
            this.Property(t => t.ShipCityName).HasColumnName("ShipCityName");
            this.Property(t => t.ProductOutboundCode).HasColumnName("ProductOutboundCode");
            this.Property(t => t.BranchName).HasColumnName("BranchName");
            this.Property(t => t.CodeInvoiceRed).HasColumnName("CodeInvoiceRed");
            this.Property(t => t.SalerFullName).HasColumnName("SalerFullName");
            this.Property(t => t.ProductOutboundId).HasColumnName("ProductOutboundId");
            this.Property(t => t.IsReturn).HasColumnName("IsReturn");
            this.Property(t => t.NextPaymentDate).HasColumnName("NextPaymentDate");
            this.Property(t => t.StaffCreateName).HasColumnName("StaffCreateName");
            this.Property(t => t.StaffCreateId).HasColumnName("StaffCreateId");
            this.Property(t => t.Image).HasColumnName("Image");
            this.Property(t => t.CustomerPhone).HasColumnName("CustomerPhone");
            this.Property(t => t.CustomerName).HasColumnName("CustomerName");
            this.Property(t => t.Type).HasColumnName("Type");
            this.Property(t => t.FixedDiscount).HasColumnName("FixedDiscount");
            this.Property(t => t.IrregularDiscount).HasColumnName("IrregularDiscount");
            this.Property(t => t.Month).HasColumnName("Month");
            this.Property(t => t.Year).HasColumnName("Year");
            this.Property(t => t.Day).HasColumnName("Day");
            this.Property(t => t.WeekOfYear).HasColumnName("WeekOfYear");
            this.Property(t => t.DrugStoreDate).HasColumnName("DrugStoreDate");
            this.Property(t => t.DrugStoreUserId).HasColumnName("DrugStoreUserId");
            this.Property(t => t.AccountancyDate).HasColumnName("AccountancyDate");
            this.Property(t => t.AccountancyUserId).HasColumnName("AccountancyUserId");

            this.Property(t => t.CityId).HasColumnName("CityId");
            this.Property(t => t.DistrictId).HasColumnName("DistrictId");
            this.Property(t => t.ProvinceName).HasColumnName("ProvinceName");
            this.Property(t => t.DistrictName).HasColumnName("DistrictName");
        }
    }
}
