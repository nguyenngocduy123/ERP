using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Erp.Domain.Sale.Entities.Mapping
{
    public class TotalDiscountMoneyNTMap : EntityTypeConfiguration<TotalDiscountMoneyNT>
    {
        public TotalDiscountMoneyNTMap()
        {
            // Primary Key
            this.HasKey(t => t.Id);

            // Properties

                        this.Property(t => t.Status).HasMaxLength(50);


            // Table & Column Mappings
            this.ToTable("Sale_TotalDiscountMoneyNT");
            this.Property(t => t.Id).HasColumnName("Id");
            this.Property(t => t.CreatedUserId).HasColumnName("CreatedUserId");
            this.Property(t => t.CreatedDate).HasColumnName("CreatedDate");
            this.Property(t => t.ModifiedUserId).HasColumnName("ModifiedUserId");
            this.Property(t => t.ModifiedDate).HasColumnName("ModifiedDate");
            this.Property(t => t.AssignedUserId).HasColumnName("AssignedUserId");
            this.Property(t => t.DrugStoreId).HasColumnName("DrugStoreId");
            this.Property(t => t.BranchId).HasColumnName("BranchId");
            this.Property(t => t.UserManagerId).HasColumnName("UserManagerId");
            this.Property(t => t.Month).HasColumnName("Month");
            this.Property(t => t.Year).HasColumnName("Year");
            this.Property(t => t.QuantityDay).HasColumnName("QuantityDay");
            this.Property(t => t.PercentDecrease).HasColumnName("PercentDecrease");
            this.Property(t => t.DiscountAmount).HasColumnName("DiscountAmount");
            this.Property(t => t.DecreaseAmount ).HasColumnName("DecreaseAmount ");
            this.Property(t => t.RemainingAmount).HasColumnName("RemainingAmount");
            this.Property(t => t.Status).HasColumnName("Status");

        }
    }
}
