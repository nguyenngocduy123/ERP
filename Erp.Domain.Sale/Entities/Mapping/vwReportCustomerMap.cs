using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Erp.Domain.Sale.Entities.Mapping
{
    class vwReportCustomerMap: EntityTypeConfiguration<vwReportCustomer>
    {
        public vwReportCustomerMap()
        {
            // Primary Key
            this.HasKey(t => t.Id);

            // Properties
            

            // Table & Column Mappings
            this.ToTable("vwSale_ReportCustomer");
            this.Property(t => t.Id).HasColumnName("Id");
            this.Property(t => t.CreatedUserId).HasColumnName("CreatedUserId");
            this.Property(t => t.CreatedDate).HasColumnName("CreatedDate");
            this.Property(t => t.ModifiedUserId).HasColumnName("ModifiedUserId");
            this.Property(t => t.ModifiedDate).HasColumnName("ModifiedDate");
            this.Property(t => t.AssignedUserId).HasColumnName("AssignedUserId");

            this.Property(t => t.BranchId).HasColumnName("BranchId");
            this.Property(t => t.Code).HasColumnName("Code");
            this.Property(t => t.CompanyName).HasColumnName("CompanyName");
            this.Property(t => t.Note).HasColumnName("Note");

            this.Property(t => t.QtyContact).HasColumnName("QtyContact");
            this.Property(t => t.QtyInvoice).HasColumnName("QtyInvoice");
            this.Property(t => t.QtySaleOrder).HasColumnName("QtySaleOrder");

        }
    }
}
