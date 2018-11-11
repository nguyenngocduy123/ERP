using Erp.Domain.Sale.Entities;
using Erp.Domain.Sale.Interfaces;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;

namespace Erp.Domain.Sale.Repositories
{
    public class CommisionRepository : GenericRepository<ErpSaleDbContext, Commision>, ICommisionRepository
    {
        public CommisionRepository(ErpSaleDbContext context)
            : base(context)
        {

        }

        public IQueryable<vwCommision> GetAllCommision()
        {
            return Context.vwCommision.Where(item => (item.IsDeleted == null || item.IsDeleted == false));
        }

        public Commision GetCommisionById(int Id)
        {
            return Context.Commision.SingleOrDefault(item => item.Id == Id && (item.IsDeleted == null || item.IsDeleted == false));
        }

        public vwCommision GetvwCommisionById(int Id)
        {
            return Context.vwCommision.SingleOrDefault(item => item.Id == Id && (item.IsDeleted == null || item.IsDeleted == false));
        }

        public void InsertCommision(Commision Commision)
        {
            Context.Commision.Add(Commision);
            Context.Entry(Commision).State = EntityState.Added;
            Context.SaveChanges();
        }

        public void DeleteCommision(int Id)
        {
            Commision deletedCommision = GetCommisionById(Id);
            Context.Commision.Remove(deletedCommision);
            Context.Entry(deletedCommision).State = EntityState.Deleted;
            Context.SaveChanges();
        }        

        public void DeleteCommisionRs(int Id)
        {
            Commision deleteCommisionRs = GetCommisionById(Id);
            deleteCommisionRs.IsDeleted = true;
            UpdateCommision(deleteCommisionRs);
        }

        public void UpdateCommision(Commision Commision)
        {
            Context.Entry(Commision).State = EntityState.Modified;
            Context.SaveChanges();
        }
    }
}
