using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Erp.Domain.Account.Entities.Mapping
{
    public class TransactionMap : EntityTypeConfiguration<Transaction>
    {
        public TransactionMap()
        {
            // Primary Key
            this.HasKey(t => t.Id);

            // Properties
            this.Property(t => t.TransactionModule).HasMaxLength(20);
            this.Property(t => t.TransactionCode).HasMaxLength(20);
            this.Property(t => t.TransactionName).HasMaxLength(150);


            // Table & Column Mappings
            this.ToTable("Account_Transaction");
            this.Property(t => t.Id).HasColumnName("Id");
            this.Property(t => t.CreatedUserId).HasColumnName("CreatedUserId");
            this.Property(t => t.CreatedDate).HasColumnName("CreatedDate");
            this.Property(t => t.ModifiedUserId).HasColumnName("ModifiedUserId");
            this.Property(t => t.ModifiedDate).HasColumnName("ModifiedDate");
            this.Property(t => t.AssignedUserId).HasColumnName("AssignedUserId");

            this.Property(t => t.TransactionModule).HasColumnName("TransactionModule");
            this.Property(t => t.TransactionCode).HasColumnName("TransactionCode");
            this.Property(t => t.TransactionName).HasColumnName("TransactionName");

        }
    }
}
