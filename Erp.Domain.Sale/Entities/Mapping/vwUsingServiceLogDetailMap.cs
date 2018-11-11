using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Erp.Domain.Sale.Entities.Mapping
{
    public class vwUsingServiceLogDetailMap : EntityTypeConfiguration<vwUsingServiceLogDetail>
    {
        public vwUsingServiceLogDetailMap()
        {
            // Primary Key
            this.HasKey(t => t.Id);

            // Properties

            

            // Table & Column Mappings
            this.ToTable("vwSale_UsingServiceLogDetail");
            this.Property(t => t.Id).HasColumnName("Id");
            this.Property(t => t.CreatedUserId).HasColumnName("CreatedUserId");
            this.Property(t => t.CreatedDate).HasColumnName("CreatedDate");
            this.Property(t => t.ModifiedUserId).HasColumnName("ModifiedUserId");
            this.Property(t => t.ModifiedDate).HasColumnName("ModifiedDate");
            this.Property(t => t.AssignedUserId).HasColumnName("AssignedUserId");


            this.Property(t => t.UsingServiceId).HasColumnName("UsingServiceId");
            this.Property(t => t.StaffId).HasColumnName("StaffId");
            this.Property(t => t.Name).HasColumnName("Name");
            this.Property(t => t.Code).HasColumnName("Code");
            this.Property(t => t.Type).HasColumnName("Type");
            this.Property(t => t.Status).HasColumnName("Status");
            this.Property(t => t.ProductInvoiceCode).HasColumnName("ProductInvoiceCode");
            this.Property(t => t.LastName).HasColumnName("LastName");
            this.Property(t => t.ProductInvoiceId).HasColumnName("ProductInvoiceId");
            this.Property(t => t.CustomerId).HasColumnName("CustomerId");
            this.Property(t => t.ServiceName).HasColumnName("ServiceName");
            this.Property(t => t.ServiceInvoiceDetailId).HasColumnName("ServiceInvoiceDetailId");
            this.Property(t => t.CustomerCode).HasColumnName("CustomerCode");
            this.Property(t => t.CustomerName).HasColumnName("CustomerName");
            this.Property(t => t.IsVote).HasColumnName("IsVote");
            this.Property(t => t.CustomerImage).HasColumnName("CustomerImage");
            this.Property(t => t.BranchId).HasColumnName("BranchId");
            this.Property(t => t.ProfileImage).HasColumnName("ProfileImage");
        }
    }
}
