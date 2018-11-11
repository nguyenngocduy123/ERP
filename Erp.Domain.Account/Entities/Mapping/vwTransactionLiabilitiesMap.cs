using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Erp.Domain.Account.Entities.Mapping
{
    public class vwTransactionLiabilitiesMap : EntityTypeConfiguration<vwTransactionLiabilities>
    {
        public vwTransactionLiabilitiesMap()
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
            this.ToTable("vwAccount_TransactionLiabilities");
            this.Property(t => t.Id).HasColumnName("Id");
            this.Property(t => t.CreatedUserId).HasColumnName("CreatedUserId");
            this.Property(t => t.CreatedDate).HasColumnName("CreatedDate");
            this.Property(t => t.ModifiedUserId).HasColumnName("ModifiedUserId");
            this.Property(t => t.ModifiedDate).HasColumnName("ModifiedDate");
            this.Property(t => t.AssignedUserId).HasColumnName("AssignedUserId");

            this.Property(t => t.TransactionCode).HasColumnName("TransactionCode");
            this.Property(t => t.TransactionModule).HasColumnName("TransactionModule");
            this.Property(t => t.TransactionName).HasColumnName("TransactionName");
            this.Property(t => t.TargetModule).HasColumnName("TargetModule");
            this.Property(t => t.TargetCode).HasColumnName("TargetCode");
            this.Property(t => t.Debit).HasColumnName("Debit");
            this.Property(t => t.Credit).HasColumnName("Credit");
            this.Property(t => t.NextPaymentDate).HasColumnName("NextPaymentDate");
            this.Property(t => t.PaymentMethod).HasColumnName("PaymentMethod");
            this.Property(t => t.Phone).HasColumnName("Phone");
            this.Property(t => t.TargetName).HasColumnName("TargetName");
            this.Property(t => t.MaChungTuGoc).HasColumnName("MaChungTuGoc");
            this.Property(t => t.LoaiChungTuGoc).HasColumnName("LoaiChungTuGoc");
            this.Property(t => t.Note).HasColumnName("Note");
        }
    }
}
