using Erp.Domain.Sale.Entities;
using Erp.Domain.Sale.Interfaces;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace Erp.Domain.Sale.Repositories
{
    public class UsingServiceLogRepository : GenericRepository<ErpSaleDbContext, UsingServiceLog>, IUsingServiceLogRepository
    {
        public UsingServiceLogRepository(ErpSaleDbContext context)
            : base(context)
        {

        }

        /// <summary>
        /// Get all UsingServiceLog
        /// </summary>
        /// <returns>UsingServiceLog list</returns>
        public IQueryable<UsingServiceLog> GetAllUsingServiceLog()
        {
            return Context.UsingServiceLog.Where(item => (item.IsDeleted == null || item.IsDeleted == false));
        }
        public IQueryable<vwUsingServiceLog> GetAllvwUsingServiceLog()
        {
            return Context.vwUsingServiceLog.Where(item => (item.IsDeleted == null || item.IsDeleted == false));
        }
        /// <summary>
        /// Get UsingServiceLog information by specific id
        /// </summary>
        /// <param name="UsingServiceLogId">Id of UsingServiceLog</param>
        /// <returns></returns>
        public UsingServiceLog GetUsingServiceLogById(int Id)
        {
            return Context.UsingServiceLog.SingleOrDefault(item => item.Id == Id && (item.IsDeleted == null || item.IsDeleted == false));
        }
        public vwUsingServiceLog GetvwUsingServiceLogById(int Id)
        {
            return Context.vwUsingServiceLog.SingleOrDefault(item => item.Id == Id && (item.IsDeleted == null || item.IsDeleted == false));
        }
        /// <summary>
        /// Insert UsingServiceLog into database
        /// </summary>
        /// <param name="UsingServiceLog">Object infomation</param>
        public void InsertUsingServiceLog(UsingServiceLog UsingServiceLog)
        {
            Context.UsingServiceLog.Add(UsingServiceLog);
            Context.Entry(UsingServiceLog).State = EntityState.Added;
            Context.SaveChanges();
        }

        /// <summary>
        /// Delete UsingServiceLog with specific id
        /// </summary>
        /// <param name="Id">UsingServiceLog Id</param>
        public void DeleteUsingServiceLog(int Id)
        {
            UsingServiceLog deletedUsingServiceLog = GetUsingServiceLogById(Id);
            Context.UsingServiceLog.Remove(deletedUsingServiceLog);
            Context.Entry(deletedUsingServiceLog).State = EntityState.Deleted;
            Context.SaveChanges();
        }
        
        /// <summary>
        /// Delete a UsingServiceLog with its Id and Update IsDeleted IF that UsingServiceLog has relationship with others
        /// </summary>
        /// <param name="UsingServiceLogId">Id of UsingServiceLog</param>
        public void DeleteUsingServiceLogRs(int Id)
        {
            UsingServiceLog deleteUsingServiceLogRs = GetUsingServiceLogById(Id);
            deleteUsingServiceLogRs.IsDeleted = true;
            UpdateUsingServiceLog(deleteUsingServiceLogRs);
        }

        /// <summary>
        /// Update UsingServiceLog into database
        /// </summary>
        /// <param name="UsingServiceLog">UsingServiceLog object</param>
        public void UpdateUsingServiceLog(UsingServiceLog UsingServiceLog)
        {
            Context.Entry(UsingServiceLog).State = EntityState.Modified;
            Context.SaveChanges();
        }
    }
}
