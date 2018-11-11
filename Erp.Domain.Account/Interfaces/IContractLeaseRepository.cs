using Erp.Domain.Account.Entities;
using System.Collections.Generic;
using System.Linq;

namespace Erp.Domain.Account.Interfaces
{
    public interface IContractLeaseRepository
    {
        /// <summary>
        /// Get all ContractLease
        /// </summary>
        /// <returns>ContractLease list</returns>
        IQueryable<ContractLease> GetAllContractLease();
        IQueryable<vwContractLease> GetAllvwContractLease();
        /// <summary>
        /// Get ContractLease information by specific id
        /// </summary>
        /// <param name="Id">Id of ContractLease</param>
        /// <returns></returns>
        ContractLease GetContractLeaseById(int Id);
        vwContractLease GetvwContractLeaseById(int Id);
        /// <summary>
        /// Insert ContractLease into database
        /// </summary>
        /// <param name="ContractLease">Object infomation</param>
        void InsertContractLease(ContractLease ContractLease);

        /// <summary>
        /// Delete ContractLease with specific id
        /// </summary>
        /// <param name="Id">ContractLease Id</param>
        void DeleteContractLease(int Id);

        /// <summary>
        /// Delete a ContractLease with its Id and Update IsDeleted IF that ContractLease has relationship with others
        /// </summary>
        /// <param name="Id">Id of ContractLease</param>
        void DeleteContractLeaseRs(int Id);

        /// <summary>
        /// Update ContractLease into database
        /// </summary>
        /// <param name="ContractLease">ContractLease object</param>
        void UpdateContractLease(ContractLease ContractLease);
    }
}
