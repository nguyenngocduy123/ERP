using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Erp.Domain.Staff.Entities.Mapping
{
    public class StaffsMap : EntityTypeConfiguration<Staffs>
    {
        public StaffsMap()
        {
            // Primary Key
            this.HasKey(t => t.Id);

            // Properties
            this.Property(t => t.Name).HasMaxLength(150);
                        this.Property(t => t.Code).HasMaxLength(50);
            this.Property(t => t.Address).HasMaxLength(150);
            this.Property(t => t.Phone).HasMaxLength(20);
            this.Property(t => t.IdCardNumber).HasMaxLength(20);
            this.Property(t => t.IdCardIssued).HasMaxLength(10);
            this.Property(t => t.Ethnic).HasMaxLength(50);
            this.Property(t => t.Religion).HasMaxLength(50);
            this.Property(t => t.Email).HasMaxLength(50);
            this.Property(t => t.MaritalStatus).HasMaxLength(50);


            // Table & Column Mappings
            this.ToTable("Staff_Staff");
            this.Property(t => t.Id).HasColumnName("Id");
            this.Property(t => t.CreatedUserId).HasColumnName("CreatedUserId");
            this.Property(t => t.CreatedDate).HasColumnName("CreatedDate");
            this.Property(t => t.ModifiedUserId).HasColumnName("ModifiedUserId");
            this.Property(t => t.ModifiedDate).HasColumnName("ModifiedDate");
            this.Property(t => t.Name).HasColumnName("Name");

            this.Property(t => t.Code).HasColumnName("Code");
            this.Property(t => t.Birthday).HasColumnName("Birthday");
            this.Property(t => t.Gender).HasColumnName("Gender");
            this.Property(t => t.Address).HasColumnName("Address");
            this.Property(t => t.Phone).HasColumnName("Phone");
            this.Property(t => t.IdCardNumber).HasColumnName("IdCardNumber");
            this.Property(t => t.IdCardDate).HasColumnName("IdCardDate");
            this.Property(t => t.IdCardIssued).HasColumnName("IdCardIssued");
            this.Property(t => t.Ethnic).HasColumnName("Ethnic");
            this.Property(t => t.Religion).HasColumnName("Religion");
            this.Property(t => t.Email).HasColumnName("Email");
            this.Property(t => t.Literacy).HasColumnName("Literacy");
            this.Property(t => t.Technique).HasColumnName("Technique");
            this.Property(t => t.Language).HasColumnName("Language");
            //this.Property(t => t.BranchDepartmentId).HasColumnName("BranchDepartmentId");
            //this.Property(t => t.Position).HasColumnName("Position");
            this.Property(t => t.MaritalStatus).HasColumnName("MaritalStatus");
            this.Property(t => t.ProvinceId).HasColumnName("ProvinceId");
            this.Property(t => t.DistrictId).HasColumnName("DistrictId");
            this.Property(t => t.WardId).HasColumnName("WardId");
            this.Property(t => t.ProfileImage).HasColumnName("ProfileImage");
            this.Property(t => t.CountryId).HasColumnName("CountryId");
            this.Property(t => t.UserId).HasColumnName("UserId");
            this.Property(t => t.Email2).HasColumnName("Email2");
            this.Property(t => t.Phone2).HasColumnName("Phone2");
            this.Property(t => t.StartDate).HasColumnName("StartDate");
            this.Property(t => t.EndDate).HasColumnName("EndDate");
            //this.Property(t => t.Sale_BranchId).HasColumnName("Sale_BranchId");
            //this.Property(t => t.CheckInOut_UserId).HasColumnName("CheckInOut_UserId");
            this.Property(t => t.IsWorking).HasColumnName("IsWorking");
            this.Property(t => t.DrugStore).HasColumnName("DrugStore");
            this.Property(t => t.StaffParentId).HasColumnName("StaffParentId");
            this.Property(t => t.CommissionPercent).HasColumnName("CommissionPercent");
            this.Property(t => t.MinimumRevenue).HasColumnName("MinimumRevenue");
            this.Property(t => t.PositionId).HasColumnName("PositionId");
        }
    }
}
