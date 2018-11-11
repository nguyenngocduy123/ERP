using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Erp.Domain.Entities.Mapping
{
    public class UserTypePageMap : EntityTypeConfiguration<UserTypePage>
    {
        public UserTypePageMap()
        {
            // Primary Key
            this.HasKey(t => new { t.UserTypeId, t.PageId });

            // Properties
            this.Property(t => t.UserTypeId)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.PageId)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            // Table & Column Mappings
            this.ToTable("System_UserTypePage");
            this.Property(t => t.UserTypeId).HasColumnName("UserTypeId");
            this.Property(t => t.PageId).HasColumnName("PageId");
            this.Property(t => t.View).HasColumnName("View");
            this.Property(t => t.Edit).HasColumnName("Edit");
            this.Property(t => t.Add).HasColumnName("Add");
            this.Property(t => t.Delete).HasColumnName("Delete");
            this.Property(t => t.Import).HasColumnName("Import");
            this.Property(t => t.Export).HasColumnName("Export");
            this.Property(t => t.Print).HasColumnName("Print");

            // Relationships
            //this.HasRequired(t => t.Page)
            //    .WithMany(t => t.UserTypePages)
            //    .HasForeignKey(d => d.PageId);

        }
    }
}
