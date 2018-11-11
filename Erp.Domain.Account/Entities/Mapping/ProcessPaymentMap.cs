using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Erp.Domain.Account.Entities.Mapping
{
    public class ProcessPaymentMap : EntityTypeConfiguration<ProcessPayment>
    {
        public ProcessPaymentMap()
        {
            // Primary Key
            this.HasKey(t => t.Id);

            // Properties
            this.Property(t => t.Name).HasMaxLength(150);
                        this.Property(t => t.FormPayment).HasMaxLength(150);
            this.Property(t => t.CodeTrading).HasMaxLength(100);
            this.Property(t => t.Bank).HasMaxLength(150);
            this.Property(t => t.Payer).HasMaxLength(200);


            // Table & Column Mappings
            this.ToTable("Account_ProcessPayment");
            this.Property(t => t.Id).HasColumnName("Id");
            this.Property(t => t.CreatedUserId).HasColumnName("CreatedUserId");
            this.Property(t => t.CreatedDate).HasColumnName("CreatedDate");
            this.Property(t => t.ModifiedUserId).HasColumnName("ModifiedUserId");
            this.Property(t => t.ModifiedDate).HasColumnName("ModifiedDate");
            this.Property(t => t.AssignedUserId).HasColumnName("AssignedUserId");
            this.Property(t => t.Name).HasColumnName("Name");

            this.Property(t => t.OrderNo).HasColumnName("OrderNo");
            this.Property(t => t.DayPayment).HasColumnName("DayPayment");
            this.Property(t => t.MoneyPayment).HasColumnName("MoneyPayment");
            this.Property(t => t.FormPayment).HasColumnName("FormPayment");
            this.Property(t => t.CodeTrading).HasColumnName("CodeTrading");
            this.Property(t => t.Bank).HasColumnName("Bank");
            this.Property(t => t.Payer).HasColumnName("Payer");
            this.Property(t => t.ContractId).HasColumnName("ContractId");
            this.Property(t => t.Status).HasColumnName("Status");
            this.Property(t => t.TransactionCode).HasColumnName("TransactionCode");
        }
    }
}
