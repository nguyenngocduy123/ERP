using Erp.Domain.Account.Entities;
using System.Collections.Generic;
using System.Linq;

namespace Erp.Domain.Account.Interfaces
{
    public interface IReceiptRepository
    {
        /// <summary>
        /// Get all Receipt
        /// </summary>
        /// <returns>Receipt list</returns>
        IQueryable<vwReceipt> GetAllReceipt();
        IQueryable<vwReceipt> GetAllvwReceiptFull();
        IQueryable<Receipt> GetAllReceiptFull();
        IQueryable<Receipt> GetAllReceipts();
        /// <summary>
        /// Get Receipt information by specific id
        /// </summary>
        /// <param name="Id">Id of Receipt</param>
        /// <returns></returns>
        Receipt GetReceiptById(int Id);
        vwReceipt GetvwReceiptById(int Id);
        /// <summary>
        /// Insert Receipt into database
        /// </summary>
        /// <param name="Receipt">Object infomation</param>
        void InsertReceipt(Receipt Receipt);

        /// <summary>
        /// Delete Receipt with specific id
        /// </summary>
        /// <param name="Id">Receipt Id</param>
        void DeleteReceipt(int Id);

        /// <summary>
        /// Delete a Receipt with its Id and Update IsDeleted IF that Receipt has relationship with others
        /// </summary>
        /// <param name="Id">Id of Receipt</param>
        void DeleteReceiptRs(int Id);

        /// <summary>
        /// Update Receipt into database
        /// </summary>
        /// <param name="Receipt">Receipt object</param>
        void UpdateReceipt(Receipt Receipt);
    }
}
