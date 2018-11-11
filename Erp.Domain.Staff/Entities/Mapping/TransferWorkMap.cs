using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Erp.Domain.Staff.Entities.Mapping
{
    public class TransferWorkMap : EntityTypeConfiguration<TransferWork>
    {
        public TransferWorkMap()
        {
            // Primary Key
            this.HasKey(t => t.Id);

            // Properties
            this.Property(t => t.PositionOld).HasMaxLength(150);
            this.Property(t => t.PositionNew).HasMaxLength(150);
            this.Property(t => t.Status).HasMaxLength(150);
            this.Property(t => t.Reason).HasMaxLength(250);
            this.Property(t => t.Code).HasMaxLength(150);


            // Table & Column Mappings
            this.ToTable("Staff_TransferWork");
            this.Property(t => t.Id).HasColumnName("Id");
            this.Property(t => t.CreatedUserId).HasColumnName("CreatedUserId");
            this.Property(t => t.CreatedDate).HasColumnName("CreatedDate");
            this.Property(t => t.ModifiedUserId).HasColumnName("ModifiedUserId");
            this.Property(t => t.ModifiedDate).HasColumnName("ModifiedDate");

            this.Property(t => t.StaffId).HasColumnName("StaffId");
            this.Property(t => t.BranchDepartmentOldId).HasColumnName("BranchDepartmentOldId");
            this.Property(t => t.BranchDepartmentNewId).HasColumnName("BranchDepartmentNewId");
            this.Property(t => t.PositionOld).HasColumnName("PositionOld");
            this.Property(t => t.PositionNew).HasColumnName("PositionNew");
            this.Property(t => t.DayDecision).HasColumnName("DayDecision");
            this.Property(t => t.UserId).HasColumnName("UserId");
            this.Property(t => t.Status).HasColumnName("Status");
            this.Property(t => t.Reason).HasColumnName("Reason");
            this.Property(t => t.DayEffective).HasColumnName("DayEffective");
            this.Property(t => t.Code).HasColumnName("Code");
            this.Property(t => t.CodeStaffOld).HasColumnName("CodeStaffOld");
            this.Property(t => t.CodeStaffNew).HasColumnName("CodeStaffNew");
        }
    }
}
