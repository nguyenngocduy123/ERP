using Erp.Domain.Account.Entities;
using Erp.Domain.Account.Interfaces;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity;
using System.Linq;

namespace Erp.Domain.Account.Repositories
{
    public class TransactionRepository : GenericRepository<ErpAccountDbContext, Transaction>, ITransactionRepository
    {
        public TransactionRepository(ErpAccountDbContext context)
            : base(context)
        {

        }

        /// <summary>
        /// Get all Transaction
        /// </summary>
        /// <returns>Transaction list</returns>
        public IQueryable<Transaction> GetAllTransaction()
        {
            return Context.Transaction.Where(item => (item.IsDeleted == null || item.IsDeleted == false));
        }

        /// <summary>
        /// Get Transaction information by specific id
        /// </summary>
        /// <param name="TransactionId">Id of Transaction</param>
        /// <returns></returns>
        public Transaction GetTransactionById(int Id)
        {
            return Context.Transaction.SingleOrDefault(item => item.Id == Id && (item.IsDeleted == null || item.IsDeleted == false));
        }

        /// <summary>
        /// Insert Transaction into database
        /// </summary>
        /// <param name="Transaction">Object infomation</param>
        public void InsertTransaction(Transaction Transaction)
        {
            Context.Transaction.Add(Transaction);
            Context.Entry(Transaction).State = EntityState.Added;
            Context.SaveChanges();
        }

        /// <summary>
        /// Delete Transaction with specific id
        /// </summary>
        /// <param name="Id">Transaction Id</param>
        public void DeleteTransaction(int Id)
        {
            Transaction deletedTransaction = GetTransactionById(Id);
            Context.Transaction.Remove(deletedTransaction);
            Context.Entry(deletedTransaction).State = EntityState.Deleted;
            Context.SaveChanges();
        }
        
        /// <summary>
        /// Delete a Transaction with its Id and Update IsDeleted IF that Transaction has relationship with others
        /// </summary>
        /// <param name="TransactionId">Id of Transaction</param>
        public void DeleteTransactionRs(int Id)
        {
            Transaction deleteTransactionRs = GetTransactionById(Id);
            deleteTransactionRs.IsDeleted = true;
            UpdateTransaction(deleteTransactionRs);
        }

        /// <summary>
        /// Update Transaction into database
        /// </summary>
        /// <param name="Transaction">Transaction object</param>
        public void UpdateTransaction(Transaction Transaction)
        {
            Context.Entry(Transaction).State = EntityState.Modified;
            Context.SaveChanges();
        }

        /// <summary>
        /// Get all TransactionRelationship
        /// </summary>
        /// <returns>TransactionRelationship list</returns>
        public IQueryable<TransactionRelationship> GetAllTransactionRelationship()
        {
            return Context.TransactionRelationship.Where(item => (item.IsDeleted == null || item.IsDeleted == false));
        }

        public IQueryable<vwTransactionRelationship> GetAllvwTransactionRelationship()
        {
            return Context.vwTransactionRelationship.Where(item => (item.IsDeleted == null || item.IsDeleted == false));
        }

        /// <summary>
        /// Get TransactionRelationship information by specific id
        /// </summary>
        /// <param name="TransactionRelationshipId">Id of TransactionRelationship</param>
        /// <returns></returns>
        public TransactionRelationship GetTransactionRelationshipById(int Id)
        {
            return Context.TransactionRelationship.SingleOrDefault(item => item.Id == Id && (item.IsDeleted == null || item.IsDeleted == false));
        }

        /// <summary>
        /// Insert TransactionRelationship into database
        /// </summary>
        /// <param name="TransactionRelationship">Object infomation</param>
        public void InsertTransactionRelationship(TransactionRelationship TransactionRelationship)
        {
            Context.TransactionRelationship.Add(TransactionRelationship);
            Context.Entry(TransactionRelationship).State = EntityState.Added;
            Context.SaveChanges();
        }

        /// <summary>
        /// Delete TransactionRelationship with specific id
        /// </summary>
        /// <param name="Id">TransactionRelationship Id</param>
        public void DeleteTransactionRelationship(int Id)
        {
            TransactionRelationship deletedTransactionRelationship = GetTransactionRelationshipById(Id);
            Context.TransactionRelationship.Remove(deletedTransactionRelationship);
            Context.Entry(deletedTransactionRelationship).State = EntityState.Deleted;
            Context.SaveChanges();
        }

        /// <summary>
        /// Delete a TransactionRelationship with its Id and Update IsDeleted IF that TransactionRelationship has relationship with others
        /// </summary>
        /// <param name="TransactionRelationshipId">Id of TransactionRelationship</param>
        public void DeleteTransactionRelationshipRs(int Id)
        {
            TransactionRelationship deleteTransactionRelationshipRs = GetTransactionRelationshipById(Id);
            deleteTransactionRelationshipRs.IsDeleted = true;
            UpdateTransactionRelationship(deleteTransactionRelationshipRs);
        }

        /// <summary>
        /// Update TransactionRelationship into database
        /// </summary>
        /// <param name="TransactionRelationship">TransactionRelationship object</param>
        public void UpdateTransactionRelationship(TransactionRelationship TransactionRelationship)
        {
            Context.Entry(TransactionRelationship).State = EntityState.Modified;
            Context.SaveChanges();
        }
    }
}
