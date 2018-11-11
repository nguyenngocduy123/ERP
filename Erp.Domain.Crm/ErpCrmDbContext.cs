using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using Erp.Domain.Crm.Entities;
using System.Reflection;

namespace Erp.Domain.Crm
{
    public class ErpCrmDbContext : DbContext, IDbContext
    {
        static ErpCrmDbContext()
        {
            Database.SetInitializer<ErpCrmDbContext>(null);
        }

        public ErpCrmDbContext()
            : base("Name=ErpDbContext")
        {
        }

        public new IDbSet<TEntity> Set<TEntity>() where TEntity : class
        {
            return base.Set<TEntity>();
        }

        public DbSet<Campaign> Campaign { get; set; }
        public DbSet<Process> Process { get; set; }
        public DbSet<ProcessAction> ProcessAction { get; set; }
        public DbSet<Task> Task { get; set; }
        public DbSet<vwTask> vwTask { get; set; }
        public DbSet<ProcessStage> ProcessStage { get; set; }
        public DbSet<ProcessStep> ProcessStep { get; set; }
        public DbSet<Question> Question { get; set; }
        public DbSet<Answer> Answer { get; set; }
        public DbSet<Vote> Vote { get; set; }
        public DbSet<vwVote> vwVote { get; set; }
        public DbSet<vwVote2> vwVote2 { get; set; }
        public DbSet<ProcessApplied> ProcessApplied { get; set; }
        public DbSet<SMSLog> SMSLog { get; set; }
        public DbSet<vwSMSLog> vwSMSLog { get; set; }
        public DbSet<EmailLog> EmailLog { get; set; }
        public DbSet<vwEmailLog> vwEmailLog { get; set; }
      //  public DbSet<LogAccumulatePoint> LogAccumulatePoint { get; set; }
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
