using Erp.Domain.Account.Entities;
using System.Collections.Generic;
using System.Linq;

namespace Erp.Domain.Account.Interfaces
{
    public interface IContractSellRepository
    {
        /// <summary>
        /// Get all ContractSell
        /// </summary>
        /// <returns>ContractSell list</returns>
        IQueryable<ContractSell> GetAllContractSell();
        IQueryable<vwContractSell> GetAllvwContractSell();
        /// <summary>
        /// Get ContractSell information by specific id
        /// </summary>
        /// <param name="Id">Id of ContractSell</param>
        /// <returns></returns>
        ContractSell GetContractSellById(int Id);
        vwContractSell GetvwContractSellById(int Id);
        /// <summary>
        /// Insert ContractSell into database
        /// </summary>
        /// <param name="ContractSell">Object infomation</param>
        void InsertContractSell(ContractSell ContractSell);

        /// <summary>
        /// Delete ContractSell with specific id
        /// </summary>
        /// <param name="Id">ContractSell Id</param>
        void DeleteContractSell(int Id);

        /// <summary>
        /// Delete a ContractSell with its Id and Update IsDeleted IF that ContractSell has relationship with others
        /// </summary>
        /// <param name="Id">Id of ContractSell</param>
        void DeleteContractSellRs(int Id);

        /// <summary>
        /// Update ContractSell into database
        /// </summary>
        /// <param name="ContractSell">ContractSell object</param>
        void UpdateContractSell(ContractSell ContractSell);
    }
}
