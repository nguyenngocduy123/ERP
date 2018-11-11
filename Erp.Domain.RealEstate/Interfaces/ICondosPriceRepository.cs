using Erp.Domain.RealEstate.Entities;
using System.Collections.Generic;
using System.Linq;

namespace Erp.Domain.RealEstate.Interfaces
{
    public interface ICondosPriceRepository
    {
        /// <summary>
        /// Get all CondosPrice
        /// </summary>
        /// <returns>CondosPrice list</returns>
        IQueryable<CondosPrice> GetAllCondosPrice();

        /// <summary>
        /// Get CondosPrice information by specific id
        /// </summary>
        /// <param name="Id">Id of CondosPrice</param>
        /// <returns></returns>
        CondosPrice GetCondosPriceById(int Id);

        /// <summary>
        /// Insert CondosPrice into database
        /// </summary>
        /// <param name="CondosPrice">Object infomation</param>
        void InsertCondosPrice(CondosPrice CondosPrice);

        /// <summary>
        /// Delete CondosPrice with specific id
        /// </summary>
        /// <param name="Id">CondosPrice Id</param>
        void DeleteCondosPrice(int Id);

        /// <summary>
        /// Delete a CondosPrice with its Id and Update IsDeleted IF that CondosPrice has relationship with others
        /// </summary>
        /// <param name="Id">Id of CondosPrice</param>
        void DeleteCondosPriceRs(int Id);

        /// <summary>
        /// Update CondosPrice into database
        /// </summary>
        /// <param name="CondosPrice">CondosPrice object</param>
        void UpdateCondosPrice(CondosPrice CondosPrice);
    }
}
