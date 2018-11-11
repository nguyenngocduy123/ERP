using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Erp.Domain.Sale.Entities.Mapping
{
    public class WarehouseMap : EntityTypeConfiguration<Warehouse>
    {
        public WarehouseMap()
        {
            // Primary Key
            this.HasKey(t => t.Id);

            // Properties
            this.Property(t => t.Name).HasMaxLength(150);
                        this.Property(t => t.Note).HasMaxLength(300);
            this.Property(t => t.Address).HasMaxLength(200);
            this.Property(t => t.WardId).HasMaxLength(50);
            this.Property(t => t.DistrictId).HasMaxLength(50);
            this.Property(t => t.CityId).HasMaxLength(10);


            // Table & Column Mappings
            this.ToTable("Sale_Warehouse");
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
            this.Property(t => t.BranchId).HasColumnName("BranchId");
            this.Property(t => t.KeeperId).HasColumnName("KeeperId");
            this.Property(t => t.IsSale).HasColumnName("IsSale");
            this.Property(t => t.Categories).HasColumnName("Categories");
        }
    }
}
