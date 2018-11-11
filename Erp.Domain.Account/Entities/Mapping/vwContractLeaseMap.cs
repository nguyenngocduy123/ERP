using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Erp.Domain.Account.Entities.Mapping
{
    public class vwContractLeaseMap : EntityTypeConfiguration<vwContractLease>
    {
        public vwContractLeaseMap()
        {
            // Primary Key
            this.HasKey(t => t.Id);

            // Properties
                        this.Property(t => t.Unit).HasMaxLength(50);
            this.Property(t => t.UnitMoney).HasMaxLength(50);


            // Table & Column Mappings
            this.ToTable("vwAccount_ContractLease");
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
            this.Property(t => t.UnitMoney).HasColumnName("UnitMoney");
            this.Property(t => t.DayHandOver).HasColumnName("DayHandOver");
            this.Property(t => t.EffectiveDate).HasColumnName("EffectiveDate");
            this.Property(t => t.ExpiryDate).HasColumnName("ExpiryDate");
            this.Property(t => t.DayPay).HasColumnName("DayPay");

            //view condos
            this.Property(t => t.NameCondos).HasColumnName("NameCondos");

            this.Property(t => t.ProjectId).HasColumnName("ProjectId");
            this.Property(t => t.BlockId).HasColumnName("BlockId");
            this.Property(t => t.FloorId).HasColumnName("FloorId");
            this.Property(t => t.CodeCondos).HasColumnName("CodeCondos");
            this.Property(t => t.Area).HasColumnName("Area");
            this.Property(t => t.PriceCondos).HasColumnName("PriceCondos");
            this.Property(t => t.NumbersOfLivingRoom).HasColumnName("NumbersOfLivingRoom");
            this.Property(t => t.NumbersOfBedRoom).HasColumnName("NumbersOfBedRoom");
            this.Property(t => t.NumbersOfKitchenRoom).HasColumnName("NumbersOfKitchenRoom");
            this.Property(t => t.NumbersOfToilet).HasColumnName("NumbersOfToilet");
            this.Property(t => t.NumbersOfBalcony).HasColumnName("NumbersOfBalcony");
            this.Property(t => t.Orientation).HasColumnName("Orientation");
            this.Property(t => t.Status).HasColumnName("Status");
            //this.Property(t => t.Currency).HasColumnName("Currency");
            this.Property(t => t.ProjectName).HasColumnName("ProjectName");
            this.Property(t => t.BlockName).HasColumnName("BlockName");
            this.Property(t => t.FloorName).HasColumnName("FloorName");

        }
    }
}
