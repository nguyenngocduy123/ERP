using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Erp.Domain.Crm.Entities.Mapping
{
    public class vwVote2Map : EntityTypeConfiguration<vwVote2>
    {
        public vwVote2Map()
        {
            // Primary Key
            this.HasKey(t => t.Id);

            // Properties

            // Table & Column Mappings
            this.ToTable("vwCrm_Vote2");
            this.Property(t => t.Id).HasColumnName("Id");
            this.Property(t => t.CreatedUserId).HasColumnName("CreatedUserId");
            this.Property(t => t.CreatedDate).HasColumnName("CreatedDate");
            this.Property(t => t.ModifiedUserId).HasColumnName("ModifiedUserId");
            this.Property(t => t.ModifiedDate).HasColumnName("ModifiedDate");
            this.Property(t => t.AssignedUserId).HasColumnName("AssignedUserId");

            this.Property(t => t.CustomerId).HasColumnName("CustomerId");
            this.Property(t => t.InvoiceId).HasColumnName("InvoiceId");
            this.Property(t => t.QuestionId).HasColumnName("QuestionId");
            this.Property(t => t.AnswerId).HasColumnName("AnswerId");
            this.Property(t => t.UsingServiceLogDetailId).HasColumnName("UsingServiceLogDetailId");
            this.Property(t => t.Note).HasColumnName("Note");
            //vw
            this.Property(t => t.StaffName).HasColumnName("StaffName");
            this.Property(t => t.CustomerName).HasColumnName("CustomerName");
            this.Property(t => t.StaffId).HasColumnName("StaffId");
            this.Property(t => t.Type).HasColumnName("Type");
            this.Property(t => t.StaffCode).HasColumnName("StaffCode");
            this.Property(t => t.ProfileImage).HasColumnName("ProfileImage");

            this.Property(t => t.ProductInvoiceCode).HasColumnName("ProductInvoiceCode");
            this.Property(t => t.ProductInvoiceId).HasColumnName("ProductInvoiceId");
            this.Property(t => t.CustomerCode).HasColumnName("CustomerCode");
            this.Property(t => t.CustomerImage).HasColumnName("CustomerImage");
            this.Property(t => t.ServiceName).HasColumnName("ServiceName");
            this.Property(t => t.QuestionName).HasColumnName("QuestionName");
            this.Property(t => t.AnswerContent).HasColumnName("AnswerContent");
            this.Property(t => t.BranchId).HasColumnName("BranchId");
            this.Property(t => t.BranchName).HasColumnName("BranchName");
        }
    }
}
