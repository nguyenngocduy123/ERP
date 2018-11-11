using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Erp.Domain.Staff.Entities.Mapping
{
    public class vwNotificationsDetailMap : EntityTypeConfiguration<vwNotificationsDetail>
    {
        public vwNotificationsDetailMap()
        {
            // Primary Key
            this.HasKey(t => t.Id);



            // Table & Column Mappings
            this.ToTable("vwStaff_NotificationsDetail");
            this.Property(t => t.Id).HasColumnName("Id");
            this.Property(t => t.CreatedUserId).HasColumnName("CreatedUserId");
            this.Property(t => t.CreatedDate).HasColumnName("CreatedDate");
            this.Property(t => t.ModifiedUserId).HasColumnName("ModifiedUserId");
            this.Property(t => t.ModifiedDate).HasColumnName("ModifiedDate");
            this.Property(t => t.AssignedUserId).HasColumnName("AssignedUserId");

            this.Property(t => t.StaffId).HasColumnName("StaffId");
            this.Property(t => t.Comment).HasColumnName("Comment");
            this.Property(t => t.NotificationsId).HasColumnName("NotificationsId");

            this.Property(t => t.Name).HasColumnName("Name");
            this.Property(t => t.Code).HasColumnName("Code");
            this.Property(t => t.ProfileImage).HasColumnName("ProfileImage");
            this.Property(t => t.Gender).HasColumnName("Gender");
        }
    }
}
