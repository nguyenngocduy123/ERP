using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Erp.Domain.Sale.Entities.Mapping
{
    public class vwLogLoyaltyPointMap : EntityTypeConfiguration<vwLogLoyaltyPoint>
    {
        public vwLogLoyaltyPointMap()
        {
            // Primary Key
            this.HasKey(t => t.Id);

            // Properties
            //this.Property(t => t.Name).HasMaxLength(150);
            

            // Table & Column Mappings
            this.ToTable("vwSale_LogLoyaltyPoint");
            this.Property(t => t.Id).HasColumnName("Id");
            this.Property(t => t.CreatedUserId).HasColumnName("CreatedUserId");
            this.Property(t => t.CreatedDate).HasColumnName("CreatedDate");
            this.Property(t => t.ModifiedUserId).HasColumnName("ModifiedUserId");
            this.Property(t => t.ModifiedDate).HasColumnName("ModifiedDate");
            this.Property(t => t.AssignedUserId).HasColumnName("AssignedUserId");

            this.Property(t => t.CustomerId).HasColumnName("CustomerId");
            this.Property(t => t.ProductInvoiceId).HasColumnName("ProductInvoiceId");
            this.Property(t => t.PlusPoint).HasColumnName("PlusPoint");
            this.Property(t => t.TotalPoint).HasColumnName("TotalPoint");
            //this.Property(t => t.MemberCardId).HasColumnName("MemberCardId");

            this.Property(t => t.ProductInvoiceCode).HasColumnName("ProductInvoiceCode");
            this.Property(t => t.ProductInvoiceDate).HasColumnName("ProductInvoiceDate");
            this.Property(t => t.TotalAmount).HasColumnName("TotalAmount");
            //this.Property(t => t.MemberCardCode).HasColumnName("MemberCardCode");
        }
    }
}
