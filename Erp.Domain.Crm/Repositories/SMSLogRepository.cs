using Erp.Domain.Crm.Entities;
using Erp.Domain.Crm.Interfaces;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace Erp.Domain.Crm.Repositories
{
    public class SMSLogRepository : GenericRepository<ErpCrmDbContext, SMSLog>, ISMSLogRepository
    {
        public SMSLogRepository(ErpCrmDbContext context)
            : base(context)
        {

        }

        /// <summary>
        /// Get all SMSLog
        /// </summary>
        /// <returns>SMSLog list</returns>
        public IQueryable<SMSLog> GetAllSMSLog()
        {
            return Context.SMSLog.Where(item => (item.IsDeleted == null || item.IsDeleted == false));
        }

        /// <summary>
        /// Get SMSLog information by specific id
        /// </summary>
        /// <param name="SMSLogId">Id of SMSLog</param>
        /// <returns></returns>
        public SMSLog GetSMSLogById(int Id)
        {
            return Context.SMSLog.SingleOrDefault(item => item.Id == Id && (item.IsDeleted == null || item.IsDeleted == false));
        }

        /// <summary>
        /// Insert SMSLog into database
        /// </summary>
        /// <param name="SMSLog">Object infomation</param>
        public void InsertSMSLog(SMSLog SMSLog)
        {
            Context.SMSLog.Add(SMSLog);
            Context.Entry(SMSLog).State = EntityState.Added;
            Context.SaveChanges();
        }

        /// <summary>
        /// Delete SMSLog with specific id
        /// </summary>
        /// <param name="Id">SMSLog Id</param>
        public void DeleteSMSLog(int Id)
        {
            SMSLog deletedSMSLog = GetSMSLogById(Id);
            Context.SMSLog.Remove(deletedSMSLog);
            Context.Entry(deletedSMSLog).State = EntityState.Deleted;
            Context.SaveChanges();
        }

        /// <summary>
        /// Delete a SMSLog with its Id and Update IsDeleted IF that Campaign has relationship with others
        /// </summary>
        /// <param name="SMSLogId">Id of SMSLog</param>
        public void DeleteSMSLogRs(int Id)
        {
            SMSLog deleteSMSLogRs = GetSMSLogById(Id);
            deleteSMSLogRs.IsDeleted = true;
            UpdateSMSLog(deleteSMSLogRs);
        }

        /// <summary>
        /// Update SMSLog into database
        /// </summary>
        /// <param name="SMSLog">SMSLog object</param>
        public void UpdateSMSLog(SMSLog SMSLog)
        {
            Context.Entry(SMSLog).State = EntityState.Modified;
            Context.SaveChanges();
        }

        /// <summary>
        /// Get all vwv
        /// </summary>
        /// <returns>vwSMSLog list</returns>
        public IQueryable<vwSMSLog> GetAllvwSMSLog()
        {
            return Context.vwSMSLog.Where(item => (item.IsDeleted == null || item.IsDeleted == false));
        }

        /// <summary>
        /// Get vwSMSLog information by specific id
        /// </summary>
        /// <param name="vwSMSLogId">Id of vwSMSLog</param>
        /// <returns></returns>
        public vwSMSLog GetvwSMSLogById(int Id)
        {
            return Context.vwSMSLog.SingleOrDefault(item => item.Id == Id && (item.IsDeleted == null || item.IsDeleted == false));
        }
    }
}
