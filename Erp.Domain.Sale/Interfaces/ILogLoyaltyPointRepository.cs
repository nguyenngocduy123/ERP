using Erp.Domain.Sale.Entities;
using System.Collections.Generic;
using System.Linq;

namespace Erp.Domain.Sale.Interfaces
{
    public interface ILogLoyaltyPointRepository
    {
        /// <summary>
        /// Get all LogLoyaltyPoint
        /// </summary>
        /// <returns>LogLoyaltyPoint list</returns>
        IQueryable<LogLoyaltyPoint> GetAllLogLoyaltyPoint();
        IQueryable<vwLogLoyaltyPoint> GetAllvwLogLoyaltyPoint();
        /// <summary>
        /// Get LogLoyaltyPoint information by specific id
        /// </summary>
        /// <param name="Id">Id of LogLoyaltyPoint</param>
        /// <returns></returns>
        LogLoyaltyPoint GetLogLoyaltyPointById(int Id);
        vwLogLoyaltyPoint GetvwLogLoyaltyPointById(int Id);
        /// <summary>
        /// Insert LogLoyaltyPoint into database
        /// </summary>
        /// <param name="LogLoyaltyPoint">Object infomation</param>
        void InsertLogLoyaltyPoint(LogLoyaltyPoint LogLoyaltyPoint);

        /// <summary>
        /// Delete LogLoyaltyPoint with specific id
        /// </summary>
        /// <param name="Id">LogLoyaltyPoint Id</param>
        void DeleteLogLoyaltyPoint(int Id);

        /// <summary>
        /// Delete a LogLoyaltyPoint with its Id and Update IsDeleted IF that LogLoyaltyPoint has relationship with others
        /// </summary>
        /// <param name="Id">Id of LogLoyaltyPoint</param>
        void DeleteLogLoyaltyPointRs(int Id);

        /// <summary>
        /// Update LogLoyaltyPoint into database
        /// </summary>
        /// <param name="LogLoyaltyPoint">LogLoyaltyPoint object</param>
        void UpdateLogLoyaltyPoint(LogLoyaltyPoint LogLoyaltyPoint);
    }
}
