using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Erp.Domain.Account.Entities.Mapping
{
    public class vwTransactionMap : EntityTypeConfiguration<vwTransaction>
    {
        public vwTransactionMap()
        {
            // Primary Key
            this.HasKey(t => t.Id);

            // Properties
            //this.Property(t => t.TransactionCode).HasMaxLength(20);
            //this.Property(t => t.TransactionType).HasMaxLength(50);
            //this.Property(t => t.TargetType).HasMaxLength(50);
            //this.Property(t => t.TargetCode).HasMaxLength(20);
            //this.Property(t => t.Status).HasMaxLength(100);


            // Table & Column Mappings
            this.ToTable("vwAccount_Transaction");
            this.Property(t => t.Id).HasColumnName("Id");
            this.Property(t => t.CreatedUserId).HasColumnName("CreatedUserId");
            this.Property(t => t.CreatedDate).HasColumnName("CreatedDate");
            this.Property(t => t.ModifiedUserId).HasColumnName("ModifiedUserId");
            this.Property(t => t.ModifiedDate).HasColumnName("ModifiedDate");
            this.Property(t => t.AssignedUserId).HasColumnName("AssignedUserId");
            this.Property(t => t.TransactionCode).HasColumnName("TransactionCode");

            this.Property(t => t.TransactionType).HasColumnName("TransactionType");
            this.Property(t => t.TransactionTypeName).HasColumnName("TransactionTypeName");
            this.Property(t => t.TargetType).HasColumnName("TargetType");
            this.Property(t => t.TargetCode).HasColumnName("TargetCode");
            this.Property(t => t.Total).HasColumnName("Total");
            this.Property(t => t.Payment).HasColumnName("Payment");
            this.Property(t => t.Remain).HasColumnName("Remain");
            this.Property(t => t.ParentId).HasColumnName("ParentId");
            this.Property(t => t.TargetName).HasColumnName("TargetName");
            this.Property(t => t.NextPaymentDate).HasColumnName("NextPaymentDate");
            this.Property(t => t.PaymentMethod).HasColumnName("PaymentMethod");
        }
    }
}
