using Erp.Domain.Sale.Entities;
using Erp.Domain.Sale.Interfaces;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity;
using System.Linq;

namespace Erp.Domain.Sale.Repositories
{
    public class WarehouseLocationItemRepository : GenericRepository<ErpSaleDbContext, WarehouseLocationItem>, IWarehouseLocationItemRepository
    {
        public WarehouseLocationItemRepository(ErpSaleDbContext context)
            : base(context)
        {

        }

        public IQueryable<WarehouseLocationItem> GetAllWarehouseLocationItem()
        {
            return Context.WarehouseLocationItem.Where(item => (item.IsDeleted == null || item.IsDeleted == false) && item.IsOut == false);
        }

        public IQueryable<WarehouseLocationItem> GetAllLocationItem()
        {
            return Context.WarehouseLocationItem.Where(item => (item.IsDeleted == null || item.IsDeleted == false));
        }
        public IQueryable<vwWarehouseLocationItem> GetAllvwWarehouseLocationItem()
        {
            return Context.vwWarehouseLocationItem.Where(item => (item.IsDeleted == null || item.IsDeleted == false) && item.IsOut == false);
        }

        
        public WarehouseLocationItem GetWarehouseLocationItemById(int Id)
        {
            return Context.WarehouseLocationItem.SingleOrDefault(item => item.Id == Id && (item.IsDeleted == null || item.IsDeleted == false));
        }

        public WarehouseLocationItem GetWarehouseLocationItemBySerialNumber(int? warehouseId, int? productId, string serialNumber)
        {
            return Context.WarehouseLocationItem.SingleOrDefault(item => item.WarehouseId == warehouseId && item.ProductId == productId && item.SN == serialNumber && (item.IsDeleted == null || item.IsDeleted == false) && item.IsOut == false);
        }

        
        public void InsertWarehouseLocationItem(WarehouseLocationItem WarehouseLocationItem)
        {
            Context.WarehouseLocationItem.Add(WarehouseLocationItem);
            Context.Entry(WarehouseLocationItem).State = EntityState.Added;
            Context.SaveChanges();
        }

        public void InsertWarehouseLocationItem(List<WarehouseLocationItem> listItem)
        {
            for (int i = 0; i < listItem.Count; i++)
            {
                Context.WarehouseLocationItem.Add(listItem[i]);
                Context.Entry(listItem[i]).State = EntityState.Added;
            }

            Context.SaveChanges();
        }

        
        public void DeleteWarehouseLocationItem(int Id)
        {
            WarehouseLocationItem deletedWarehouseLocationItem = GetWarehouseLocationItemById(Id);
            Context.WarehouseLocationItem.Remove(deletedWarehouseLocationItem);
            Context.Entry(deletedWarehouseLocationItem).State = EntityState.Deleted;
            Context.SaveChanges();
        }
        
       
        public void DeleteWarehouseLocationItemRs(int Id)
        {
            WarehouseLocationItem deleteWarehouseLocationItemRs = GetWarehouseLocationItemById(Id);
            deleteWarehouseLocationItemRs.IsDeleted = true;
            UpdateWarehouseLocationItem(deleteWarehouseLocationItemRs);
        }

        
        public void UpdateWarehouseLocationItem(WarehouseLocationItem WarehouseLocationItem)
        {
            Context.Entry(WarehouseLocationItem).State = EntityState.Modified;
            Context.SaveChanges();
        }
    }
}
