using Erp.Domain.Crm.Entities;
using System.Collections.Generic;
using System.Linq;

namespace Erp.Domain.Crm.Interfaces
{
    public interface ISMSLogRepository
    {
        /// <summary>
        /// Get all SMSLog
        /// </summary>
        /// <returns>SMSLog list</returns>
        IQueryable<SMSLog> GetAllSMSLog();

        /// <summary>
        /// Get SMSLog information by specific id
        /// </summary>
        /// <param name="Id">Id of SMSLog</param>
        /// <returns></returns>
        SMSLog GetSMSLogById(int Id);

        /// <summary>
        /// Insert SMSLog into database
        /// </summary>
        /// <param name="SMSLog">Object infomation</param>
        void InsertSMSLog(SMSLog SMSLog);

        /// <summary>
        /// Delete SMSLog with specific id
        /// </summary>
        /// <param name="Id">SMSLog Id</param>
        void DeleteSMSLog(int Id);

        /// <summary>
        /// Delete a SMSLog with its Id and Update IsDeleted IF that SMSLog has relationship with others
        /// </summary>
        /// <param name="Id">Id of SMSLog</param>
        void DeleteSMSLogRs(int Id);

        /// <summary>
        /// Update SMSLog into database
        /// </summary>
        /// <param name="SMSLog">SMSLog object</param>
        void UpdateSMSLog(SMSLog SMSLog);

        /// <summary>
        /// Get all vwSMSLog
        /// </summary>
        /// <returns>vwSMSLog list</returns>
        IQueryable<vwSMSLog> GetAllvwSMSLog();

        /// <summary>
        /// Get vwSMSLog information by specific id
        /// </summary>
        /// <param name="Id">Id of vwSMSLog</param>
        /// <returns></returns>
        vwSMSLog GetvwSMSLogById(int Id);
    }
}
