using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Erp.Domain.Staff.Entities.Mapping
{
    public class vwInternalNotificationsMap : EntityTypeConfiguration<vwInternalNotifications>
    {
        public vwInternalNotificationsMap()
        {
            // Primary Key
            this.HasKey(t => t.Id);

            // Properties
            this.Property(t => t.Titles ).HasMaxLength(200);
            this.Property(t => t.Type ).HasMaxLength(50);



            // Table & Column Mappings
            this.ToTable("vwStaff_InternalNotifications");
            this.Property(t => t.Id).HasColumnName("Id");
            this.Property(t => t.CreatedUserId).HasColumnName("CreatedUserId");
            this.Property(t => t.CreatedDate).HasColumnName("CreatedDate");
            this.Property(t => t.ModifiedUserId).HasColumnName("ModifiedUserId");
            this.Property(t => t.ModifiedDate).HasColumnName("ModifiedDate");
            this.Property(t => t.AssignedUserId).HasColumnName("AssignedUserId");

            this.Property(t => t.PlaceOfNotice).HasColumnName("PlaceOfNotice");
            this.Property(t => t.PlaceOfReceipt).HasColumnName("PlaceOfReceipt");
            this.Property(t => t.Titles).HasColumnName("Titles");
            this.Property(t => t.Type).HasColumnName("Type");
            this.Property(t => t.Content).HasColumnName("Content");
            this.Property(t => t.Type).HasColumnName("Type");
            this.Property(t => t.Disable).HasColumnName("Disable");
            this.Property(t => t.Seen).HasColumnName("Seen");
            this.Property(t => t.ActionName).HasColumnName("ActionName");
            this.Property(t => t.ModuleName).HasColumnName("ModuleName");
            this.Property(t => t.UserName).HasColumnName("UserName");
            this.Property(t => t.FullName).HasColumnName("FullName");
            this.Property(t => t.ModifiedUserName).HasColumnName("ModifiedUserName");
            this.Property(t => t.ProfileImage).HasColumnName("ProfileImage");
        }
    }
}
