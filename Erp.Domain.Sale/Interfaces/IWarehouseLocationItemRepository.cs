using Erp.Domain.Sale.Entities;
using System.Collections.Generic;
using System.Linq;

namespace Erp.Domain.Sale.Interfaces
{
    public interface IWarehouseLocationItemRepository
    {
        /// <summary>
        /// Get all WarehouseLocationItem
        /// </summary>
        /// <returns>WarehouseLocationItem list</returns>
        IQueryable<WarehouseLocationItem> GetAllWarehouseLocationItem();
        IQueryable<WarehouseLocationItem> GetAllLocationItem();
        IQueryable<vwWarehouseLocationItem> GetAllvwWarehouseLocationItem();

        /// <summary>
        /// Get WarehouseLocationItem information by specific id
        /// </summary>
        /// <param name="Id">Id of WarehouseLocationItem</param>
        /// <returns></returns>
        WarehouseLocationItem GetWarehouseLocationItemById(int Id);
        WarehouseLocationItem GetWarehouseLocationItemBySerialNumber(int? warehouseId, int? productId, string serialNumber);

        /// <summary>
        /// Insert WarehouseLocationItem into database
        /// </summary>
        /// <param name="WarehouseLocationItem">Object infomation</param>
        void InsertWarehouseLocationItem(WarehouseLocationItem WarehouseLocationItem);
        void InsertWarehouseLocationItem(List<WarehouseLocationItem> listItem);
        /// <summary>
        /// Delete WarehouseLocationItem with specific id
        /// </summary>
        /// <param name="Id">WarehouseLocationItem Id</param>
        void DeleteWarehouseLocationItem(int Id);

        /// <summary>
        /// Delete a WarehouseLocationItem with its Id and Update IsDeleted IF that WarehouseLocationItem has relationship with others
        /// </summary>
        /// <param name="Id">Id of WarehouseLocationItem</param>
        void DeleteWarehouseLocationItemRs(int Id);

        /// <summary>
        /// Update WarehouseLocationItem into database
        /// </summary>
        /// <param name="WarehouseLocationItem">WarehouseLocationItem object</param>
        void UpdateWarehouseLocationItem(WarehouseLocationItem WarehouseLocationItem);
    }
}
