using Erp.Domain.Sale.Entities;
using Erp.Domain.Sale.Interfaces;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity;
using System.Linq;

namespace Erp.Domain.Sale.Repositories
{
    public class LogVipRepository : GenericRepository<ErpSaleDbContext, LogVip>, ILogVipRepository
    {
        public LogVipRepository(ErpSaleDbContext context)
            : base(context)
        {

        }

        /// <summary>
        /// Get all LogVip
        /// </summary>
        /// <returns>LogVip list</returns>
        public IQueryable<LogVip> GetAllLogVip()
        {
            return Context.LogVip.Where(item => (item.IsDeleted == null || item.IsDeleted == false));
        }

        /// <summary>
        /// Get LogVip information by specific id
        /// </summary>
        /// <param name="LogVipId">Id of LogVip</param>
        /// <returns></returns>
        public LogVip GetLogVipById(int Id)
        {
            return Context.LogVip.SingleOrDefault(item => item.Id == Id && (item.IsDeleted == null || item.IsDeleted == false));
        }

        /// <summary>
        /// Insert LogVip into database
        /// </summary>
        /// <param name="LogVip">Object infomation</param>
        public void InsertLogVip(LogVip LogVip)
        {
            Context.LogVip.Add(LogVip);
            Context.Entry(LogVip).State = EntityState.Added;
            Context.SaveChanges();
        }

        /// <summary>
        /// Delete LogVip with specific id
        /// </summary>
        /// <param name="Id">LogVip Id</param>
        public void DeleteLogVip(int Id)
        {
            LogVip deletedLogVip = GetLogVipById(Id);
            Context.LogVip.Remove(deletedLogVip);
            Context.Entry(deletedLogVip).State = EntityState.Deleted;
            Context.SaveChanges();
        }
        
        /// <summary>
        /// Delete a LogVip with its Id and Update IsDeleted IF that LogVip has relationship with others
        /// </summary>
        /// <param name="LogVipId">Id of LogVip</param>
        public void DeleteLogVipRs(int Id)
        {
            LogVip deleteLogVipRs = GetLogVipById(Id);
            deleteLogVipRs.IsDeleted = true;
            UpdateLogVip(deleteLogVipRs);
        }

        /// <summary>
        /// Update LogVip into database
        /// </summary>
        /// <param name="LogVip">LogVip object</param>
        public void UpdateLogVip(LogVip LogVip)
        {
            Context.Entry(LogVip).State = EntityState.Modified;
            Context.SaveChanges();
        }
    }
}
