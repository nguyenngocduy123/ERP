using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Erp.Domain.Staff.Entities.Mapping
{
    public class vwTaxIncomePersonDetailMap : EntityTypeConfiguration<vwTaxIncomePersonDetail>
    {
        public vwTaxIncomePersonDetailMap()
        {
            // Primary Key
            this.HasKey(t => t.Id);

            // Properties
            //this.Property(t => t.Name).HasMaxLength(150);
            

            // Table & Column Mappings
            this.ToTable("vwStaff_TaxIncomePersonDetail");
            this.Property(t => t.Id).HasColumnName("Id");
            this.Property(t => t.CreatedUserId).HasColumnName("CreatedUserId");
            this.Property(t => t.CreatedDate).HasColumnName("CreatedDate");
            this.Property(t => t.ModifiedUserId).HasColumnName("ModifiedUserId");
            this.Property(t => t.ModifiedDate).HasColumnName("ModifiedDate");
            this.Property(t => t.AssignedUserId).HasColumnName("AssignedUserId");
            this.Property(t => t.StaffId).HasColumnName("StaffId");

            this.Property(t => t.TaxIncomePersonId).HasColumnName("TaxIncomePersonId");
            this.Property(t => t.Code).HasColumnName("Code");
            this.Property(t => t.CountryId).HasColumnName("CountryId");
            this.Property(t => t.DistrictName).HasColumnName("DistrictName");
            this.Property(t => t.Email).HasColumnName("Email");
            this.Property(t => t.Gender).HasColumnName("Gender");
            this.Property(t => t.GenderName).HasColumnName("GenderName");
            this.Property(t => t.IdCardNumber).HasColumnName("IdCardNumber");
            this.Property(t => t.PositionName).HasColumnName("PositionName");
            this.Property(t => t.ProvinceName).HasColumnName("ProvinceName");
            this.Property(t => t.Sale_BranchId).HasColumnName("Sale_BranchId");
            this.Property(t => t.WardName).HasColumnName("WardName");

        }
    }
}
