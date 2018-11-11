using Erp.Domain.Account.Entities;
using Erp.Domain.Account.Interfaces;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace Erp.Domain.Account.Repositories
{
    public class PaymentDetailRepository : GenericRepository<ErpAccountDbContext, PaymentDetail>, IPaymentDetailRepository
    {
        public PaymentDetailRepository(ErpAccountDbContext context)
            : base(context)
        {

        }

        /// <summary>
        /// Get all ReceiptDetail
        /// </summary>
        /// <returns>ReceiptDetail list</returns>
        public IQueryable<PaymentDetail> GetAllPaymentDetail()
        {
            return Context.PaymentDetail.Where(item => (item.IsDeleted == null || item.IsDeleted == false));
        }

        /// <summary>
        /// Get ReceiptDetail information by specific id
        /// </summary>
        /// <param name="ReceiptDetailId">Id of ReceiptDetail</param>
        /// <returns></returns>
        public PaymentDetail GetPaymentDetailById(int Id)
        {
            return Context.PaymentDetail.SingleOrDefault(item => item.Id == Id && (item.IsDeleted == null || item.IsDeleted == false));
        }
        public PaymentDetail GetPaymentDetailByPaymentId(int PaymentId)
        {
            return Context.PaymentDetail.SingleOrDefault(item => item.PaymentId == PaymentId && (item.IsDeleted == null || item.IsDeleted == false));
        }


        public IQueryable<PaymentDetail> GetAllPaymentDetailByPaymentId(int PaymentId)
        {
            return Context.PaymentDetail.Where(item => item.PaymentId == PaymentId && (item.IsDeleted == null || item.IsDeleted == false));
        }
        /// <summary>
        /// Insert ReceiptDetail into database
        /// </summary>
        /// <param name="ReceiptDetail">Object infomation</param>
        public void InsertPaymentDetail(PaymentDetail PaymentDetail)
        {
            Context.PaymentDetail.Add(PaymentDetail);
            Context.Entry(PaymentDetail).State = EntityState.Added;
            Context.SaveChanges();
        }

        /// <summary>
        /// Delete ReceiptDetail with specific id
        /// </summary>
        /// <param name="Id">ReceiptDetail Id</param>
        public void DeletePaymentDetail(int Id)
        {
            PaymentDetail deletedPaymentDetail = GetPaymentDetailById(Id);
            Context.PaymentDetail.Remove(deletedPaymentDetail);
            Context.Entry(deletedPaymentDetail).State = EntityState.Deleted;
            Context.SaveChanges();
        }
        
        /// <summary>
        /// Delete a ReceiptDetail with its Id and Update IsDeleted IF that ReceiptDetail has relationship with others
        /// </summary>
        /// <param name="ReceiptDetailId">Id of ReceiptDetail</param>
        public void DeletePaymentDetailRs(int Id)
        {
            PaymentDetail deletePaymentDetailRs = GetPaymentDetailById(Id);
            deletePaymentDetailRs.IsDeleted = true;
            UpdatePaymentDetail(deletePaymentDetailRs);
        }

        /// <summary>
        /// Update ReceiptDetail into database
        /// </summary>
        /// <param name="ReceiptDetail">ReceiptDetail object</param>
        public void UpdatePaymentDetail(PaymentDetail PaymentDetail)
        {
            Context.Entry(PaymentDetail).State = EntityState.Modified;
            Context.SaveChanges();
        }
    }
}
