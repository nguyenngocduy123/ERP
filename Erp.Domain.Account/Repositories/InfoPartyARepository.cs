using Erp.Domain.Account.Entities;
using Erp.Domain.Account.Interfaces;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace Erp.Domain.Account.Repositories
{
    public class InfoPartyARepository : GenericRepository<ErpAccountDbContext, InfoPartyA>, IInfoPartyARepository
    {
        public InfoPartyARepository(ErpAccountDbContext context)
            : base(context)
        {

        }

        /// <summary>
        /// Get all InfoPartyA
        /// </summary>
        /// <returns>InfoPartyA list</returns>
        public IQueryable<InfoPartyA> GetAllInfoPartyA()
        {
            return Context.InfoPartyA.Where(item => (item.IsDeleted == null || item.IsDeleted == false));
        }
        public IQueryable<vwInfoPartyA> GetAllvwInfoPartyA()
        {
            return Context.vwInfoPartyA.Where(item => (item.IsDeleted == null || item.IsDeleted == false));
        }
        /// <summary>
        /// Get InfoPartyA information by specific id
        /// </summary>
        /// <param name="InfoPartyAId">Id of InfoPartyA</param>
        /// <returns></returns>
        public InfoPartyA GetInfoPartyAById(int Id)
        {
            return Context.InfoPartyA.SingleOrDefault(item => item.Id == Id && (item.IsDeleted == null || item.IsDeleted == false));
        }
        public vwInfoPartyA GetvwInfoPartyAById(int Id)
        {
            return Context.vwInfoPartyA.SingleOrDefault(item => item.Id == Id && (item.IsDeleted == null || item.IsDeleted == false));
        }
        /// <summary>
        /// Insert InfoPartyA into database
        /// </summary>
        /// <param name="InfoPartyA">Object infomation</param>
        public void InsertInfoPartyA(InfoPartyA InfoPartyA)
        {
            Context.InfoPartyA.Add(InfoPartyA);
            Context.Entry(InfoPartyA).State = EntityState.Added;
            Context.SaveChanges();
        }

        /// <summary>
        /// Delete InfoPartyA with specific id
        /// </summary>
        /// <param name="Id">InfoPartyA Id</param>
        public void DeleteInfoPartyA(int Id)
        {
            InfoPartyA deletedInfoPartyA = GetInfoPartyAById(Id);
            Context.InfoPartyA.Remove(deletedInfoPartyA);
            Context.Entry(deletedInfoPartyA).State = EntityState.Deleted;
            Context.SaveChanges();
        }
        
        /// <summary>
        /// Delete a InfoPartyA with its Id and Update IsDeleted IF that InfoPartyA has relationship with others
        /// </summary>
        /// <param name="InfoPartyAId">Id of InfoPartyA</param>
        public void DeleteInfoPartyARs(int Id)
        {
            InfoPartyA deleteInfoPartyARs = GetInfoPartyAById(Id);
            deleteInfoPartyARs.IsDeleted = true;
            UpdateInfoPartyA(deleteInfoPartyARs);
        }

        /// <summary>
        /// Update InfoPartyA into database
        /// </summary>
        /// <param name="InfoPartyA">InfoPartyA object</param>
        public void UpdateInfoPartyA(InfoPartyA InfoPartyA)
        {
            Context.Entry(InfoPartyA).State = EntityState.Modified;
            Context.SaveChanges();
        }
    }
}
