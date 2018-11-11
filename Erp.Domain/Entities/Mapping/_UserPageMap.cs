using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Erp.Domain.Entities.Mapping
{
    public class UserPageMap : EntityTypeConfiguration<UserPage>
    {
        public UserPageMap()
        {
            // Primary Key
            this.HasKey(t => new { t.UserId, t.PageId });

            // Properties
            this.Property(t => t.UserId)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.PageId)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            // Table & Column Mappings
            this.ToTable("System_UserPage");
            this.Property(t => t.UserId).HasColumnName("UserId");
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
            //    .WithMany(t => t.UserPages)
            //    .HasForeignKey(d => d.PageId);

        }
    }
}
