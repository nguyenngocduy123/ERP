using Erp.Domain.Sale.Entities;
using Erp.Domain.Sale.Interfaces;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity;
using System.Linq;

namespace Erp.Domain.Sale.Repositories
{
    public class LogServiceRemminderRepository : GenericRepository<ErpSaleDbContext, LogServiceRemminder>, ILogServiceRemminderRepository
    {
        public LogServiceRemminderRepository(ErpSaleDbContext context)
            : base(context)
        {

        }

        /// <summary>
        /// Get all LogServiceRemminder
        /// </summary>
        /// <returns>LogServiceRemminder list</returns>
        public IQueryable<LogServiceRemminder> GetAllLogServiceRemminder()
        {
            return Context.LogServiceRemminder.Where(item => (item.IsDeleted == null || item.IsDeleted == false));
        }
        public IQueryable<vwLogServiceRemminder> GetAllvwLogServiceRemminder()
        {
            return Context.vwLogServiceRemminder.Where(item => (item.IsDeleted == null || item.IsDeleted == false));
        }
        /// <summary>
        /// Get LogServiceRemminder information by specific id
        /// </summary>
        /// <param name="LogServiceRemminderId">Id of LogServiceRemminder</param>
        /// <returns></returns>
        public LogServiceRemminder GetLogServiceRemminderById(int Id)
        {
            return Context.LogServiceRemminder.SingleOrDefault(item => item.Id == Id && (item.IsDeleted == null || item.IsDeleted == false));
        }
        public vwLogServiceRemminder GetvwLogServiceRemminderById(int Id)
        {
            return Context.vwLogServiceRemminder.SingleOrDefault(item => item.Id == Id && (item.IsDeleted == null || item.IsDeleted == false));
        }

        /// <summary>
        /// Insert LogServiceRemminder into database
        /// </summary>
        /// <param name="LogServiceRemminder">Object infomation</param>
        public void InsertLogServiceRemminder(LogServiceRemminder LogServiceRemminder)
        {
            Context.LogServiceRemminder.Add(LogServiceRemminder);
            Context.Entry(LogServiceRemminder).State = EntityState.Added;
            Context.SaveChanges();
        }

        /// <summary>
        /// Delete LogServiceRemminder with specific id
        /// </summary>
        /// <param name="Id">LogServiceRemminder Id</param>
        public void DeleteLogServiceRemminder(int Id)
        {
            LogServiceRemminder deletedLogServiceRemminder = GetLogServiceRemminderById(Id);
            Context.LogServiceRemminder.Remove(deletedLogServiceRemminder);
            Context.Entry(deletedLogServiceRemminder).State = EntityState.Deleted;
            Context.SaveChanges();
        }
        
        /// <summary>
        /// Delete a LogServiceRemminder with its Id and Update IsDeleted IF that LogServiceRemminder has relationship with others
        /// </summary>
        /// <param name="LogServiceRemminderId">Id of LogServiceRemminder</param>
        public void DeleteLogServiceRemminderRs(int Id)
        {
            LogServiceRemminder deleteLogServiceRemminderRs = GetLogServiceRemminderById(Id);
            deleteLogServiceRemminderRs.IsDeleted = true;
            UpdateLogServiceRemminder(deleteLogServiceRemminderRs);
        }

        /// <summary>
        /// Update LogServiceRemminder into database
        /// </summary>
        /// <param name="LogServiceRemminder">LogServiceRemminder object</param>
        public void UpdateLogServiceRemminder(LogServiceRemminder LogServiceRemminder)
        {
            Context.Entry(LogServiceRemminder).State = EntityState.Modified;
            Context.SaveChanges();
        }
    }
}
