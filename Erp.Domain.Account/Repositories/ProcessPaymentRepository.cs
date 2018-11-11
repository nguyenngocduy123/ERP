using Erp.Domain.Account.Entities;
using Erp.Domain.Account.Interfaces;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity;
using System.Linq;

namespace Erp.Domain.Account.Repositories
{
    public class ProcessPaymentRepository : GenericRepository<ErpAccountDbContext, ProcessPayment>, IProcessPaymentRepository
    {
        public ProcessPaymentRepository(ErpAccountDbContext context)
            : base(context)
        {

        }

        /// <summary>
        /// Get all ProcessPayment
        /// </summary>
        /// <returns>ProcessPayment list</returns>
        public IQueryable<vwProcessPayment> GetAllProcessPayment()
        {
            return Context.vwProcessPayment.Where(item => (item.IsDeleted == null || item.IsDeleted == false));
        }

        /// <summary>
        /// Get ProcessPayment information by specific id
        /// </summary>
        /// <param name="ProcessPaymentId">Id of ProcessPayment</param>
        /// <returns></returns>
        public ProcessPayment GetProcessPaymentById(int Id)
        {
            return Context.ProcessPayment.SingleOrDefault(item => item.Id == Id && (item.IsDeleted == null || item.IsDeleted == false));
        }

        /// <summary>
        /// Insert ProcessPayment into database
        /// </summary>
        /// <param name="ProcessPayment">Object infomation</param>
        public void InsertProcessPayment(ProcessPayment ProcessPayment)
        {
            Context.ProcessPayment.Add(ProcessPayment);
            Context.Entry(ProcessPayment).State = EntityState.Added;
            Context.SaveChanges();
        }

        /// <summary>
        /// Delete ProcessPayment with specific id
        /// </summary>
        /// <param name="Id">ProcessPayment Id</param>
        public void DeleteProcessPayment(int Id)
        {
            ProcessPayment deletedProcessPayment = GetProcessPaymentById(Id);
            Context.ProcessPayment.Remove(deletedProcessPayment);
            Context.Entry(deletedProcessPayment).State = EntityState.Deleted;
            Context.SaveChanges();
        }
        
        /// <summary>
        /// Delete a ProcessPayment with its Id and Update IsDeleted IF that ProcessPayment has relationship with others
        /// </summary>
        /// <param name="ProcessPaymentId">Id of ProcessPayment</param>
        public void DeleteProcessPaymentRs(int Id)
        {
            ProcessPayment deleteProcessPaymentRs = GetProcessPaymentById(Id);
            deleteProcessPaymentRs.IsDeleted = true;
            UpdateProcessPayment(deleteProcessPaymentRs);
        }

        /// <summary>
        /// Update ProcessPayment into database
        /// </summary>
        /// <param name="ProcessPayment">ProcessPayment object</param>
        public void UpdateProcessPayment(ProcessPayment ProcessPayment)
        {
            Context.Entry(ProcessPayment).State = EntityState.Modified;
            Context.SaveChanges();
        }
    }
}
