using Erp.Domain.Staff.Entities;
using System.Collections.Generic;
using System.Linq;

namespace Erp.Domain.Staff.Interfaces
{
    public interface ITaxIncomePersonDetailRepository
    {
        /// <summary>
        /// Get all TaxIncomePersonDetail
        /// </summary>
        /// <returns>TaxIncomePersonDetail list</returns>
        IQueryable<TaxIncomePersonDetail> GetAllTaxIncomePersonDetail();

        IQueryable<vwTaxIncomePersonDetail> GetAllvwTaxIncomePersonDetail();

        /// <summary>
        /// Get TaxIncomePersonDetail information by specific id
        /// </summary>
        /// <param name="Id">Id of TaxIncomePersonDetail</param>
        /// <returns></returns>
        TaxIncomePersonDetail GetTaxIncomePersonDetailById(int Id);

        /// <summary>
        /// Insert TaxIncomePersonDetail into database
        /// </summary>
        /// <param name="TaxIncomePersonDetail">Object infomation</param>
        void InsertTaxIncomePersonDetail(TaxIncomePersonDetail TaxIncomePersonDetail);

        /// <summary>
        /// Delete TaxIncomePersonDetail with specific id
        /// </summary>
        /// <param name="Id">TaxIncomePersonDetail Id</param>
        void DeleteTaxIncomePersonDetail(int Id);

        /// <summary>
        /// Delete a TaxIncomePersonDetail with its Id and Update IsDeleted IF that TaxIncomePersonDetail has relationship with others
        /// </summary>
        /// <param name="Id">Id of TaxIncomePersonDetail</param>
        void DeleteTaxIncomePersonDetailRs(int Id);

        /// <summary>
        /// Update TaxIncomePersonDetail into database
        /// </summary>
        /// <param name="TaxIncomePersonDetail">TaxIncomePersonDetail object</param>
        void UpdateTaxIncomePersonDetail(TaxIncomePersonDetail TaxIncomePersonDetail);
    }
}
