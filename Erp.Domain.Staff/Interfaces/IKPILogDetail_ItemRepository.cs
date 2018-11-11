using Erp.Domain.Staff.Entities;
using System.Collections.Generic;
using System.Linq;

namespace Erp.Domain.Staff.Interfaces
{
    public interface IKPILogDetail_ItemRepository
    {
        /// <summary>
        /// Get all KPILogDetail_Item
        /// </summary>
        /// <returns>KPILogDetail_Item list</returns>
        IQueryable<KPILogDetail_Item> GetAllKPILogDetail_Item();

        /// <summary>
        /// Get KPILogDetail_Item information by specific id
        /// </summary>
        /// <param name="Id">Id of KPILogDetail_Item</param>
        /// <returns></returns>
        KPILogDetail_Item GetKPILogDetail_ItemById(int Id);

        /// <summary>
        /// Insert KPILogDetail_Item into database
        /// </summary>
        /// <param name="KPILogDetail_Item">Object infomation</param>
        void InsertKPILogDetail_Item(KPILogDetail_Item KPILogDetail_Item);

        /// <summary>
        /// Delete KPILogDetail_Item with specific id
        /// </summary>
        /// <param name="Id">KPILogDetail_Item Id</param>
        void DeleteKPILogDetail_Item(int Id);

        /// <summary>
        /// Delete a KPILogDetail_Item with its Id and Update IsDeleted IF that KPILogDetail_Item has relationship with others
        /// </summary>
        /// <param name="Id">Id of KPILogDetail_Item</param>
        void DeleteKPILogDetail_ItemRs(int Id);

        /// <summary>
        /// Update KPILogDetail_Item into database
        /// </summary>
        /// <param name="KPILogDetail_Item">KPILogDetail_Item object</param>
        void UpdateKPILogDetail_Item(KPILogDetail_Item KPILogDetail_Item);
    }
}
