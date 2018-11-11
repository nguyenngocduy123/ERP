using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Erp.Domain.Staff.Entities.Mapping
{
    public class DotBCBHXHDetailMap : EntityTypeConfiguration<DotBCBHXHDetail>
    {
        public DotBCBHXHDetailMap()
        {
            // Primary Key
            this.HasKey(t => t.Id);

            // Properties
            this.Property(t => t.Name).HasMaxLength(150);
            this.Property(t => t.Type).HasMaxLength(50);


            // Table & Column Mappings
            this.ToTable("Staff_DotBCBHXHDetail");
            this.Property(t => t.Id).HasColumnName("Id");
            this.Property(t => t.CreatedUserId).HasColumnName("CreatedUserId");
            this.Property(t => t.CreatedDate).HasColumnName("CreatedDate");
            this.Property(t => t.ModifiedUserId).HasColumnName("ModifiedUserId");
            this.Property(t => t.ModifiedDate).HasColumnName("ModifiedDate");
            this.Property(t => t.AssignedUserId).HasColumnName("AssignedUserId");
            this.Property(t => t.Name).HasColumnName("Name");

            this.Property(t => t.DotBCBHXHId).HasColumnName("DotBCBHXHId");
            this.Property(t => t.SocialInsuranceId).HasColumnName("SocialInsuranceId");
            this.Property(t => t.Type).HasColumnName("Type");
            this.Property(t => t.StaffId).HasColumnName("StaffId");
            this.Property(t => t.MedicalCode).HasColumnName("MedicalCode");
            this.Property(t => t.MedicalStartDate).HasColumnName("MedicalStartDate");
            this.Property(t => t.MedicalEndDate).HasColumnName("MedicalEndDate");
            this.Property(t => t.MedicalIssue).HasColumnName("MedicalIssue");
            this.Property(t => t.MedicalDefaultValue).HasColumnName("MedicalDefaultValue");
            this.Property(t => t.SocietyCode).HasColumnName("SocietyCode");
            this.Property(t => t.SocietyStartDate).HasColumnName("SocietyStartDate");
            this.Property(t => t.SocietyEndDate).HasColumnName("SocietyEndDate");
            this.Property(t => t.SocietyIssue).HasColumnName("SocietyIssue");
            this.Property(t => t.SocietyDefaultValue).HasColumnName("SocietyDefaultValue");
            this.Property(t => t.Note).HasColumnName("Note");
            this.Property(t => t.Status).HasColumnName("Status");
            this.Property(t => t.PC_CV).HasColumnName("PC_CV");
            this.Property(t => t.PC_TNN).HasColumnName("PC_TNN");
            this.Property(t => t.PC_TNVK).HasColumnName("PC_TNVK");
            this.Property(t => t.PC_Khac).HasColumnName("PC_Khac");
            this.Property(t => t.TienLuong).HasColumnName("TienLuong");
        }
    }
}
