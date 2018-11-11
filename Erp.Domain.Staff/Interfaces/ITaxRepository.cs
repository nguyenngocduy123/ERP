using Erp.Domain.Staff.Entities;
using System.Collections.Generic;
using System.Linq;

namespace Erp.Domain.Staff.Interfaces
{
    public interface ITaxRepository
    {
        /// <summary>
        /// Get all Tax
        /// </summary>
        /// <returns>Tax list</returns>
        IQueryable<Tax> GetAllTax();
        IQueryable<vwTax> GetvwAllTax();

        /// <summary>
        /// Get Tax information by specific id
        /// </summary>
        /// <param name="Id">Id of Tax</param>
        /// <returns></returns>
        Tax GetTaxById(int Id);

        /// <summary>
        /// Insert Tax into database
        /// </summary>
        /// <param name="Tax">Object infomation</param>
        void InsertTax(Tax Tax);

        /// <summary>
        /// Delete Tax with specific id
        /// </summary>
        /// <param name="Id">Tax Id</param>
        void DeleteTax(int Id);

        /// <summary>
        /// Delete a Tax with its Id and Update IsDeleted IF that Tax has relationship with others
        /// </summary>
        /// <param name="Id">Id of Tax</param>
        void DeleteTaxRs(int Id);

        /// <summary>
        /// Update Tax into database
        /// </summary>
        /// <param name="Tax">Tax object</param>
        void UpdateTax(Tax Tax);
    }
}
