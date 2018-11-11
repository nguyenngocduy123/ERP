using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Erp.Domain.Staff.Entities.Mapping
{
    public class HistoryCommissionStaffMap : EntityTypeConfiguration<HistoryCommissionStaff>
    {
        public HistoryCommissionStaffMap()
        {
            // Primary Key
            this.HasKey(t => t.Id);

            // Properties
                        this.Property(t => t.PositionName).HasMaxLength(200);


            // Table & Column Mappings
            this.ToTable("Staff_HistoryCommissionStaff");
            this.Property(t => t.Id).HasColumnName("Id");
            this.Property(t => t.CreatedUserId).HasColumnName("CreatedUserId");
            this.Property(t => t.CreatedDate).HasColumnName("CreatedDate");
            this.Property(t => t.ModifiedUserId).HasColumnName("ModifiedUserId");
            this.Property(t => t.ModifiedDate).HasColumnName("ModifiedDate");
            this.Property(t => t.StaffParentId).HasColumnName("StaffParentId");
            this.Property(t => t.StaffId).HasColumnName("StaffId");
            this.Property(t => t.StaffName).HasColumnName("StaffName");
            this.Property(t => t.Month).HasColumnName("Month");
            this.Property(t => t.Year).HasColumnName("Year");
            this.Property(t => t.PositionName).HasColumnName("PositionName");
            this.Property(t => t.CommissionPercent).HasColumnName("CommissionPercent");
            this.Property(t => t.MinimumRevenue).HasColumnName("MinimumRevenue");
            this.Property(t => t.RevenueDS).HasColumnName("RevenueDS");
            this.Property(t => t.AmountCommission).HasColumnName("AmountCommission");

        }
    }
}
