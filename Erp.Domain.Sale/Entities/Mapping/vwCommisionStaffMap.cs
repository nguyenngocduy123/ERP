using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Erp.Domain.Sale.Entities.Mapping
{
    public class vwCommisionStaffMap : EntityTypeConfiguration<vwCommisionStaff>
    {
        public vwCommisionStaffMap()
        {
            // Primary Key
            this.HasKey(t => t.Id);

            // Properties
            this.Property(t => t.Note).HasMaxLength(300);
            this.Property(t => t.ProductInvoiceCode).HasMaxLength(20);
            this.Property(t => t.StaffCode).HasMaxLength(50);
            this.Property(t => t.BranchName).HasMaxLength(100);


            // Table & Column Mappings
            this.ToTable("vwSale_Commision_Staff");
            this.Property(t => t.Id).HasColumnName("Id");
            this.Property(t => t.CreatedUserId).HasColumnName("CreatedUserId");
            this.Property(t => t.CreatedDate).HasColumnName("CreatedDate");
            this.Property(t => t.ModifiedUserId).HasColumnName("ModifiedUserId");
            this.Property(t => t.ModifiedDate).HasColumnName("ModifiedDate");

            this.Property(t => t.StaffId).HasColumnName("StaffId");
            this.Property(t => t.BranchId).HasColumnName("BranchId");
            this.Property(t => t.InvoiceId).HasColumnName("InvoiceId");
            this.Property(t => t.InvoiceDetailId).HasColumnName("InvoiceDetailId");
            this.Property(t => t.InvoiceType).HasColumnName("InvoiceType");
            this.Property(t => t.PercentOfCommision).HasColumnName("PercentOfCommision");
            this.Property(t => t.AmountOfCommision).HasColumnName("AmountOfCommision");
            this.Property(t => t.Note).HasColumnName("Note");
            this.Property(t => t.IsResolved).HasColumnName("IsResolved");
            this.Property(t => t.StaffName).HasColumnName("StaffName");
            this.Property(t => t.StaffProfileImage).HasColumnName("StaffProfileImage");
            this.Property(t => t.ProductInvoiceCode).HasColumnName("ProductInvoiceCode");
            this.Property(t => t.StaffCode).HasColumnName("StaffCode");
            this.Property(t => t.BranchName).HasColumnName("BranchName");
            this.Property(t => t.ProductCode).HasColumnName("ProductCode");
            this.Property(t => t.ProductName).HasColumnName("ProductName");
            this.Property(t => t.ProductImage).HasColumnName("ProductImage");
            this.Property(t => t.month).HasColumnName("month");
            this.Property(t => t.year).HasColumnName("year");
        }
    }
}
