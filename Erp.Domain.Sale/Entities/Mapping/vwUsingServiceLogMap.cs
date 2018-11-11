using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Erp.Domain.Sale.Entities.Mapping
{
    public class vwUsingServiceLogMap : EntityTypeConfiguration<vwUsingServiceLog>
    {
        public vwUsingServiceLogMap()
        {
            // Primary Key
            this.HasKey(t => t.Id);

            // Properties
            

            // Table & Column Mappings
            this.ToTable("vwSale_UsingServiceLog");
            this.Property(t => t.Id).HasColumnName("Id");
            this.Property(t => t.CreatedUserId).HasColumnName("CreatedUserId");
            this.Property(t => t.CreatedDate).HasColumnName("CreatedDate");
            this.Property(t => t.ModifiedUserId).HasColumnName("ModifiedUserId");
            this.Property(t => t.ModifiedDate).HasColumnName("ModifiedDate");
            this.Property(t => t.AssignedUserId).HasColumnName("AssignedUserId");


            this.Property(t => t.ServiceInvoiceDetailId).HasColumnName("ServiceInvoiceDetailId");
            this.Property(t => t.Quantity).HasColumnName("Quantity");
            this.Property(t => t.QuantityUsed).HasColumnName("QuantityUsed");
            this.Property(t => t.ServiceId).HasColumnName("ServiceId");

            this.Property(t => t.ProductName).HasColumnName("ProductName");
            this.Property(t => t.ProductCode).HasColumnName("ProductCode");
            this.Property(t => t.CategoryCode).HasColumnName("CategoryCode");
            this.Property(t => t.IsCombo).HasColumnName("IsCombo");
            this.Property(t => t.ItemName).HasColumnName("ItemName");
            this.Property(t => t.ItemCode).HasColumnName("ItemCode");
            this.Property(t => t.ItemCategory).HasColumnName("ItemCategory");
            this.Property(t => t.ServiceComboId).HasColumnName("ServiceComboId");
            this.Property(t => t.SalerName).HasColumnName("SalerName");
            this.Property(t => t.ProductInvoiceId).HasColumnName("ProductInvoiceId");
            this.Property(t => t.CustomerId).HasColumnName("CustomerId");
            this.Property(t => t.ProductInvoiceCode).HasColumnName("ProductInvoiceCode");
            this.Property(t => t.ProductInvoiceDate).HasColumnName("ProductInvoiceDate");

            this.Property(t => t.CustomerCode).HasColumnName("CustomerCode");
            this.Property(t => t.CustomerName).HasColumnName("CustomerName");
        }
    }
}
