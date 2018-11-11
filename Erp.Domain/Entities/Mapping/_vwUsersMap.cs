using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Erp.Domain.Entities.Mapping
{
    public class vwUsersMap : EntityTypeConfiguration<vwUsers>
    {
        public vwUsersMap()
        {
            // Primary Key
            this.HasKey(t => t.Id);

            // Properties
            this.Property(t => t.UserName).HasMaxLength(150);
            this.Property(t => t.FirstName).HasMaxLength(50);
            this.Property(t => t.LastName).HasMaxLength(50);
            this.Property(t => t.FullName).HasMaxLength(100);
            this.Property(t => t.Address).HasMaxLength(200);
            this.Property(t => t.Mobile).HasMaxLength(200);
            this.Property(t => t.DrugStore).HasMaxLength(500);

            // Table & Column Mappings
            this.ToTable("vwSystem_Users");
            this.Property(t => t.Id).HasColumnName("Id");
            this.Property(t => t.CreatedDate).HasColumnName("CreatedDate");
            this.Property(t => t.ModifiedDate).HasColumnName("ModifiedDate");

            this.Property(t => t.UserName).HasColumnName("UserName");
            this.Property(t => t.Status).HasColumnName("Status");
            this.Property(t => t.UserCode).HasColumnName("UserCode");
            this.Property(t => t.FirstName).HasColumnName("FirstName");
            this.Property(t => t.LastName).HasColumnName("LastName");
            this.Property(t => t.FullName).HasColumnName("FullName");
            this.Property(t => t.DateOfBirth).HasColumnName("DateOfBirth");
            this.Property(t => t.Address).HasColumnName("Address");
            this.Property(t => t.Mobile).HasColumnName("Mobile");
            this.Property(t => t.Email).HasColumnName("Email");
            this.Property(t => t.Sex).HasColumnName("Sex");
            this.Property(t => t.UserTypeId).HasColumnName("UserTypeId");
            this.Property(t => t.LoginFailedCount).HasColumnName("LoginFailedCount");
            this.Property(t => t.LoginFailedCount2).HasColumnName("LoginFailedCount2");
            this.Property(t => t.LoginFailedCount3).HasColumnName("LoginFailedCount3");
            this.Property(t => t.LastChangedPassword).HasColumnName("LastChangedPassword");
            this.Property(t => t.UserTypeName).HasColumnName("UserTypeName");
            //this.Property(t => t.BranchId).HasColumnName("BranchId");
            //this.Property(t => t.BranchName).HasColumnName("BranchName");
            //this.Property(t => t.BranchCode).HasColumnName("BranchCode");
            this.Property(t => t.WarehouseId).HasColumnName("WarehouseId");
            this.Property(t => t.ProfileImage).HasColumnName("ProfileImage");
            this.Property(t => t.ParentId).HasColumnName("ParentId");
            this.Property(t => t.ParentName).HasColumnName("ParentName");
            this.Property(t => t.DrugStore).HasColumnName("DrugStore");
            this.Property(t => t.UserTypeCode).HasColumnName("UserTypeCode");
            this.Property(t => t.DrugStoreCode).HasColumnName("DrugStoreCode");
            this.Property(t => t.DrugStoreName).HasColumnName("DrugStoreName");
            this.Property(t => t.CooperationDay).HasColumnName("CooperationDay");
        }
    }
}
