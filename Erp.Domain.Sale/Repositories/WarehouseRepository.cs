using Erp.Domain.Sale.Entities;
using Erp.Domain.Sale.Interfaces;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace Erp.Domain.Sale.Repositories
{
    public class WarehouseRepository : GenericRepository<ErpSaleDbContext, Warehouse>, IWarehouseRepository
    {
        public WarehouseRepository(ErpSaleDbContext context)
            : base(context)
        {

        }

        /// <summary>
        /// Get all Warehouse
        /// </summary>
        /// <returns>Warehouse list</returns>
        public IQueryable<Warehouse> GetAllWarehouse()
        {
            return Context.Warehouse.Where(item => (item.IsDeleted == null || item.IsDeleted == false));
        }
        public IQueryable<vwWarehouse> GetvwAllWarehouse()
        {
            return Context.vwWarehouse.Where(item => (item.IsDeleted == null || item.IsDeleted == false));
        }
        /// <summary>
        /// Get Warehouse information by specific id
        /// </summary>
        /// <param name="WarehouseId">Id of Warehouse</param>
        /// <returns></returns>
        public Warehouse GetWarehouseById(int Id)
        {
            return Context.Warehouse.SingleOrDefault(item => item.Id == Id && (item.IsDeleted == null || item.IsDeleted == false));
        }

        /// <summary>
        /// Insert Warehouse into database
        /// </summary>
        /// <param name="Warehouse">Object infomation</param>
        public void InsertWarehouse(Warehouse Warehouse)
        {
            Context.Warehouse.Add(Warehouse);
            Context.Entry(Warehouse).State = EntityState.Added;
            Context.SaveChanges();
        }

        /// <summary>
        /// Delete Warehouse with specific id
        /// </summary>
        /// <param name="Id">Warehouse Id</param>
        public void DeleteWarehouse(int Id)
        {
            Warehouse deletedWarehouse = GetWarehouseById(Id);
            Context.Warehouse.Remove(deletedWarehouse);
            Context.Entry(deletedWarehouse).State = EntityState.Deleted;
            Context.SaveChanges();
        }
        
        /// <summary>
        /// Delete a Warehouse with its Id and Update IsDeleted IF that Warehouse has relationship with others
        /// </summary>
        /// <param name="WarehouseId">Id of Warehouse</param>
        public void DeleteWarehouseRs(int Id)
        {
            Warehouse deleteWarehouseRs = GetWarehouseById(Id);
            deleteWarehouseRs.IsDeleted = true;
            UpdateWarehouse(deleteWarehouseRs);
        }

        /// <summary>
        /// Update Warehouse into database
        /// </summary>
        /// <param name="Warehouse">Warehouse object</param>
        public void UpdateWarehouse(Warehouse Warehouse)
        {
            Context.Entry(Warehouse).State = EntityState.Modified;
            Context.SaveChanges();
        }
    }
}
