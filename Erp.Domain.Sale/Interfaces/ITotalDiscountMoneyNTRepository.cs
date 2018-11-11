using Erp.Domain.Sale.Entities;
using System.Collections.Generic;
using System.Linq;

namespace Erp.Domain.Sale.Interfaces
{
    public interface ITotalDiscountMoneyNTRepository
    {
        /// <summary>
        /// Get all TotalDiscountMoneyNT
        /// </summary>
        /// <returns>TotalDiscountMoneyNT list</returns>
        IQueryable<TotalDiscountMoneyNT> GetAllTotalDiscountMoneyNT();
        IQueryable<vwTotalDiscountMoneyNT> GetvwAllTotalDiscountMoneyNT();
        /// <summary>
        /// Get TotalDiscountMoneyNT information by specific id
        /// </summary>
        /// <param name="Id">Id of TotalDiscountMoneyNT</param>
        /// <returns></returns>
        TotalDiscountMoneyNT GetTotalDiscountMoneyNTById(int Id);
        vwTotalDiscountMoneyNT GetvwTotalDiscountMoneyNTById(int Id);
        /// <summary>
        /// Insert TotalDiscountMoneyNT into database
        /// </summary>
        /// <param name="TotalDiscountMoneyNT">Object infomation</param>
        void InsertTotalDiscountMoneyNT(TotalDiscountMoneyNT TotalDiscountMoneyNT);

        /// <summary>
        /// Delete TotalDiscountMoneyNT with specific id
        /// </summary>
        /// <param name="Id">TotalDiscountMoneyNT Id</param>
        void DeleteTotalDiscountMoneyNT(int Id);

        /// <summary>
        /// Delete a TotalDiscountMoneyNT with its Id and Update IsDeleted IF that TotalDiscountMoneyNT has relationship with others
        /// </summary>
        /// <param name="Id">Id of TotalDiscountMoneyNT</param>
        void DeleteTotalDiscountMoneyNTRs(int Id);

        /// <summary>
        /// Update TotalDiscountMoneyNT into database
        /// </summary>
        /// <param name="TotalDiscountMoneyNT">TotalDiscountMoneyNT object</param>
        void UpdateTotalDiscountMoneyNT(TotalDiscountMoneyNT TotalDiscountMoneyNT);
    }
}
