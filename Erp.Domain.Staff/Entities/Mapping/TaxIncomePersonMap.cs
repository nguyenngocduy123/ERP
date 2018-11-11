using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Erp.Domain.Staff.Entities.Mapping
{
    public class TaxIncomePersonMap : EntityTypeConfiguration<TaxIncomePerson>
    {
        public TaxIncomePersonMap()
        {
            // Primary Key
            this.HasKey(t => t.Id);

            // Properties
            this.Property(t => t.Name).HasMaxLength(150);
            

            // Table & Column Mappings
            this.ToTable("Staff_TaxIncomePerson");
            this.Property(t => t.Id).HasColumnName("Id");
            this.Property(t => t.CreatedUserId).HasColumnName("CreatedUserId");
            this.Property(t => t.CreatedDate).HasColumnName("CreatedDate");
            this.Property(t => t.ModifiedUserId).HasColumnName("ModifiedUserId");
            this.Property(t => t.ModifiedDate).HasColumnName("ModifiedDate");
            this.Property(t => t.AssignedUserId).HasColumnName("AssignedUserId");
            this.Property(t => t.Name).HasColumnName("Name");
            this.Property(t => t.GeneralManageId).HasColumnName("GeneralManageId");
            this.Property(t => t.GeneralTaxationId).HasColumnName("GeneralTaxationId");
            this.Property(t => t.StaffEndDate).HasColumnName("StaffEndDate");
            this.Property(t => t.StaffStartDate).HasColumnName("StaffStartDate");
            this.Property(t => t.Code).HasColumnName("Code");


        }
    }
}
