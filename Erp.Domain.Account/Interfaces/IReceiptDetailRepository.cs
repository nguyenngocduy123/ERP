using Erp.Domain.Account.Entities;
using System.Collections.Generic;
using System.Linq;

namespace Erp.Domain.Account.Interfaces
{
    public interface IReceiptDetailRepository
    {
        /// <summary>
        /// Get all ReceiptDetail
        /// </summary>
        /// <returns>ReceiptDetail list</returns>
        IQueryable<ReceiptDetail> GetAllReceiptDetail();
        IQueryable<ReceiptDetail> GetAllReceiptDetailFull();
        /// <summary>
        /// Get ReceiptDetail information by specific id
        /// </summary>
        /// <param name="Id">Id of ReceiptDetail</param>
        /// <returns></returns>
        ReceiptDetail GetReceiptDetailById(int Id);
        ReceiptDetail GetReceiptDetailByReceiptId(int ReceiptId);
        IQueryable<ReceiptDetail> GetAllReceiptDetailByReceiptId(int ReceiptId);

        /// <summary>
        /// Insert ReceiptDetail into database
        /// </summary>
        /// <param name="ReceiptDetail">Object infomation</param>
        void InsertReceiptDetail(ReceiptDetail ReceiptDetail);

        /// <summary>
        /// Delete ReceiptDetail with specific id
        /// </summary>
        /// <param name="Id">ReceiptDetail Id</param>
        void DeleteReceiptDetail(int Id);

        /// <summary>
        /// Delete a ReceiptDetail with its Id and Update IsDeleted IF that ReceiptDetail has relationship with others
        /// </summary>
        /// <param name="Id">Id of ReceiptDetail</param>
        void DeleteReceiptDetailRs(int Id);

        /// <summary>
        /// Update ReceiptDetail into database
        /// </summary>
        /// <param name="ReceiptDetail">ReceiptDetail object</param>
        void UpdateReceiptDetail(ReceiptDetail ReceiptDetail);
    }
}
