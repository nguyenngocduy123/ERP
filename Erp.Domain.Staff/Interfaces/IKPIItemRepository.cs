using Erp.Domain.Staff.Entities;
using System.Collections.Generic;
using System.Linq;

namespace Erp.Domain.Staff.Interfaces
{
    public interface IKPIItemRepository
    {
        /// <summary>
        /// Get all KPIItem
        /// </summary>
        /// <returns>KPIItem list</returns>
        IQueryable<KPIItem> GetAllKPIItem();

        /// <summary>
        /// Get KPIItem information by specific id
        /// </summary>
        /// <param name="Id">Id of KPIItem</param>
        /// <returns></returns>
        KPIItem GetKPIItemById(int Id);

        /// <summary>
        /// Insert KPIItem into database
        /// </summary>
        /// <param name="KPIItem">Object infomation</param>
        void InsertKPIItem(KPIItem KPIItem);

        /// <summary>
        /// Delete KPIItem with specific id
        /// </summary>
        /// <param name="Id">KPIItem Id</param>
        void DeleteKPIItem(int Id);

        /// <summary>
        /// Delete a KPIItem with its Id and Update IsDeleted IF that KPIItem has relationship with others
        /// </summary>
        /// <param name="Id">Id of KPIItem</param>
        void DeleteKPIItemRs(int Id);

        /// <summary>
        /// Update KPIItem into database
        /// </summary>
        /// <param name="KPIItem">KPIItem object</param>
        void UpdateKPIItem(KPIItem KPIItem);
    }
}
