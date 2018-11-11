using Erp.Domain.RealEstate.Entities;
using System.Collections.Generic;
using System.Linq;

namespace Erp.Domain.RealEstate.Interfaces
{
    public interface ICondosLayoutRepository
    {
        /// <summary>
        /// Get all CondosLayout
        /// </summary>
        /// <returns>CondosLayout list</returns>
        IQueryable<CondosLayout> GetAllCondosLayout();

        /// <summary>
        /// Get CondosLayout information by specific id
        /// </summary>
        /// <param name="Id">Id of CondosLayout</param>
        /// <returns></returns>
        CondosLayout GetCondosLayoutById(int Id);

        /// <summary>
        /// Insert CondosLayout into database
        /// </summary>
        /// <param name="CondosLayout">Object infomation</param>
        void InsertCondosLayout(CondosLayout CondosLayout);

        /// <summary>
        /// Delete CondosLayout with specific id
        /// </summary>
        /// <param name="Id">CondosLayout Id</param>
        void DeleteCondosLayout(int Id);

        /// <summary>
        /// Delete a CondosLayout with its Id and Update IsDeleted IF that CondosLayout has relationship with others
        /// </summary>
        /// <param name="Id">Id of CondosLayout</param>
        void DeleteCondosLayoutRs(int Id);

        /// <summary>
        /// Update CondosLayout into database
        /// </summary>
        /// <param name="CondosLayout">CondosLayout object</param>
        void UpdateCondosLayout(CondosLayout CondosLayout);
    }
}
