using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Erp.Domain.Account.Entities.Mapping
{
    public class InfoPartyAMap : EntityTypeConfiguration<InfoPartyA>
    {
        public InfoPartyAMap()
        {
            // Primary Key
            this.HasKey(t => t.Id);

            // Properties
            this.Property(t => t.Name).HasMaxLength(150);
            this.Property(t => t.NamePrefix).HasMaxLength(150);
            this.Property(t => t.Position).HasMaxLength(50);
            this.Property(t => t.Address).HasMaxLength(300);
            this.Property(t => t.IdCardNumber).HasMaxLength(20);
            this.Property(t => t.IdCardIssued).HasMaxLength(10);
            this.Property(t => t.Phone).HasMaxLength(20);
            this.Property(t => t.Fax).HasMaxLength(50);
            this.Property(t => t.AccountNumber).HasMaxLength(20);
            this.Property(t => t.Bank).HasMaxLength(50);
            this.Property(t => t.TaxCode).HasMaxLength(50);


            // Table & Column Mappings
            this.ToTable("Account_InfoPartyA");
            this.Property(t => t.Id).HasColumnName("Id");
            this.Property(t => t.CreatedUserId).HasColumnName("CreatedUserId");
            this.Property(t => t.CreatedDate).HasColumnName("CreatedDate");
            this.Property(t => t.ModifiedUserId).HasColumnName("ModifiedUserId");
            this.Property(t => t.ModifiedDate).HasColumnName("ModifiedDate");
            this.Property(t => t.AssignedUserId).HasColumnName("AssignedUserId");
            this.Property(t => t.Name).HasColumnName("Name");
            this.Property(t => t.CompanyName).HasColumnName("CompanyName");
            this.Property(t => t.NamePrefix).HasColumnName("NamePrefix");
            this.Property(t => t.Birthday).HasColumnName("Birthday");
            this.Property(t => t.Position).HasColumnName("Position");
            this.Property(t => t.Address).HasColumnName("Address");
            this.Property(t => t.IdCardNumber).HasColumnName("IdCardNumber");
            this.Property(t => t.IdCardIssued).HasColumnName("IdCardIssued");
            this.Property(t => t.IdCardDate).HasColumnName("IdCardDate");
            this.Property(t => t.Phone).HasColumnName("Phone");
            this.Property(t => t.Fax).HasColumnName("Fax");
            this.Property(t => t.AccountNumber).HasColumnName("AccountNumber");
            this.Property(t => t.Bank).HasColumnName("Bank");
            this.Property(t => t.TaxCode).HasColumnName("TaxCode");
            this.Property(t => t.ProvinceId).HasColumnName("ProvinceId");
            this.Property(t => t.DistrictId).HasColumnName("DistrictId");
            this.Property(t => t.WardId).HasColumnName("WardId");
        }
    }
}
