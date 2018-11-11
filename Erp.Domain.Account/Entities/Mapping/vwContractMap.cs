using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Erp.Domain.Account.Entities.Mapping
{
    public class vwContractMap : EntityTypeConfiguration<vwContract>
    {
        public vwContractMap()
        {
            // Primary Key
            this.HasKey(t => t.Id);

            // Properties
            this.Property(t => t.Type).HasMaxLength(20);
            this.Property(t => t.Code).HasMaxLength(50);
            this.Property(t => t.Place).HasMaxLength(150);
            this.Property(t => t.TemplateFile).HasMaxLength(150);


            // Table & Column Mappings
            this.ToTable("vwAccount_Contract");
            this.Property(t => t.Id).HasColumnName("Id");
            this.Property(t => t.CreatedUserId).HasColumnName("CreatedUserId");
            this.Property(t => t.CreatedDate).HasColumnName("CreatedDate");
            this.Property(t => t.ModifiedUserId).HasColumnName("ModifiedUserId");
            this.Property(t => t.ModifiedDate).HasColumnName("ModifiedDate");
            this.Property(t => t.AssignedUserId).HasColumnName("AssignedUserId");

            this.Property(t => t.Type).HasColumnName("Type");
            this.Property(t => t.Code).HasColumnName("Code");
            this.Property(t => t.Place).HasColumnName("Place");
            this.Property(t => t.EffectiveDate).HasColumnName("EffectiveDate");
            this.Property(t => t.ContractQuantity).HasColumnName("ContractQuantity");
            this.Property(t => t.TemplateFile).HasColumnName("TemplateFile");
            this.Property(t => t.InfoPartyAId).HasColumnName("InfoPartyAId");
            this.Property(t => t.IdTypeContract).HasColumnName("IdTypeContract");
            this.Property(t => t.CustomerId).HasColumnName("CustomerId");
            this.Property(t => t.TransactionCode).HasColumnName("TransactionCode");
            this.Property(t => t.Status).HasColumnName("Status");
            //view
            this.Property(t => t.CustomerName).HasColumnName("CustomerName");
            this.Property(t => t.InfoPartyAName).HasColumnName("InfoPartyAName");
            this.Property(t => t.InfoPartyCompanyName).HasColumnName("InfoPartyCompanyName");
        }
    }
}
