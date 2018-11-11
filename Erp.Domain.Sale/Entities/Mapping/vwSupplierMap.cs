using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Erp.Domain.Sale.Entities.Mapping
{
    public class vwSupplierMap : EntityTypeConfiguration<vwSupplier>
    {
        public vwSupplierMap()
        {
            // Primary Key
            this.HasKey(t => t.Id);

            // Properties
            this.Property(t => t.Name).HasMaxLength(150);
                        this.Property(t => t.Code).HasMaxLength(20);
            this.Property(t => t.Note).HasMaxLength(300);
            this.Property(t => t.Address).HasMaxLength(100);
            this.Property(t => t.WardId).HasMaxLength(10);
            this.Property(t => t.DistrictId).HasMaxLength(10);
            this.Property(t => t.CityId).HasMaxLength(10);
            this.Property(t => t.Phone).HasMaxLength(15);
            this.Property(t => t.Mobile).HasMaxLength(15);
            this.Property(t => t.Email).HasMaxLength(50);
            this.Property(t => t.CompanyName).HasMaxLength(150);


            // Table & Column Mappings
            this.ToTable("vwSale_Supplier");
            this.Property(t => t.Id).HasColumnName("Id");
            this.Property(t => t.CreatedUserId).HasColumnName("CreatedUserId");
            this.Property(t => t.CreatedDate).HasColumnName("CreatedDate");
            this.Property(t => t.ModifiedUserId).HasColumnName("ModifiedUserId");
            this.Property(t => t.ModifiedDate).HasColumnName("ModifiedDate");
            this.Property(t => t.Name).HasColumnName("Name");

            this.Property(t => t.Code).HasColumnName("Code");
            this.Property(t => t.Note).HasColumnName("Note");
            this.Property(t => t.Address).HasColumnName("Address");
            this.Property(t => t.WardId).HasColumnName("WardId");
            this.Property(t => t.DistrictId).HasColumnName("DistrictId");
            this.Property(t => t.CityId).HasColumnName("CityId");
            this.Property(t => t.Phone).HasColumnName("Phone");
            this.Property(t => t.Mobile).HasColumnName("Mobile");
            this.Property(t => t.Email).HasColumnName("Email");
            this.Property(t => t.CompanyName).HasColumnName("CompanyName");
            this.Property(t => t.Type).HasColumnName("Type");
            this.Property(t => t.Fields).HasColumnName("Fields");
            this.Property(t => t.TaxCode).HasColumnName("TaxCode");
            this.Property(t => t.ProductIdOfSupplier).HasColumnName("ProductIdOfSupplier");
            this.Property(t => t.ProvinceName).HasColumnName("ProvinceName");
            this.Property(t => t.DistrictName).HasColumnName("DistrictName");
            this.Property(t => t.WardName).HasColumnName("WardName");
        }
    }
}
