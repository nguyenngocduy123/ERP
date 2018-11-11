using Erp.Domain.Account.Entities;
using Erp.Domain.Account.Interfaces;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity;
using System.Linq;

namespace Erp.Domain.Account.Repositories
{
    public class PaymentRepository : GenericRepository<ErpAccountDbContext, Payment>, IPaymentRepository
    {
        public PaymentRepository(ErpAccountDbContext context)
            : base(context)
        {

        }

        /// <summary>
        /// Get all Payment
        /// </summary>
        /// <returns>Payment list</returns>
        public IQueryable<Payment> GetAllPayment()
        {
            return Context.Payment.Where(item => (item.IsDeleted == null || item.IsDeleted == false));
        }
        public IQueryable<vwPayment> GetAllvwPayment()
        {
            return Context.vwPayment.Where(item => (item.IsDeleted == null || item.IsDeleted == false));
        }
        public IQueryable<vwPayment> GetAllvwPaymentFull()
        {
            return Context.vwPayment;
        }
        /// <summary>
        /// Get Payment information by specific id
        /// </summary>
        /// <param name="PaymentId">Id of Payment</param>
        /// <returns></returns>
        public Payment GetPaymentById(int Id)
        {
            return Context.Payment.SingleOrDefault(item => item.Id == Id && (item.IsDeleted == null || item.IsDeleted == false));
        }
        public vwPayment GetvwPaymentById(int Id)
        {
            return Context.vwPayment.SingleOrDefault(item => item.Id == Id);
        }
        public vwPayment GetvwPaymentByTagetId(int TagetId, string TagetName, string Name)
        {
            return Context.vwPayment.SingleOrDefault(item => item.TargetId == TagetId&&item.TargetName==TagetName&&item.Name==Name);
        }
        /// <summary>
        /// Insert Payment into database
        /// </summary>
        /// <param name="Payment">Object infomation</param>
        public void InsertPayment(Payment Payment)
        {
            Context.Payment.Add(Payment);
            Context.Entry(Payment).State = EntityState.Added;
            Context.SaveChanges();
        }

        /// <summary>
        /// Delete Payment with specific id
        /// </summary>
        /// <param name="Id">Payment Id</param>
        public void DeletePayment(int Id)
        {
            Payment deletedPayment = GetPaymentById(Id);
            Context.Payment.Remove(deletedPayment);
            Context.Entry(deletedPayment).State = EntityState.Deleted;
            Context.SaveChanges();
        }
        
        /// <summary>
        /// Delete a Payment with its Id and Update IsDeleted IF that Payment has relationship with others
        /// </summary>
        /// <param name="PaymentId">Id of Payment</param>
        public void DeletePaymentRs(int Id)
        {
            Payment deletePaymentRs = GetPaymentById(Id);
            deletePaymentRs.IsDeleted = true;
            UpdatePayment(deletePaymentRs);
        }

        /// <summary>
        /// Update Payment into database
        /// </summary>
        /// <param name="Payment">Payment object</param>
        public void UpdatePayment(Payment Payment)
        {
            Context.Entry(Payment).State = EntityState.Modified;
            Context.SaveChanges();
        }
    }
}
