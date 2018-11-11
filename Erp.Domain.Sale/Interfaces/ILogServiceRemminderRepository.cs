using Erp.Domain.Sale.Entities;
using System.Collections.Generic;
using System.Linq;

namespace Erp.Domain.Sale.Interfaces
{
    public interface ILogServiceRemminderRepository
    {
        /// <summary>
        /// Get all LogServiceRemminder
        /// </summary>
        /// <returns>LogServiceRemminder list</returns>
        IQueryable<LogServiceRemminder> GetAllLogServiceRemminder();
        IQueryable<vwLogServiceRemminder> GetAllvwLogServiceRemminder();
        /// <summary>
        /// Get LogServiceRemminder information by specific id
        /// </summary>
        /// <param name="Id">Id of LogServiceRemminder</param>
        /// <returns></returns>
        LogServiceRemminder GetLogServiceRemminderById(int Id);
        vwLogServiceRemminder GetvwLogServiceRemminderById(int Id);
        /// <summary>
        /// Insert LogServiceRemminder into database
        /// </summary>
        /// <param name="LogServiceRemminder">Object infomation</param>
        void InsertLogServiceRemminder(LogServiceRemminder LogServiceRemminder);

        /// <summary>
        /// Delete LogServiceRemminder with specific id
        /// </summary>
        /// <param name="Id">LogServiceRemminder Id</param>
        void DeleteLogServiceRemminder(int Id);

        /// <summary>
        /// Delete a LogServiceRemminder with its Id and Update IsDeleted IF that LogServiceRemminder has relationship with others
        /// </summary>
        /// <param name="Id">Id of LogServiceRemminder</param>
        void DeleteLogServiceRemminderRs(int Id);

        /// <summary>
        /// Update LogServiceRemminder into database
        /// </summary>
        /// <param name="LogServiceRemminder">LogServiceRemminder object</param>
        void UpdateLogServiceRemminder(LogServiceRemminder LogServiceRemminder);
    }
}
