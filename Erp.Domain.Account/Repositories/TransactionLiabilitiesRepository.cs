using Erp.Domain.Account.Entities;
using Erp.Domain.Account.Interfaces;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace Erp.Domain.Account.Repositories
{
    public class TransactionLiabilitiesRepository : GenericRepository<ErpAccountDbContext, TransactionLiabilities>, ITransactionLiabilitiesRepository
    {
        public TransactionLiabilitiesRepository(ErpAccountDbContext context)
            : base(context)
        {

        }

        /// <summary>
        /// Get all Transaction
        /// </summary>
        /// <returns>Transaction list</returns>
        public IQueryable<TransactionLiabilities> GetAllTransaction()
        {
            return Context.TransactionLiabilities.Where(item => (item.IsDeleted == null || item.IsDeleted == false));
        }

        public IQueryable<vwTransactionLiabilities> GetAllvwTransaction()
        {
            return Context.vwTransactionLiabilities.Where(item => (item.IsDeleted == null || item.IsDeleted == false));
        }

        public IQueryable<vwAccount_Liabilities> GetvwAccount_Liabilities()
        {
            return Context.vwAccount_Liabilities;
        }

        /// <summary>
        /// Get Transaction information by specific id
        /// </summary>
        /// <param name="TransactionId">Id of Transaction</param>
        /// <returns></returns>
        public TransactionLiabilities GetTransactionById(int Id)
        {
            return Context.TransactionLiabilities.SingleOrDefault(item => item.Id == Id && (item.IsDeleted == null || item.IsDeleted == false));
        }

        /// <summary>
        /// Insert Transaction into database
        /// </summary>
        /// <param name="Transaction">Object infomation</param>
        public void InsertTransaction(TransactionLiabilities Transaction)
        {
            Context.TransactionLiabilities.Add(Transaction);
            Context.Entry(Transaction).State = EntityState.Added;
            Context.SaveChanges();
        }

        /// <summary>
        /// Delete Transaction with specific id
        /// </summary>
        /// <param name="Id">Transaction Id</param>
        public void DeleteTransaction(int Id)
        {
            TransactionLiabilities deletedTransaction = GetTransactionById(Id);
            Context.TransactionLiabilities.Remove(deletedTransaction);
            Context.Entry(deletedTransaction).State = EntityState.Deleted;
            Context.SaveChanges();
        }
        
        /// <summary>
        /// Delete a Transaction with its Id and Update IsDeleted IF that Transaction has relationship with others
        /// </summary>
        /// <param name="TransactionId">Id of Transaction</param>
        public void DeleteTransactionRs(int Id)
        {
            TransactionLiabilities deleteTransactionRs = GetTransactionById(Id);
            deleteTransactionRs.IsDeleted = true;
            UpdateTransaction(deleteTransactionRs);
        }

        /// <summary>
        /// Update Transaction into database
        /// </summary>
        /// <param name="Transaction">Transaction object</param>
        public void UpdateTransaction(TransactionLiabilities Transaction)
        {
            Context.Entry(Transaction).State = EntityState.Modified;
            Context.SaveChanges();
        }
    }
}
