using Erp.Domain.Sale.Entities;
using System.Collections.Generic;
using System.Linq;

namespace Erp.Domain.Sale.Interfaces
{
    public interface ILogVipRepository
    {
        /// <summary>
        /// Get all LogVip
        /// </summary>
        /// <returns>LogVip list</returns>
        IQueryable<LogVip> GetAllLogVip();

        /// <summary>
        /// Get LogVip information by specific id
        /// </summary>
        /// <param name="Id">Id of LogVip</param>
        /// <returns></returns>
        LogVip GetLogVipById(int Id);

        /// <summary>
        /// Insert LogVip into database
        /// </summary>
        /// <param name="LogVip">Object infomation</param>
        void InsertLogVip(LogVip LogVip);

        /// <summary>
        /// Delete LogVip with specific id
        /// </summary>
        /// <param name="Id">LogVip Id</param>
        void DeleteLogVip(int Id);

        /// <summary>
        /// Delete a LogVip with its Id and Update IsDeleted IF that LogVip has relationship with others
        /// </summary>
        /// <param name="Id">Id of LogVip</param>
        void DeleteLogVipRs(int Id);

        /// <summary>
        /// Update LogVip into database
        /// </summary>
        /// <param name="LogVip">LogVip object</param>
        void UpdateLogVip(LogVip LogVip);
    }
}
