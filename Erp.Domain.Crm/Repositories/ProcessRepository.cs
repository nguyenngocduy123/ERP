using Erp.Domain.Crm.Entities;
using Erp.Domain.Crm.Interfaces;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;

namespace Erp.Domain.Crm.Repositories
{
    public class ProcessRepository : GenericRepository<ErpCrmDbContext, Process>, IProcessRepository
    {
        public ProcessRepository(ErpCrmDbContext context)
            : base(context)
        {

        }

        /// <summary>
        /// Get all Process
        /// </summary>
        /// <returns>Process list</returns>
        public IQueryable<Process> GetAllProcess()
        {
            return Context.Process.Where(item => (item.IsDeleted == null || item.IsDeleted == false));
        }

        /// <summary>
        /// Get Process information by specific id
        /// </summary>
        /// <param name="ProcessId">Id of Process</param>
        /// <returns></returns>
        public Process GetProcessById(int Id)
        {
            return Context.Process.SingleOrDefault(item => item.Id == Id && (item.IsDeleted == null || item.IsDeleted == false));
        }

        /// <summary>
        /// Insert Process into database
        /// </summary>
        /// <param name="Process">Object infomation</param>
        public void InsertProcess(Process Process)
        {
            Context.Process.Add(Process);
            Context.Entry(Process).State = EntityState.Added;
            Context.SaveChanges();
        }

        /// <summary>
        /// Delete Process with specific id
        /// </summary>
        /// <param name="Id">Process Id</param>
        public void DeleteProcess(int Id)
        {
            Process deletedProcess = GetProcessById(Id);
            Context.Process.Remove(deletedProcess);
            Context.Entry(deletedProcess).State = EntityState.Deleted;
            Context.SaveChanges();
        }
        
        /// <summary>
        /// Delete a Process with its Id and Update IsDeleted IF that Process has relationship with others
        /// </summary>
        /// <param name="ProcessId">Id of Process</param>
        public void DeleteProcessRs(int Id)
        {
            Process deleteProcessRs = GetProcessById(Id);
            deleteProcessRs.IsDeleted = true;
            UpdateProcess(deleteProcessRs);
        }

        /// <summary>
        /// Update Process into database
        /// </summary>
        /// <param name="Process">Process object</param>
        public void UpdateProcess(Process Process)
        {
            Context.Entry(Process).State = EntityState.Modified;
            Context.SaveChanges();
        }
    }
}
