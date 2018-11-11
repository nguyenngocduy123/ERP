using Erp.Domain.Crm.Entities;
using Erp.Domain.Crm.Interfaces;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;


namespace Erp.Domain.Crm.Repositories
{
    public class EmailLogReponsitory : GenericRepository<ErpCrmDbContext, EmailLog>, IEmailLogRepository
    {
        public EmailLogReponsitory(ErpCrmDbContext context)
            : base(context)
        {

        }

        /// <summary>
        /// Get all EmailLog
        /// </summary>
        /// <returns>EmailLog list</returns>
        public IQueryable<EmailLog> GetAllEmailLog()
        {
            return Context.EmailLog.Where(item => (item.IsDeleted == null || item.IsDeleted == false));
        }

        /// <summary>
        /// Get EmailLog information by specific id
        /// </summary>
        /// <param name="EmailLogId">Id of EmailLog</param>
        /// <returns></returns>
        public EmailLog GetEmailLogById(int Id)
        {
            return Context.EmailLog.SingleOrDefault(item => item.Id == Id && (item.IsDeleted == null || item.IsDeleted == false));
        }

        /// <summary>
        /// Insert EmailLog into database
        /// </summary>
        /// <param name="EmailLog">Object infomation</param>
        public void InsertEmailLog(EmailLog EmailLog)
        {
            Context.EmailLog.Add(EmailLog);
            Context.Entry(EmailLog).State = EntityState.Added;
            Context.SaveChanges();
        }

        /// <summary>
        /// Delete EmailLog with specific id
        /// </summary>
        /// <param name="Id">EmailLog Id</param>
        public void DeleteEmailLog(int Id)
        {
            EmailLog deletedEmailLog = GetEmailLogById(Id);
            Context.EmailLog.Remove(deletedEmailLog);
            Context.Entry(deletedEmailLog).State = EntityState.Deleted;
            Context.SaveChanges();
        }

        /// <summary>
        /// Delete a EmailLog with its Id and Update IsDeleted IF that Campaign has relationship with others
        /// </summary>
        /// <param name="EmailLogId">Id of EmailLog</param>
        public void DeleteEmailLogRs(int Id)
        {
            EmailLog deleteEmailLogRs = GetEmailLogById(Id);
            deleteEmailLogRs.IsDeleted = true;
            UpdateEmailLog(deleteEmailLogRs);
        }

        /// <summary>
        /// Update EmailLog into database
        /// </summary>
        /// <param name="EmailLog">EmailLog object</param>
        public void UpdateEmailLog(EmailLog EmailLog)
        {
            Context.Entry(EmailLog).State = EntityState.Modified;
            Context.SaveChanges();
        }

        /// <summary>
        /// Get all vwEmailLog
        /// </summary>
        /// <returns>vwEmailLog list</returns>
        public IQueryable<vwEmailLog> GetAllvwEmailLog()
        {
            return Context.vwEmailLog.Where(item => (item.IsDeleted == null || item.IsDeleted == false));
        }

        /// <summary>
        /// Get vwEmailLog information by specific id
        /// </summary>
        /// <param name="vwEmailLogId">Id of vwEmailLog</param>
        /// <returns></returns>
        public vwEmailLog GetvwEmailLogById(int Id)
        {
            return Context.vwEmailLog.SingleOrDefault(item => item.Id == Id && (item.IsDeleted == null || item.IsDeleted == false));
        }
    }
}
