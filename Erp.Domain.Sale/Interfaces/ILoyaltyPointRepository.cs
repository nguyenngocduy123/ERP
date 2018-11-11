using Erp.Domain.Sale.Entities;
using System.Collections.Generic;
using System.Linq;

namespace Erp.Domain.Sale.Interfaces
{
    public interface ILoyaltyPointRepository
    {
        /// <summary>
        /// Get all LoyaltyPoint
        /// </summary>
        /// <returns>LoyaltyPoint list</returns>
        IQueryable<LoyaltyPoint> GetAllLoyaltyPoint();

        /// <summary>
        /// Get LoyaltyPoint information by specific id
        /// </summary>
        /// <param name="Id">Id of LoyaltyPoint</param>
        /// <returns></returns>
        LoyaltyPoint GetLoyaltyPointById(int Id);

        /// <summary>
        /// Insert LoyaltyPoint into database
        /// </summary>
        /// <param name="LoyaltyPoint">Object infomation</param>
        void InsertLoyaltyPoint(LoyaltyPoint LoyaltyPoint);

        /// <summary>
        /// Delete LoyaltyPoint with specific id
        /// </summary>
        /// <param name="Id">LoyaltyPoint Id</param>
        void DeleteLoyaltyPoint(int Id);

        /// <summary>
        /// Delete a LoyaltyPoint with its Id and Update IsDeleted IF that LoyaltyPoint has relationship with others
        /// </summary>
        /// <param name="Id">Id of LoyaltyPoint</param>
        void DeleteLoyaltyPointRs(int Id);

        /// <summary>
        /// Update LoyaltyPoint into database
        /// </summary>
        /// <param name="LoyaltyPoint">LoyaltyPoint object</param>
        void UpdateLoyaltyPoint(LoyaltyPoint LoyaltyPoint);
    }
}
