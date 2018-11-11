using Erp.Domain.Staff.Entities;
using Erp.Domain.Staff.Interfaces;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace Erp.Domain.Staff.Repositories
{
    public class ProcessPayRepository : GenericRepository<ErpStaffDbContext, ProcessPay>, IProcessPayRepository
    {
        public ProcessPayRepository(ErpStaffDbContext context)
            : base(context)
        {

        }

        /// <summary>
        /// Get all ProcessPay
        /// </summary>
        /// <returns>ProcessPay list</returns>
        public IQueryable<ProcessPay> GetAllProcessPay()
        {
            return Context.ProcessPay.Where(item => (item.IsDeleted == null || item.IsDeleted == false));
        }

        /// <summary>
        /// Get ProcessPay information by specific id
        /// </summary>
        /// <param name="ProcessPayId">Id of ProcessPay</param>
        /// <returns></returns>
        public ProcessPay GetProcessPayById(int Id)
        {
            return Context.ProcessPay.SingleOrDefault(item => item.Id == Id && (item.IsDeleted == null || item.IsDeleted == false));
        }

        /// <summary>
        /// Insert ProcessPay into database
        /// </summary>
        /// <param name="ProcessPay">Object infomation</param>
        public void InsertProcessPay(ProcessPay ProcessPay)
        {
            Context.ProcessPay.Add(ProcessPay);
            Context.Entry(ProcessPay).State = EntityState.Added;
            Context.SaveChanges();
        }

        /// <summary>
        /// Delete ProcessPay with specific id
        /// </summary>
        /// <param name="Id">ProcessPay Id</param>
        public void DeleteProcessPay(int Id)
        {
            ProcessPay deletedProcessPay = GetProcessPayById(Id);
            Context.ProcessPay.Remove(deletedProcessPay);
            Context.Entry(deletedProcessPay).State = EntityState.Deleted;
            Context.SaveChanges();
        }
        
        /// <summary>
        /// Delete a ProcessPay with its Id and Update IsDeleted IF that ProcessPay has relationship with others
        /// </summary>
        /// <param name="ProcessPayId">Id of ProcessPay</param>
        public void DeleteProcessPayRs(int Id)
        {
            ProcessPay deleteProcessPayRs = GetProcessPayById(Id);
            deleteProcessPayRs.IsDeleted = true;
            UpdateProcessPay(deleteProcessPayRs);
        }

        /// <summary>
        /// Update ProcessPay into database
        /// </summary>
        /// <param name="ProcessPay">ProcessPay object</param>
        public void UpdateProcessPay(ProcessPay ProcessPay)
        {
            Context.Entry(ProcessPay).State = EntityState.Modified;
            Context.SaveChanges();
        }
    }
}
