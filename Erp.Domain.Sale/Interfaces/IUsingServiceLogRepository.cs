using Erp.Domain.Sale.Entities;
using System.Collections.Generic;
using System.Linq;

namespace Erp.Domain.Sale.Interfaces
{
    public interface IUsingServiceLogRepository
    {
        /// <summary>
        /// Get all UsingServiceLog
        /// </summary>
        /// <returns>UsingServiceLog list</returns>
        IQueryable<UsingServiceLog> GetAllUsingServiceLog();
        IQueryable<vwUsingServiceLog> GetAllvwUsingServiceLog();
        /// <summary>
        /// Get UsingServiceLog information by specific id
        /// </summary>
        /// <param name="Id">Id of UsingServiceLog</param>
        /// <returns></returns>
        UsingServiceLog GetUsingServiceLogById(int Id);
        vwUsingServiceLog GetvwUsingServiceLogById(int Id);
        /// <summary>
        /// Insert UsingServiceLog into database
        /// </summary>
        /// <param name="UsingServiceLog">Object infomation</param>
        void InsertUsingServiceLog(UsingServiceLog UsingServiceLog);

        /// <summary>
        /// Delete UsingServiceLog with specific id
        /// </summary>
        /// <param name="Id">UsingServiceLog Id</param>
        void DeleteUsingServiceLog(int Id);

        /// <summary>
        /// Delete a UsingServiceLog with its Id and Update IsDeleted IF that UsingServiceLog has relationship with others
        /// </summary>
        /// <param name="Id">Id of UsingServiceLog</param>
        void DeleteUsingServiceLogRs(int Id);

        /// <summary>
        /// Update UsingServiceLog into database
        /// </summary>
        /// <param name="UsingServiceLog">UsingServiceLog object</param>
        void UpdateUsingServiceLog(UsingServiceLog UsingServiceLog);
    }
}
