using Erp.Domain.Sale.Entities;
using System.Collections.Generic;
using System.Linq;

namespace Erp.Domain.Sale.Interfaces
{
    public interface ISupplierRepository
    {
        /// <summary>
        /// Get all Supplier
        /// </summary>
        /// <returns>Supplier list</returns>
        IQueryable<Supplier> GetAllSupplier();
        IQueryable<vwSupplier> GetAllvwSupplier();
        /// <summary>
        /// Get Supplier information by specific id
        /// </summary>
        /// <param name="Id">Id of Supplier</param>
        /// <returns></returns>
        Supplier GetSupplierById(int Id);
        vwSupplier GetvwSupplierById(int Id);
        /// <summary>
        /// Insert Supplier into database
        /// </summary>
        /// <param name="Supplier">Object infomation</param>
        void InsertSupplier(Supplier Supplier);

        /// <summary>
        /// Delete Supplier with specific id
        /// </summary>
        /// <param name="Id">Supplier Id</param>
        void DeleteSupplier(int Id);

        /// <summary>
        /// Delete a Supplier with its Id and Update IsDeleted IF that Supplier has relationship with others
        /// </summary>
        /// <param name="Id">Id of Supplier</param>
        void DeleteSupplierRs(int Id);

        /// <summary>
        /// Update Supplier into database
        /// </summary>
        /// <param name="Supplier">Supplier object</param>
        void UpdateSupplier(Supplier Supplier);
    }
}
