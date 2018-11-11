using Erp.Domain.Account.Entities;
using System.Collections.Generic;
using System.Linq;

namespace Erp.Domain.Account.Interfaces
{
    public interface ITransactionLiabilitiesRepository
    {
        /// <summary>
        /// Get all Transaction
        /// </summary>
        /// <returns>Transaction list</returns>
        IQueryable<TransactionLiabilities> GetAllTransaction();
        IQueryable<vwTransactionLiabilities> GetAllvwTransaction();

        IQueryable<vwAccount_Liabilities> GetvwAccount_Liabilities();

        /// <summary>
        /// Get Transaction information by specific id
        /// </summary>
        /// <param name="Id">Id of Transaction</param>
        /// <returns></returns>
        TransactionLiabilities GetTransactionById(int Id);

        /// <summary>
        /// Insert Transaction into database
        /// </summary>
        /// <param name="Transaction">Object infomation</param>
        void InsertTransaction(TransactionLiabilities Transaction);

        /// <summary>
        /// Delete Transaction with specific id
        /// </summary>
        /// <param name="Id">Transaction Id</param>
        void DeleteTransaction(int Id);

        /// <summary>
        /// Delete a Transaction with its Id and Update IsDeleted IF that Transaction has relationship with others
        /// </summary>
        /// <param name="Id">Id of Transaction</param>
        void DeleteTransactionRs(int Id);

        /// <summary>
        /// Update Transaction into database
        /// </summary>
        /// <param name="Transaction">Transaction object</param>
        void UpdateTransaction(TransactionLiabilities Transaction);
    }
}
