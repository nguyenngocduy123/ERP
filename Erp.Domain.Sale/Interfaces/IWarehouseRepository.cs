using Erp.Domain.Sale.Entities;
using System.Collections.Generic;
using System.Linq;

namespace Erp.Domain.Sale.Interfaces
{
    public interface IWarehouseRepository
    {
        /// <summary>
        /// Get all Warehouse
        /// </summary>
        /// <returns>Warehouse list</returns>
        IQueryable<Warehouse> GetAllWarehouse();
        IQueryable<vwWarehouse> GetvwAllWarehouse();
        /// <summary>
        /// Get Warehouse information by specific id
        /// </summary>
        /// <param name="Id">Id of Warehouse</param>
        /// <returns></returns>
        Warehouse GetWarehouseById(int Id);

        /// <summary>
        /// Insert Warehouse into database
        /// </summary>
        /// <param name="Warehouse">Object infomation</param>
        void InsertWarehouse(Warehouse Warehouse);

        /// <summary>
        /// Delete Warehouse with specific id
        /// </summary>
        /// <param name="Id">Warehouse Id</param>
        void DeleteWarehouse(int Id);

        /// <summary>
        /// Delete a Warehouse with its Id and Update IsDeleted IF that Warehouse has relationship with others
        /// </summary>
        /// <param name="Id">Id of Warehouse</param>
        void DeleteWarehouseRs(int Id);

        /// <summary>
        /// Update Warehouse into database
        /// </summary>
        /// <param name="Warehouse">Warehouse object</param>
        void UpdateWarehouse(Warehouse Warehouse);
    }
}
