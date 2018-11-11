using Erp.Domain.Account.Entities;
using System.Collections.Generic;
using System.Linq;

namespace Erp.Domain.Account.Interfaces
{
    public interface IPaymentDetailRepository
    {
        /// <summary>
        /// Get all ReceiptDetail
        /// </summary>
        /// <returns>ReceiptDetail list</returns>
        IQueryable<PaymentDetail> GetAllPaymentDetail();

        /// <summary>
        /// Get ReceiptDetail information by specific id
        /// </summary>
        /// <param name="Id">Id of ReceiptDetail</param>
        /// <returns></returns>
        PaymentDetail GetPaymentDetailById(int Id);
        PaymentDetail GetPaymentDetailByPaymentId(int PaymentId);
        IQueryable<PaymentDetail> GetAllPaymentDetailByPaymentId(int PaymentId);

        /// <summary>
        /// Insert ReceiptDetail into database
        /// </summary>
        /// <param name="ReceiptDetail">Object infomation</param>
        void InsertPaymentDetail(PaymentDetail PaymentDetail);

        /// <summary>
        /// Delete ReceiptDetail with specific id
        /// </summary>
        /// <param name="Id">ReceiptDetail Id</param>
        void DeletePaymentDetail(int Id);

        /// <summary>
        /// Delete a ReceiptDetail with its Id and Update IsDeleted IF that ReceiptDetail has relationship with others
        /// </summary>
        /// <param name="Id">Id of ReceiptDetail</param>
        void DeletePaymentDetailRs(int Id);

        /// <summary>
        /// Update ReceiptDetail into database
        /// </summary>
        /// <param name="ReceiptDetail">ReceiptDetail object</param>
        void UpdatePaymentDetail(PaymentDetail PaymentDetail);
    }
}
