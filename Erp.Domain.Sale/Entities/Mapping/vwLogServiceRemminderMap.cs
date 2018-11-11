using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Erp.Domain.Sale.Entities.Mapping
{
    public class vwLogServiceRemminderMap : EntityTypeConfiguration<vwLogServiceRemminder>
    {
        public vwLogServiceRemminderMap()
        {
            // Primary Key
            this.HasKey(t => t.Id);

            // Properties
          
            this.Property(t => t.ReminderName).HasMaxLength(300);


            // Table & Column Mappings
            this.ToTable("vwSale_LogServiceRemminder");
            this.Property(t => t.Id).HasColumnName("Id");
            this.Property(t => t.CreatedUserId).HasColumnName("CreatedUserId");
            this.Property(t => t.CreatedDate).HasColumnName("CreatedDate");
            this.Property(t => t.ModifiedUserId).HasColumnName("ModifiedUserId");
            this.Property(t => t.ModifiedDate).HasColumnName("ModifiedDate");
            this.Property(t => t.AssignedUserId).HasColumnName("AssignedUserId");
            //this.Property(t => t.Name).HasColumnName("Name");

            this.Property(t => t.ProductInvoiceDetailId).HasColumnName("ProductInvoiceDetailId");
            this.Property(t => t.ReminderId).HasColumnName("ReminderId");
            this.Property(t => t.ReminderName).HasColumnName("ReminderName");
            this.Property(t => t.ReminderDate).HasColumnName("ReminderDate");
            this.Property(t => t.ServiceId).HasColumnName("ServiceId");

            this.Property(t => t.ProductGroupCode).HasColumnName("ProductGroupCode");
            this.Property(t => t.ProductGroupName).HasColumnName("ProductGroupName");
            this.Property(t => t.ProductCode).HasColumnName("ProductCode");
            this.Property(t => t.ProductName).HasColumnName("ProductName");
            this.Property(t => t.CustomerName).HasColumnName("CustomerName");
            this.Property(t => t.ProductInvoiceCode).HasColumnName("ProductInvoiceCode");
            this.Property(t => t.ProductInvoiceDate).HasColumnName("ProductInvoiceDate");
            this.Property(t => t.ProductInvoiceId).HasColumnName("ProductInvoiceId");
            this.Property(t => t.CustomerId).HasColumnName("CustomerId");

        }
    }
}
