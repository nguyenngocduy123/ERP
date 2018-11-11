using Erp.Domain.Staff.Entities;
using System.Collections.Generic;
using System.Linq;

namespace Erp.Domain.Staff.Interfaces
{
    public interface IBonusDisciplineRepository
    {
        /// <summary>
        /// Get all BonusDiscipline
        /// </summary>
        /// <returns>BonusDiscipline list</returns>
        IQueryable<BonusDiscipline> GetAllBonusDiscipline();
        IQueryable<vwBonusDiscipline> GetAllvwBonusDiscipline();
        /// <summary>
        /// Get BonusDiscipline information by specific id
        /// </summary>
        /// <param name="Id">Id of BonusDiscipline</param>
        /// <returns></returns>
        BonusDiscipline GetBonusDisciplineById(int? Id);
        vwBonusDiscipline GetvwBonusDisciplineById(int? Id);
        /// <summary>
        /// Insert BonusDiscipline into database
        /// </summary>
        /// <param name="BonusDiscipline">Object infomation</param>
        void InsertBonusDiscipline(BonusDiscipline BonusDiscipline);

        /// <summary>
        /// Delete BonusDiscipline with specific id
        /// </summary>
        /// <param name="Id">BonusDiscipline Id</param>
        void DeleteBonusDiscipline(int Id);

        /// <summary>
        /// Delete a BonusDiscipline with its Id and Update IsDeleted IF that BonusDiscipline has relationship with others
        /// </summary>
        /// <param name="Id">Id of BonusDiscipline</param>
        void DeleteBonusDisciplineRs(int Id);

        /// <summary>
        /// Update BonusDiscipline into database
        /// </summary>
        /// <param name="BonusDiscipline">BonusDiscipline object</param>
        void UpdateBonusDiscipline(BonusDiscipline BonusDiscipline);
    }
}
