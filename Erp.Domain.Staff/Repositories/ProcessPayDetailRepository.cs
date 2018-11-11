using Erp.Domain.Staff.Entities;
using Erp.Domain.Staff.Interfaces;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace Erp.Domain.Staff.Repositories
{
    public class ProcessPayDetailRepository : GenericRepository<ErpStaffDbContext, ProcessPayDetail>, IProcessPayDetailRepository
    {
        public ProcessPayDetailRepository(ErpStaffDbContext context)
            : base(context)
        {

        }

        /// <summary>
        /// Get all ProcessPay
        /// </summary>
        /// <returns>ProcessPay list</returns>
        public IQueryable<ProcessPayDetail> GetAllProcessPayDetail()
        {
            return Context.ProcessPayDetail.Where(item => (item.IsDeleted == null || item.IsDeleted == false));
        }

        /// <summary>
        /// Get ProcessPay information by specific id
        /// </summary>
        /// <param name="ProcessPayId">Id of ProcessPay</param>
        /// <returns></returns>
        public ProcessPayDetail GetProcessPayDetailById(int Id)
        {
            return Context.ProcessPayDetail.SingleOrDefault(item => item.Id == Id && (item.IsDeleted == null || item.IsDeleted == false));
        }

        /// <summary>
        /// Insert ProcessPay into database
        /// </summary>
        /// <param name="ProcessPay">Object infomation</param>
        public void InsertProcessPayDetail(ProcessPayDetail ProcessPayDetail)
        {
            Context.ProcessPayDetail.Add(ProcessPayDetail);
            Context.Entry(ProcessPayDetail).State = EntityState.Added;
            Context.SaveChanges();
        }

        /// <summary>
        /// Delete ProcessPay with specific id
        /// </summary>
        /// <param name="Id">ProcessPay Id</param>
        public void DeleteProcessPayDetail(int Id)
        {
            ProcessPayDetail deletedProcessPayDetail = GetProcessPayDetailById(Id);
            Context.ProcessPayDetail.Remove(deletedProcessPayDetail);
            Context.Entry(deletedProcessPayDetail).State = EntityState.Deleted;
            Context.SaveChanges();
        }
        
        /// <summary>
        /// Delete a ProcessPay with its Id and Update IsDeleted IF that ProcessPay has relationship with others
        /// </summary>
        /// <param name="ProcessPayId">Id of ProcessPay</param>
        public void DeleteProcessPayDetailRs(int Id)
        {
            ProcessPayDetail deleteProcessPayDetailRs = GetProcessPayDetailById(Id);
            deleteProcessPayDetailRs.IsDeleted = true;
            UpdateProcessPayDetail(deleteProcessPayDetailRs);
        }

        /// <summary>
        /// Update ProcessPay into database
        /// </summary>
        /// <param name="ProcessPay">ProcessPay object</param>
        public void UpdateProcessPayDetail(ProcessPayDetail ProcessPayDetail)
        {
            Context.Entry(ProcessPayDetail).State = EntityState.Modified;
            Context.SaveChanges();
        }
    }
}
