using Erp.Domain.Sale.Entities;
using Erp.Domain.Sale.Interfaces;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace Erp.Domain.Sale.Repositories
{
    public class InventoryRepository : GenericRepository<ErpSaleDbContext, Inventory>, IInventoryRepository
    {
        public InventoryRepository(ErpSaleDbContext context)
            : base(context)
        {

        }

        public IQueryable<Inventory> GetAllInventory()
        {
            return Context.Inventory.Where(item => (item.IsDeleted == null || item.IsDeleted == false));
        }
        public IQueryable<vwInventory> GetAllvwInventory()
        {
            return Context.vwInventory;
        }
        public IQueryable<Inventory> GetAllInventoryByWarehouseId(int Id)
        {
            return Context.Inventory.Where(item => item.WarehouseId == Id && (item.IsDeleted == null || item.IsDeleted == false));
        }

        public IQueryable<Inventory> GetAllInventoryByProductId(int Id)
        {
            return Context.Inventory.Where(item => item.ProductId == Id && (item.IsDeleted == null || item.IsDeleted == false));
        }
        public IQueryable<vwInventory> GetAllvwInventoryByWarehouseId(int Id)
        {
            return Context.vwInventory.Where(item => item.WarehouseId == Id);
        }

        public IQueryable<vwInventory> GetAllvwInventoryByBranchId(int BranchId)
        {
            return Context.vwInventory.Where(item => item.BranchId == BranchId);
        }

        public IQueryable<vwInventory> GetAllvwInventoryByProductId(int Id)
        {
            return Context.vwInventory.Where(item => item.ProductId == Id);
        }

        public Inventory GetInventoryById(int Id)
        {
            return Context.Inventory.SingleOrDefault(item => item.Id == Id && (item.IsDeleted == null || item.IsDeleted == false));
        }

        public Inventory GetInventoryByWarehouseIdAndProductId(int warehouseId, int productId)
        {
            return Context.Inventory.SingleOrDefault(item => item.WarehouseId == warehouseId && item.ProductId == productId && (item.IsDeleted == null || item.IsDeleted == false));
        }

        public void InsertInventory(Inventory Inventory)
        {
            Context.Inventory.Add(Inventory);
            Context.Entry(Inventory).State = EntityState.Added;
            Context.SaveChanges();
        }

        public void DeleteInventory(int Id)
        {
            Inventory deletedInventory = GetInventoryById(Id);
            Context.Inventory.Remove(deletedInventory);
            Context.Entry(deletedInventory).State = EntityState.Deleted;
            Context.SaveChanges();
        }
        
        public void DeleteInventoryRs(int Id)
        {
            Inventory deleteInventoryRs = GetInventoryById(Id);
            deleteInventoryRs.IsDeleted = true;
            UpdateInventory(deleteInventoryRs);
        }

        public void UpdateInventory(Inventory Inventory)
        {
            Context.Entry(Inventory).State = EntityState.Modified;
            Context.SaveChanges();
        }

        // inventory by month -------------------------------------------------------------------------------------------------------------------

        public InventoryByMonth GetInventoryByMonthById(int id)
        {
            return Context.InventoryByMonth.Where(x => x.Id == id && (x.IsDeleted == null || x.IsDeleted == false)).FirstOrDefault();
        }

        public IQueryable<InventoryByMonth> GetInventoryByMonthByWarehouseIdAndProductId(int warehouseId, int productId)
        {
            return Context.InventoryByMonth.Where(x => x.WarehouseId == warehouseId && x.ProductId == productId && (x.IsDeleted == null || x.IsDeleted == false));
        }

        public IQueryable<InventoryByMonth> GetInventoryByMonth(int month, int year)
        {
            return Context.InventoryByMonth.Where(x => x.Month == month && x.Year == year && (x.IsDeleted == null || x.IsDeleted == false));
        }

        public void InsertInventoryByMonth(InventoryByMonth Inventory)
        {
            Context.InventoryByMonth.Add(Inventory);
            Context.Entry(Inventory).State = EntityState.Added;
            Context.SaveChanges();
        }

        public void UpdateInventoryByMonth(InventoryByMonth Inventory)
        {
            Context.Entry(Inventory).State = EntityState.Modified;
            Context.SaveChanges();
        }

        public void DeleteInventoryByMonth(int Id)
        {
            InventoryByMonth deletedInventory = GetInventoryByMonthById(Id);
            Context.InventoryByMonth.Remove(deletedInventory);
            Context.Entry(deletedInventory).State = EntityState.Deleted;
            Context.SaveChanges();
        }

        public void DeleteInventoryByMonthRs(int Id)
        {
            InventoryByMonth deleteInventoryRs = GetInventoryByMonthById(Id);
            deleteInventoryRs.IsDeleted = true;
            UpdateInventoryByMonth(deleteInventoryRs);
        }
    }
}
