using Erp.Domain.Sale.Entities;
using System.Collections.Generic;
using System.Linq;

namespace Erp.Domain.Sale.Interfaces
{
    public interface IInventoryRepository
    {
        IQueryable<Inventory> GetAllInventory();
        IQueryable<Inventory> GetAllInventoryByWarehouseId(int Id);
        IQueryable<Inventory> GetAllInventoryByProductId(int Id);

        IQueryable<vwInventory> GetAllvwInventory();
        IQueryable<vwInventory> GetAllvwInventoryByWarehouseId(int Id);

        IQueryable<vwInventory> GetAllvwInventoryByBranchId(int BranchId);
        IQueryable<vwInventory> GetAllvwInventoryByProductId(int Id);

        Inventory GetInventoryById(int Id);
        Inventory GetInventoryByWarehouseIdAndProductId(int warehouseId, int productId);

        void InsertInventory(Inventory Inventory);

        void DeleteInventory(int Id);

        void DeleteInventoryRs(int Id);

        void UpdateInventory(Inventory Inventory);


        // Inventory by month ---------------------------------------------------------------------------------------------------

        InventoryByMonth GetInventoryByMonthById(int id);
        IQueryable<InventoryByMonth> GetInventoryByMonth(int month, int year);
        IQueryable<InventoryByMonth> GetInventoryByMonthByWarehouseIdAndProductId(int warehouseId, int productId);

        void InsertInventoryByMonth(InventoryByMonth Inventory);

        void UpdateInventoryByMonth(InventoryByMonth Inventory);

        void DeleteInventoryByMonth(int Id);

        void DeleteInventoryByMonthRs(int Id);
    }
}
