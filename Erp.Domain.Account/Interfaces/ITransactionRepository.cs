using Erp.Domain.Account.Entities;
using System.Collections.Generic;
using System.Linq;

namespace Erp.Domain.Account.Interfaces
{
    public interface ITransactionRepository
    {
        /// <summary>
        /// Get all Transaction
        /// </summary>
        /// <returns>Transaction list</returns>
        IQueryable<Transaction> GetAllTransaction();

        /// <summary>
        /// Get Transaction information by specific id
        /// </summary>
        /// <param name="Id">Id of Transaction</param>
        /// <returns></returns>
        Transaction GetTransactionById(int Id);

        /// <summary>
        /// Insert Transaction into database
        /// </summary>
        /// <param name="Transaction">Object infomation</param>
        void InsertTransaction(Transaction Transaction);

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
        void UpdateTransaction(Transaction Transaction);

        /// <summary>
        /// Get all TransactionRelationship
        /// </summary>
        /// <returns>TransactionRelationship list</returns>
        IQueryable<TransactionRelationship> GetAllTransactionRelationship();
        IQueryable<vwTransactionRelationship> GetAllvwTransactionRelationship();

        /// <summary>
        /// Get TransactionRelationship information by specific id
        /// </summary>
        /// <param name="Id">Id of TransactionRelationship</param>
        /// <returns></returns>
        TransactionRelationship GetTransactionRelationshipById(int Id);

        /// <summary>
        /// Insert TransactionRelationship into database
        /// </summary>
        /// <param name="TransactionRelationship">Object infomation</param>
        void InsertTransactionRelationship(TransactionRelationship TransactionRelationship);

        /// <summary>
        /// Delete TransactionRelationship with specific id
        /// </summary>
        /// <param name="Id">TransactionRelationship Id</param>
        void DeleteTransactionRelationship(int Id);

        /// <summary>
        /// Delete a TransactionRelationship with its Id and Update IsDeleted IF that TransactionRelationship has relationship with others
        /// </summary>
        /// <param name="Id">Id of TransactionRelationship</param>
        void DeleteTransactionRelationshipRs(int Id);

        /// <summary>
        /// Update TransactionRelationship into database
        /// </summary>
        /// <param name="TransactionRelationship">TransactionRelationship object</param>
        void UpdateTransactionRelationship(TransactionRelationship TransactionRelationship);
    }
}
