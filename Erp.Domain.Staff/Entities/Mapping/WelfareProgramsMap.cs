using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Erp.Domain.Staff.Entities.Mapping
{
    public class WelfareProgramsMap : EntityTypeConfiguration<WelfarePrograms>
    {
        public WelfareProgramsMap()
        {
            // Primary Key
            this.HasKey(t => t.Id);

            // Properties
            this.Property(t => t.Name).HasMaxLength(150);
                        this.Property(t => t.Note).HasMaxLength(250);
            this.Property(t => t.Status).HasMaxLength(50);
            this.Property(t => t.Purpose).HasMaxLength(250);
            this.Property(t => t.Formality).HasMaxLength(50);
            this.Property(t => t.Address).HasMaxLength(250);
            this.Property(t => t.Category).HasMaxLength(50);
            this.Property(t => t.ApplicationObject).HasMaxLength(50);


            // Table & Column Mappings
            this.ToTable("Staff_WelfarePrograms");
            this.Property(t => t.Id).HasColumnName("Id");
            this.Property(t => t.CreatedUserId).HasColumnName("CreatedUserId");
            this.Property(t => t.CreatedDate).HasColumnName("CreatedDate");
            this.Property(t => t.ModifiedUserId).HasColumnName("ModifiedUserId");
            this.Property(t => t.ModifiedDate).HasColumnName("ModifiedDate");
            this.Property(t => t.AssignedUserId).HasColumnName("AssignedUserId");
            this.Property(t => t.Name).HasColumnName("Name");
            this.Property(t => t.Code).HasColumnName("Code");
            this.Property(t => t.ProvideStartDate).HasColumnName("ProvideStartDate");
            this.Property(t => t.ProvideEndDate).HasColumnName("ProvideEndDate");
            this.Property(t => t.Quantity).HasColumnName("Quantity");
            this.Property(t => t.TotalEstimatedCost).HasColumnName("TotalEstimatedCost");
            this.Property(t => t.Note).HasColumnName("Note");
            this.Property(t => t.Status).HasColumnName("Status");
            this.Property(t => t.Purpose).HasColumnName("Purpose");
            this.Property(t => t.Formality).HasColumnName("Formality");
            this.Property(t => t.Address).HasColumnName("Address");
            this.Property(t => t.Category).HasColumnName("Category");
            this.Property(t => t.RegistrationStartDate).HasColumnName("RegistrationStartDate");
            this.Property(t => t.RegistrationEndDate).HasColumnName("RegistrationEndDate");
            this.Property(t => t.ImplementationStartDate).HasColumnName("ImplementationStartDate");
            this.Property(t => t.ImplementationEndDate).HasColumnName("ImplementationEndDate");
            this.Property(t => t.MoneyStaff).HasColumnName("MoneyStaff");
            this.Property(t => t.MoneyCompany).HasColumnName("MoneyCompany");
            this.Property(t => t.TotalStaffCompany).HasColumnName("TotalStaffCompany");
            this.Property(t => t.TotalMoneyStaff).HasColumnName("TotalMoneyStaff");
            this.Property(t => t.TotalMoneyCompany).HasColumnName("TotalMoneyCompany");
            this.Property(t => t.TotalStaffCompanyAll).HasColumnName("TotalStaffCompanyAll");
            this.Property(t => t.TotalActualCosts).HasColumnName("TotalActualCosts");
            this.Property(t => t.ApplicationObject).HasColumnName("ApplicationObject");

        }
    }
}
