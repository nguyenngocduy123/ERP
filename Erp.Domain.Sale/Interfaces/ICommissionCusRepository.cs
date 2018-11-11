using Erp.Domain.Sale.Entities;
using System.Collections.Generic;
using System.Linq;

namespace Erp.Domain.Sale.Interfaces
{
    public interface ICommissionCusRepository
    {
        /// <summary>
        /// Get all CommissionCus
        /// </summary>
        /// <returns>CommissionCus list</returns>
        IQueryable<CommissionCus> GetAllCommissionCus();

        /// <summary>
        /// Get CommissionCus information by specific id
        /// </summary>
        /// <param name="Id">Id of CommissionCus</param>
        /// <returns></returns>
        CommissionCus GetCommissionCusById(int Id);

        /// <summary>
        /// Insert CommissionCus into database
        /// </summary>
        /// <param name="CommissionCus">Object infomation</param>
        void InsertCommissionCus(CommissionCus CommissionCus);

        /// <summary>
        /// Delete CommissionCus with specific id
        /// </summary>
        /// <param name="Id">CommissionCus Id</param>
        void DeleteCommissionCus(int Id);

        /// <summary>
        /// Delete a CommissionCus with its Id and Update IsDeleted IF that CommissionCus has relationship with others
        /// </summary>
        /// <param name="Id">Id of CommissionCus</param>
        void DeleteCommissionCusRs(int Id);

        /// <summary>
        /// Update CommissionCus into database
        /// </summary>
        /// <param name="CommissionCus">CommissionCus object</param>
        void UpdateCommissionCus(CommissionCus CommissionCus);
    }
}
