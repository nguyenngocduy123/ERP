using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Erp.Domain.Staff.Entities.Mapping
{
    public class vwCalendarVisitDrugStoreMap : EntityTypeConfiguration<vwCalendarVisitDrugStore>
    {
        public vwCalendarVisitDrugStoreMap()
        {
            // Primary Key
            this.HasKey(t => t.Id);

            // Properties
            this.Property(t => t.Note).HasMaxLength(350);
                        this.Property(t => t.Status).HasMaxLength(50);


            // Table & Column Mappings
            this.ToTable("vwStaff_CalendarVisitDrugStore");
            this.Property(t => t.Id).HasColumnName("Id");
            this.Property(t => t.CreatedUserId).HasColumnName("CreatedUserId");
            this.Property(t => t.CreatedDate).HasColumnName("CreatedDate");
            this.Property(t => t.ModifiedUserId).HasColumnName("ModifiedUserId");
            this.Property(t => t.ModifiedDate).HasColumnName("ModifiedDate");
            this.Property(t => t.AssignedUserId).HasColumnName("AssignedUserId");
            this.Property(t => t.Note).HasColumnName("Note");

            this.Property(t => t.DrugStoreId).HasColumnName("DrugStoreId");
            this.Property(t => t.StartDate).HasColumnName("StartDate");
            this.Property(t => t.EndDate).HasColumnName("EndDate");
            this.Property(t => t.Status).HasColumnName("Status");
            this.Property(t => t.StaffId).HasColumnName("StaffId");
            this.Property(t => t.Month).HasColumnName("Month");
            this.Property(t => t.Year).HasColumnName("Year");
            this.Property(t => t.BranchName).HasColumnName("BranchName");
            this.Property(t => t.BranchCode).HasColumnName("BranchCode");
            this.Property(t => t.DrugStoreName).HasColumnName("DrugStoreName");
            this.Property(t => t.DrugStoreCode).HasColumnName("DrugStoreCode ");
            this.Property(t => t.StaffCode).HasColumnName("StaffCode");
            this.Property(t => t.StaffName).HasColumnName("StaffName");
            this.Property(t => t.CityId).HasColumnName("CityId");
            this.Property(t => t.DistrictId).HasColumnName("DistrictId");
            this.Property(t => t.ProvinceName).HasColumnName("ProvinceName");
            this.Property(t => t.DistrictName).HasColumnName("DistrictName");
        }
    }
}
