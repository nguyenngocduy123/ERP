using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Erp.Domain.Staff.Entities.Mapping
{
    public class BranchMap : EntityTypeConfiguration<Branch>
    {
        public BranchMap()
        {
            // Primary Key
            this.HasKey(t => t.Id);

            // Properties
            this.Property(t => t.Name).HasMaxLength(150);
                        this.Property(t => t.Code).HasMaxLength(20);
            this.Property(t => t.Address).HasMaxLength(100);
            this.Property(t => t.WardId).HasMaxLength(10);
            this.Property(t => t.DistrictId).HasMaxLength(10);
            this.Property(t => t.CityId).HasMaxLength(10);
            this.Property(t => t.Phone).HasMaxLength(15);
            this.Property(t => t.Email).HasMaxLength(50);
            this.Property(t => t.Type).HasMaxLength(20);


            // Table & Column Mappings
            this.ToTable("Staff_Branch");
            this.Property(t => t.Id).HasColumnName("Id");
            this.Property(t => t.CreatedUserId).HasColumnName("CreatedUserId");
            this.Property(t => t.CreatedDate).HasColumnName("CreatedDate");
            this.Property(t => t.ModifiedUserId).HasColumnName("ModifiedUserId");
            this.Property(t => t.ModifiedDate).HasColumnName("ModifiedDate");
            this.Property(t => t.Name).HasColumnName("Name");

            this.Property(t => t.Code).HasColumnName("Code");
            this.Property(t => t.Address).HasColumnName("Address");
            this.Property(t => t.WardId).HasColumnName("WardId");
            this.Property(t => t.DistrictId).HasColumnName("DistrictId");
            this.Property(t => t.CityId).HasColumnName("CityId");
            this.Property(t => t.Phone).HasColumnName("Phone");
            this.Property(t => t.Email).HasColumnName("Email");
            this.Property(t => t.Type).HasColumnName("Type");
            this.Property(t => t.MaxDebitAmount).HasColumnName("MaxDebitAmount");
            this.Property(t => t.ParentId).HasColumnName("ParentId");
            this.Property(t => t.CooperationDay).HasColumnName("CooperationDay");
        }
    }
}
