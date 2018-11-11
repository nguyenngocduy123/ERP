using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Erp.Domain.Account.Entities.Mapping
{
    public class vwContactMap : EntityTypeConfiguration<vwContact>
    {
        public vwContactMap()
        {
            // Primary Key
            this.HasKey(t => t.Id);

            // Properties
            this.Property(t => t.FirstName).HasMaxLength(50);
            this.Property(t => t.LastName).HasMaxLength(150);
            this.Property(t => t.Note).HasMaxLength(300);
            this.Property(t => t.Address).HasMaxLength(100);
            this.Property(t => t.WardId).HasMaxLength(10);
            this.Property(t => t.DistrictId).HasMaxLength(10);
            this.Property(t => t.CityId).HasMaxLength(10);
            this.Property(t => t.Phone).HasMaxLength(15);
            this.Property(t => t.Mobile).HasMaxLength(15);
            this.Property(t => t.Email).HasMaxLength(50);


            // Table & Column Mappings
            this.ToTable("vwSale_Contact");
            this.Property(t => t.Id).HasColumnName("Id");
            this.Property(t => t.CreatedUserId).HasColumnName("CreatedUserId");
            this.Property(t => t.CreatedDate).HasColumnName("CreatedDate");
            this.Property(t => t.ModifiedUserId).HasColumnName("ModifiedUserId");
            this.Property(t => t.ModifiedDate).HasColumnName("ModifiedDate");

            this.Property(t => t.CustomerId).HasColumnName("CustomerId");
            this.Property(t => t.FirstName).HasColumnName("FirstName");
            this.Property(t => t.LastName).HasColumnName("LastName");
            this.Property(t => t.Birthday).HasColumnName("Birthday");
            this.Property(t => t.Gender).HasColumnName("Gender");
            this.Property(t => t.Note).HasColumnName("Note");
            this.Property(t => t.Address).HasColumnName("Address");
            this.Property(t => t.WardId).HasColumnName("WardId");
            this.Property(t => t.DistrictId).HasColumnName("DistrictId");
            this.Property(t => t.CityId).HasColumnName("CityId");
            this.Property(t => t.Phone).HasColumnName("Phone");
            this.Property(t => t.Mobile).HasColumnName("Mobile");
            this.Property(t => t.Email).HasColumnName("Email");
            this.Property(t => t.SupplierId).HasColumnName("SupplierId");
            this.Property(t => t.DepartmentName).HasColumnName("DepartmentName");
            this.Property(t => t.Position).HasColumnName("Position");

            this.Property(t => t.ProvinceName).HasColumnName("ProvinceName");
            this.Property(t => t.DistrictName).HasColumnName("DistrictName");
            this.Property(t => t.WardName).HasColumnName("WardName");
            this.Property(t => t.GenderName).HasColumnName("GenderName");
        }
    }
}
