using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Erp.Domain.Account.Entities.Mapping
{
    public class ContractSellMap : EntityTypeConfiguration<ContractSell>
    {
        public ContractSellMap()
        {
            // Primary Key
            this.HasKey(t => t.Id);

            // Properties
                        this.Property(t => t.Unit).HasMaxLength(50);
            this.Property(t => t.VAT).HasMaxLength(50);
            this.Property(t => t.UnitMoney).HasMaxLength(50);


            // Table & Column Mappings
            this.ToTable("Account_ContractSell");
            this.Property(t => t.Id).HasColumnName("Id");
            this.Property(t => t.CreatedUserId).HasColumnName("CreatedUserId");
            this.Property(t => t.CreatedDate).HasColumnName("CreatedDate");
            this.Property(t => t.ModifiedUserId).HasColumnName("ModifiedUserId");
            this.Property(t => t.ModifiedDate).HasColumnName("ModifiedDate");
            this.Property(t => t.AssignedUserId).HasColumnName("AssignedUserId");

            this.Property(t => t.CondosId).HasColumnName("CondosId");
            this.Property(t => t.Quantity).HasColumnName("Quantity");
            this.Property(t => t.Price).HasColumnName("Price");
            this.Property(t => t.Unit).HasColumnName("Unit");
            this.Property(t => t.VAT).HasColumnName("VAT");
            this.Property(t => t.MaintenanceCosts).HasColumnName("MaintenanceCosts");
            this.Property(t => t.UnitMoney).HasColumnName("UnitMoney");
            this.Property(t => t.DayHandOver).HasColumnName("DayHandOver");
            this.Property(t => t.DayPay).HasColumnName("DayPay");

        }
    }
}
