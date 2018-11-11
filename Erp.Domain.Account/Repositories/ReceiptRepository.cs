using Erp.Domain.Account.Entities;
using Erp.Domain.Account.Interfaces;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity;
using System.Linq;

namespace Erp.Domain.Account.Repositories
{
    public class ReceiptRepository : GenericRepository<ErpAccountDbContext, Receipt>, IReceiptRepository
    {
        public ReceiptRepository(ErpAccountDbContext context)
            : base(context)
        {

        }

        /// <summary>
        /// Get all Receipt
        /// </summary>
        /// <returns>Receipt list</returns>
        public IQueryable<vwReceipt> GetAllReceipt()
        {
            return Context.vwReceipt.Where(item => (item.IsDeleted == null || item.IsDeleted == false));
        }
        public IQueryable<Receipt> GetAllReceipts()
        {
            return Context.Receipt.Where(item => (item.IsDeleted == null || item.IsDeleted == false));
        }
        public IQueryable<vwReceipt> GetAllvwReceiptFull()
        {
            return Context.vwReceipt;
        }
        public IQueryable<Receipt> GetAllReceiptFull()
        {
            return Context.Receipt;
        }
        /// <summary>
        /// Get Receipt information by specific id
        /// </summary>
        /// <param name="ReceiptId">Id of Receipt</param>
        /// <returns></returns>
        public Receipt GetReceiptById(int Id)
        {
            return Context.Receipt.SingleOrDefault(item => item.Id == Id && (item.IsDeleted == null || item.IsDeleted == false));
        }
        public vwReceipt GetvwReceiptById(int Id)
        {
            return Context.vwReceipt.SingleOrDefault(item => item.Id == Id);
        }
        /// <summary>
        /// Insert Receipt into database
        /// </summary>
        /// <param name="Receipt">Object infomation</param>
        public void InsertReceipt(Receipt Receipt)
        {
            Context.Receipt.Add(Receipt);
            Context.Entry(Receipt).State = EntityState.Added;
            Context.SaveChanges();
        }

        /// <summary>
        /// Delete Receipt with specific id
        /// </summary>
        /// <param name="Id">Receipt Id</param>
        public void DeleteReceipt(int Id)
        {
            Receipt deletedReceipt = GetReceiptById(Id);
            Context.Receipt.Remove(deletedReceipt);
            Context.Entry(deletedReceipt).State = EntityState.Deleted;
            Context.SaveChanges();
        }
        
        /// <summary>
        /// Delete a Receipt with its Id and Update IsDeleted IF that Receipt has relationship with others
        /// </summary>
        /// <param name="ReceiptId">Id of Receipt</param>
        public void DeleteReceiptRs(int Id)
        {
            Receipt deleteReceiptRs = GetReceiptById(Id);
            deleteReceiptRs.IsDeleted = true;
            UpdateReceipt(deleteReceiptRs);
        }

        /// <summary>
        /// Update Receipt into database
        /// </summary>
        /// <param name="Receipt">Receipt object</param>
        public void UpdateReceipt(Receipt Receipt)
        {
            Context.Entry(Receipt).State = EntityState.Modified;
            Context.SaveChanges();
        }
    }
}
