using Erp.Domain.Crm.Entities;
using System.Collections.Generic;
using System.Linq;


namespace Erp.Domain.Crm.Interfaces
{
    public interface IEmailLogRepository
    {
        /// <summary>
        /// Get all EmailLog
        /// </summary>
        /// <returns>EmailLog list</returns>
        IQueryable<EmailLog> GetAllEmailLog();

        /// <summary>
        /// Get EmailLog information by specific id
        /// </summary>
        /// <param name="Id">Id of EmailLog</param>
        /// <returns></returns>
        EmailLog GetEmailLogById(int Id);

        /// <summary>
        /// Insert EmailLog into database
        /// </summary>
        /// <param name="EmailLog">Object infomation</param>
        void InsertEmailLog(EmailLog EmailLog);

        /// <summary>
        /// Delete EmailLog with specific id
        /// </summary>
        /// <param name="Id">EmailLog Id</param>
        void DeleteEmailLog(int Id);

        /// <summary>
        /// Delete a EmailLog with its Id and Update IsDeleted IF that EmailLog has relationship with others
        /// </summary>
        /// <param name="Id">Id of EmailLog</param>
        void DeleteEmailLogRs(int Id);

        /// <summary>
        /// Update EmailLog into database
        /// </summary>
        /// <param name="EmailLog">EmailLog object</param>
        void UpdateEmailLog(EmailLog EmailLog);

        /// <summary>
        /// Get all vwEmailLog
        /// </summary>
        /// <returns>vwSMSLog list</returns>
        IQueryable<vwEmailLog> GetAllvwEmailLog();

        /// <summary>
        /// Get vwEmailLog information by specific id
        /// </summary>
        /// <param name="Id">Id of vwEmailLog</param>
        /// <returns></returns>
        vwEmailLog GetvwEmailLogById(int Id);
    }
}
