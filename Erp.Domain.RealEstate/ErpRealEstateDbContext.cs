using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using Erp.Domain.RealEstate.Entities;
using System.Reflection;

namespace Erp.Domain.RealEstate
{
    public class ErpRealEstateDbContext : DbContext, IDbContext
    {
        static ErpRealEstateDbContext()
        {
            Database.SetInitializer<ErpRealEstateDbContext>(null);
        }

        public ErpRealEstateDbContext()
            : base("Name=ErpDbContext")
        {
        }

        public new IDbSet<TEntity> Set<TEntity>() where TEntity : class
        {
            return base.Set<TEntity>();
        }

        
        public DbSet<Project> Project { get; set; }
        public DbSet<Block> Block { get; set; }
        public DbSet<Floor> Floor { get; set; }
        public DbSet<Condos> Condos { get; set; }
        public DbSet<CondosLayout> CondosLayout { get; set; }
        public DbSet<vwCondos> vwCondos { get; set; }
        public DbSet<CondosPrice> CondosPrice { get; set; }
        //<append_content_DbSet_here>

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            // mapping bằng scan Assembly
            modelBuilder.Configurations.AddFromAssembly(Assembly.GetAssembly(GetType())); //Current Assembly
            base.OnModelCreating(modelBuilder);
        }
    }

    public interface IDbContext
    {
        IDbSet<TEntity> Set<TEntity>() where TEntity : class;
        int SaveChanges();
        void Dispose();
    }
}
