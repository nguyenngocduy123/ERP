using Erp.Domain.Staff.Entities;
using System.Collections.Generic;
using System.Linq;

namespace Erp.Domain.Staff.Interfaces
{
    public interface ITaxRateRepository
    {
        /// <summary>
        /// Get all TaxRate
        /// </summary>
        /// <returns>TaxRate list</returns>
        IQueryable<TaxRate> GetAllTaxRate();

        /// <summary>
        /// Get TaxRate information by specific id
        /// </summary>
        /// <param name="Id">Id of TaxRate</param>
        /// <returns></returns>
        TaxRate GetTaxRateById(int Id);

        /// <summary>
        /// Insert TaxRate into database
        /// </summary>
        /// <param name="TaxRate">Object infomation</param>
        void InsertTaxRate(TaxRate TaxRate);

        /// <summary>
        /// Delete TaxRate with specific id
        /// </summary>
        /// <param name="Id">TaxRate Id</param>
        void DeleteTaxRate(int Id);

        /// <summary>
        /// Delete a TaxRate with its Id and Update IsDeleted IF that TaxRate has relationship with others
        /// </summary>
        /// <param name="Id">Id of TaxRate</param>
        void DeleteTaxRateRs(int Id);

        /// <summary>
        /// Update TaxRate into database
        /// </summary>
        /// <param name="TaxRate">TaxRate object</param>
        void UpdateTaxRate(TaxRate TaxRate);
    }
}
