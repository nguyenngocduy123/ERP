using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Erp.Domain.Entities;
using Erp.Domain.Entities.Mapping;
using System.Reflection;

namespace Erp.Domain
{
    public class ErpDbContext : DbContext, IDbContext
    {
        static ErpDbContext()
        {
            Database.SetInitializer<ErpDbContext>(null);
        }

        public ErpDbContext()
            : base("Name=ErpDbContext")
        {
            // this.Configuration.LazyLoadingEnabled = false;
            // this.Configuration.ProxyCreationEnabled = false;
        }

        public new IDbSet<TEntity> Set<TEntity>() where TEntity : class
        {
            return base.Set<TEntity>();
        }

        public DbSet<BOLog> BOLogs { get; set; }
        public DbSet<Category> Category { get; set; }
        public DbSet<Language> Languages { get; set; }
        public DbSet<News> News { get; set; }
        public DbSet<News_ViewedUser> News_ViewedUsers { get; set; }
        public DbSet<Page> Pages { get; set; }
        public DbSet<PageMenu> PageMenus { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<vwUsers> vwUsers { get; set; }
        public DbSet<UserPage> UserPages { get; set; }
        public DbSet<UserSetting> UserSetting { get; set; }
        public DbSet<UserType> UserType { get; set; }
        public DbSet<UserTypePage> UserTypePages { get; set; }
        public DbSet<vwPage> vwPages { get; set; }
        public DbSet<vwPageMenu> vwPageMenu { get; set; }
        public DbSet<Setting> Setting { get; set; }
        public DbSet<LoginLog> LoginLogs { get; set; }
        public DbSet<Location> Location { get; set; }
        public DbSet<Membership> Membership { get; set; }
        public DbSet<Erp.Domain.Entities.Module> Module { get; set; }
        public DbSet<MetadataField> MetadataField { get; set; }
        public DbSet<vwMetadataField> vwMetadataField { get; set; }
        public DbSet<ModuleRelationship> ModuleRelationship { get; set; }
      
     

        //<append_content_DbSet_here>

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            // mapping bằng scan Assembly
            modelBuilder.Configurations.AddFromAssembly(Assembly.GetAssembly(GetType())); //Current Assembly
            base.OnModelCreating(modelBuilder);

            // mapping bằng tay
            //modelBuilder.Configurations.Add(new BOLogMap());
            //modelBuilder.Configurations.Add(new CategoryMap());
            //modelBuilder.Configurations.Add(new LanguageMap());
            //modelBuilder.Configurations.Add(new NewsMap());
            //modelBuilder.Configurations.Add(new News_ViewedUserMap());
            //modelBuilder.Configurations.Add(new PageMap());
            //modelBuilder.Configurations.Add(new PageMenuMap());
            //modelBuilder.Configurations.Add(new UserMap());
            //modelBuilder.Configurations.Add(new vwUsersMap());
            //modelBuilder.Configurations.Add(new UserPageMap());
            //modelBuilder.Configurations.Add(new UserTypeMap());
            //modelBuilder.Configurations.Add(new UserTypePageMap());
            //modelBuilder.Configurations.Add(new UserSettingMap());
            //modelBuilder.Configurations.Add(new vwPageMap());
            //modelBuilder.Configurations.Add(new vw_PageMenuParentMap());
            //modelBuilder.Configurations.Add(new SettingMap());
            //modelBuilder.Configurations.Add(new LoginLogMap());
            //modelBuilder.Configurations.Add(new LocationMap());
            //modelBuilder.Configurations.Add(new MembershipMap());

        }
    }

    public interface IDbContext
    {
        IDbSet<TEntity> Set<TEntity>() where TEntity : class;
        int SaveChanges();
        void Dispose();
    }
}
