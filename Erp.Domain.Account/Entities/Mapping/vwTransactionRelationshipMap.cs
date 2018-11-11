using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Erp.Domain.Account.Entities.Mapping
{
    public class vwTransactionRelationshipMap : EntityTypeConfiguration<vwTransactionRelationship>
    {
        public vwTransactionRelationshipMap()
        {
            // Primary Key
            this.HasKey(t => t.Id);

            // Properties

            // Table & Column Mappings
            this.ToTable("vwAccount_TransactionRelationship");
            this.Property(t => t.Id).HasColumnName("Id");
            this.Property(t => t.CreatedUserId).HasColumnName("CreatedUserId");
            this.Property(t => t.CreatedDate).HasColumnName("CreatedDate");
            this.Property(t => t.ModifiedUserId).HasColumnName("ModifiedUserId");
            this.Property(t => t.ModifiedDate).HasColumnName("ModifiedDate");
            this.Property(t => t.AssignedUserId).HasColumnName("AssignedUserId");

            this.Property(t => t.TransactionA).HasColumnName("TransactionA");
            this.Property(t => t.TransactionB).HasColumnName("TransactionB");

            this.Property(t => t.TransactionA_Module).HasColumnName("TransactionA_Module");
            this.Property(t => t.TransactionA_Name).HasColumnName("TransactionA_Name");
            this.Property(t => t.TransactionB_Module).HasColumnName("TransactionB_Module");
            this.Property(t => t.TransactionB_Name).HasColumnName("TransactionB_Name");
        }
    }
}
