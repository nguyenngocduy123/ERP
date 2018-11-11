using Erp.Domain.Account.Entities;
using System.Collections.Generic;
using System.Linq;

namespace Erp.Domain.Account.Interfaces
{
    public interface IContractRepository
    {
        /// <summary>
        /// Get all Contract
        /// </summary>
        /// <returns>Contract list</returns>
        IQueryable<Contract> GetAllContract();
        IQueryable<vwContract> GetAllvwContract();
        IQueryable<vwLogContractbyCondos> GetvwLogContractbyCondos(int? CondosId);
        /// <summary>
        /// Get Contract information by specific id
        /// </summary>
        /// <param name="Id">Id of Contract</param>
        /// <returns></returns>
        Contract GetContractById(int? Id);
        Contract GetContractByTransactionCode(string Id);

        vwContract GetvwContractById(int Id);
        /// <summary>
        /// Insert Contract into database
        /// </summary>
        /// <param name="Contract">Object infomation</param>
        void InsertContract(Contract Contract);

        /// <summary>
        /// Delete Contract with specific id
        /// </summary>
        /// <param name="Id">Contract Id</param>
        void DeleteContract(int Id);

        /// <summary>
        /// Delete a Contract with its Id and Update IsDeleted IF that Contract has relationship with others
        /// </summary>
        /// <param name="Id">Id of Contract</param>
        void DeleteContractRs(int Id);

        /// <summary>
        /// Update Contract into database
        /// </summary>
        /// <param name="Contract">Contract object</param>
        void UpdateContract(Contract Contract);
    }
}
