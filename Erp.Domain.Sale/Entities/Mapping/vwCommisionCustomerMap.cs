using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Erp.Domain.Sale.Entities.Mapping
{
    public class vwCommisionCustomerMap : EntityTypeConfiguration<vwCommisionCustomer>
    {
        public vwCommisionCustomerMap()
        {
            // Primary Key
            this.HasKey(t => t.Id);

            // Properties


            // Table & Column Mappings
            this.ToTable("vwSale_Commision_Customer");
            this.Property(t => t.Id).HasColumnName("Id");
            this.Property(t => t.CreatedUserId).HasColumnName("CreatedUserId");
            this.Property(t => t.CreatedDate).HasColumnName("CreatedDate");
            this.Property(t => t.ModifiedUserId).HasColumnName("ModifiedUserId");
            this.Property(t => t.ModifiedDate).HasColumnName("ModifiedDate");

            this.Property(t => t.CommissionCusId).HasColumnName("CommissionCusId");
            this.Property(t => t.ProductId).HasColumnName("ProductId");
            this.Property(t => t.CommissionValue).HasColumnName("CommissionValue");
            this.Property(t => t.IsMoney).HasColumnName("IsMoney");
            this.Property(t => t.Type).HasColumnName("Type");
            this.Property(t => t.StartDate).HasColumnName("StartDate");
            this.Property(t => t.EndDate).HasColumnName("EndDate");
            this.Property(t => t.ApplyFor).HasColumnName("ApplyFor");
            this.Property(t => t.CategoryCode).HasColumnName("CategoryCode");
            this.Property(t => t.ProductCode).HasColumnName("ProductCode");
            this.Property(t => t.ProductName).HasColumnName("ProductName");
            this.Property(t => t.Price).HasColumnName("Price");
        }
    }
}
