using Erp.Domain.Sale.Entities;
using Erp.Domain.Sale.Interfaces;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace Erp.Domain.Sale.Repositories
{
    public class SupplierRepository : GenericRepository<ErpSaleDbContext, Supplier>, ISupplierRepository
    {
        public SupplierRepository(ErpSaleDbContext context)
            : base(context)
        {

        }

        /// <summary>
        /// Get all Supplier
        /// </summary>
        /// <returns>Supplier list</returns>
        public IQueryable<Supplier> GetAllSupplier()
        {
            return Context.Supplier.Where(item => (item.IsDeleted == null || item.IsDeleted == false));
        }
        public IQueryable<vwSupplier> GetAllvwSupplier()
        {
            return Context.vwSupplier.Where(item => (item.IsDeleted == null || item.IsDeleted == false));
        }
        /// <summary>
        /// Get Supplier information by specific id
        /// </summary>
        /// <param name="SupplierId">Id of Supplier</param>
        /// <returns></returns>
        public Supplier GetSupplierById(int Id)
        {
            return Context.Supplier.SingleOrDefault(item => item.Id == Id && (item.IsDeleted == null || item.IsDeleted == false));
        }
        public vwSupplier GetvwSupplierById(int Id)
        {
            return Context.vwSupplier.SingleOrDefault(item => item.Id == Id && (item.IsDeleted == null || item.IsDeleted == false));
        }
        /// <summary>
        /// Insert Supplier into database
        /// </summary>
        /// <param name="Supplier">Object infomation</param>
        public void InsertSupplier(Supplier Supplier)
        {
            Context.Supplier.Add(Supplier);
            Context.Entry(Supplier).State = EntityState.Added;
            Context.SaveChanges();
        }

        /// <summary>
        /// Delete Supplier with specific id
        /// </summary>
        /// <param name="Id">Supplier Id</param>
        public void DeleteSupplier(int Id)
        {
            Supplier deletedSupplier = GetSupplierById(Id);
            Context.Supplier.Remove(deletedSupplier);
            Context.Entry(deletedSupplier).State = EntityState.Deleted;
            Context.SaveChanges();
        }
        
        /// <summary>
        /// Delete a Supplier with its Id and Update IsDeleted IF that Supplier has relationship with others
        /// </summary>
        /// <param name="SupplierId">Id of Supplier</param>
        public void DeleteSupplierRs(int Id)
        {
            Supplier deleteSupplierRs = GetSupplierById(Id);
            deleteSupplierRs.IsDeleted = true;
            UpdateSupplier(deleteSupplierRs);
        }

        /// <summary>
        /// Update Supplier into database
        /// </summary>
        /// <param name="Supplier">Supplier object</param>
        public void UpdateSupplier(Supplier Supplier)
        {
            Context.Entry(Supplier).State = EntityState.Modified;
            Context.SaveChanges();
        }
    }
}
