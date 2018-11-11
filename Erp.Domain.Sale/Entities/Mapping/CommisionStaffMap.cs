using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Erp.Domain.Sale.Entities.Mapping
{
    public class CommisionStaffMap : EntityTypeConfiguration<CommisionStaff>
    {
        public CommisionStaffMap()
        {
            // Primary Key
            this.HasKey(t => t.Id);

            // Properties
            this.Property(t => t.Note).HasMaxLength(300);


            // Table & Column Mappings
            this.ToTable("Sale_Commision_Staff");
            this.Property(t => t.Id).HasColumnName("Id");
            this.Property(t => t.CreatedUserId).HasColumnName("CreatedUserId");
            this.Property(t => t.CreatedDate).HasColumnName("CreatedDate");
            this.Property(t => t.ModifiedUserId).HasColumnName("ModifiedUserId");
            this.Property(t => t.ModifiedDate).HasColumnName("ModifiedDate");

            this.Property(t => t.StaffId).HasColumnName("StaffId");
            this.Property(t => t.BranchId).HasColumnName("BranchId");
            this.Property(t => t.PercentOfCommision).HasColumnName("PercentOfCommision");
            this.Property(t => t.AmountOfCommision).HasColumnName("AmountOfCommision");
            this.Property(t => t.InvoiceId).HasColumnName("InvoiceId");
            this.Property(t => t.InvoiceDetailId).HasColumnName("InvoiceDetailId");
            this.Property(t => t.InvoiceType).HasColumnName("InvoiceType");
            this.Property(t => t.Note).HasColumnName("Note");
        }
    }
}
