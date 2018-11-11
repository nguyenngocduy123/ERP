using Erp.Domain.Sale.Entities;
using Erp.Domain.Sale.Interfaces;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity;
using System.Linq;

namespace Erp.Domain.Sale.Repositories
{
    public class LogLoyaltyPointRepository : GenericRepository<ErpSaleDbContext, LogLoyaltyPoint>, ILogLoyaltyPointRepository
    {
        public LogLoyaltyPointRepository(ErpSaleDbContext context)
            : base(context)
        {

        }

        /// <summary>
        /// Get all LogLoyaltyPoint
        /// </summary>
        /// <returns>LogLoyaltyPoint list</returns>
        public IQueryable<LogLoyaltyPoint> GetAllLogLoyaltyPoint()
        {
            return Context.LogLoyaltyPoint.Where(item => (item.IsDeleted == null || item.IsDeleted == false));
        }
        public IQueryable<vwLogLoyaltyPoint> GetAllvwLogLoyaltyPoint()
        {
            return Context.vwLogLoyaltyPoint.Where(item => (item.IsDeleted == null || item.IsDeleted == false));
        }
        /// <summary>
        /// Get LogLoyaltyPoint information by specific id
        /// </summary>
        /// <param name="LogLoyaltyPointId">Id of LogLoyaltyPoint</param>
        /// <returns></returns>
        public LogLoyaltyPoint GetLogLoyaltyPointById(int Id)
        {
            return Context.LogLoyaltyPoint.SingleOrDefault(item => item.Id == Id && (item.IsDeleted == null || item.IsDeleted == false));
        }
        public vwLogLoyaltyPoint GetvwLogLoyaltyPointById(int Id)
        {
            return Context.vwLogLoyaltyPoint.SingleOrDefault(item => item.Id == Id && (item.IsDeleted == null || item.IsDeleted == false));
        }
        /// <summary>
        /// Insert LogLoyaltyPoint into database
        /// </summary>
        /// <param name="LogLoyaltyPoint">Object infomation</param>
        public void InsertLogLoyaltyPoint(LogLoyaltyPoint LogLoyaltyPoint)
        {
            Context.LogLoyaltyPoint.Add(LogLoyaltyPoint);
            Context.Entry(LogLoyaltyPoint).State = EntityState.Added;
            Context.SaveChanges();
        }

        /// <summary>
        /// Delete LogLoyaltyPoint with specific id
        /// </summary>
        /// <param name="Id">LogLoyaltyPoint Id</param>
        public void DeleteLogLoyaltyPoint(int Id)
        {
            LogLoyaltyPoint deletedLogLoyaltyPoint = GetLogLoyaltyPointById(Id);
            Context.LogLoyaltyPoint.Remove(deletedLogLoyaltyPoint);
            Context.Entry(deletedLogLoyaltyPoint).State = EntityState.Deleted;
            Context.SaveChanges();
        }
        
        /// <summary>
        /// Delete a LogLoyaltyPoint with its Id and Update IsDeleted IF that LogLoyaltyPoint has relationship with others
        /// </summary>
        /// <param name="LogLoyaltyPointId">Id of LogLoyaltyPoint</param>
        public void DeleteLogLoyaltyPointRs(int Id)
        {
            LogLoyaltyPoint deleteLogLoyaltyPointRs = GetLogLoyaltyPointById(Id);
            deleteLogLoyaltyPointRs.IsDeleted = true;
            UpdateLogLoyaltyPoint(deleteLogLoyaltyPointRs);
        }

        /// <summary>
        /// Update LogLoyaltyPoint into database
        /// </summary>
        /// <param name="LogLoyaltyPoint">LogLoyaltyPoint object</param>
        public void UpdateLogLoyaltyPoint(LogLoyaltyPoint LogLoyaltyPoint)
        {
            Context.Entry(LogLoyaltyPoint).State = EntityState.Modified;
            Context.SaveChanges();
        }
    }
}
