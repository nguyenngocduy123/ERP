using Erp.Domain.Crm.Entities;
using Erp.Domain.Crm.Interfaces;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity;
using System.Linq;

namespace Erp.Domain.Crm.Repositories
{
    public class ProcessAppliedRepository : GenericRepository<ErpCrmDbContext, ProcessApplied>, IProcessAppliedRepository
    {
        public ProcessAppliedRepository(ErpCrmDbContext context)
            : base(context)
        {

        }

        /// <summary>
        /// Get all ProcessApplied
        /// </summary>
        /// <returns>ProcessApplied list</returns>
        public IQueryable<ProcessApplied> GetAllProcessApplied()
        {
            return Context.ProcessApplied.Where(item => (item.IsDeleted == null || item.IsDeleted == false));
        }

        /// <summary>
        /// Get ProcessApplied information by specific id
        /// </summary>
        /// <param name="ProcessAppliedId">Id of ProcessApplied</param>
        /// <returns></returns>
        public ProcessApplied GetProcessAppliedById(int Id)
        {
            return Context.ProcessApplied.SingleOrDefault(item => item.Id == Id && (item.IsDeleted == null || item.IsDeleted == false));
        }

        /// <summary>
        /// Insert ProcessApplied into database
        /// </summary>
        /// <param name="ProcessApplied">Object infomation</param>
        public void InsertProcessApplied(ProcessApplied ProcessApplied)
        {
            Context.ProcessApplied.Add(ProcessApplied);
            Context.Entry(ProcessApplied).State = EntityState.Added;
            Context.SaveChanges();
        }

        /// <summary>
        /// Delete ProcessApplied with specific id
        /// </summary>
        /// <param name="Id">ProcessApplied Id</param>
        public void DeleteProcessApplied(int Id)
        {
            ProcessApplied deletedProcessApplied = GetProcessAppliedById(Id);
            Context.ProcessApplied.Remove(deletedProcessApplied);
            Context.Entry(deletedProcessApplied).State = EntityState.Deleted;
            Context.SaveChanges();
        }
        
        /// <summary>
        /// Delete a ProcessApplied with its Id and Update IsDeleted IF that ProcessApplied has relationship with others
        /// </summary>
        /// <param name="ProcessAppliedId">Id of ProcessApplied</param>
        public void DeleteProcessAppliedRs(int Id)
        {
            ProcessApplied deleteProcessAppliedRs = GetProcessAppliedById(Id);
            deleteProcessAppliedRs.IsDeleted = true;
            UpdateProcessApplied(deleteProcessAppliedRs);
        }

        /// <summary>
        /// Update ProcessApplied into database
        /// </summary>
        /// <param name="ProcessApplied">ProcessApplied object</param>
        public void UpdateProcessApplied(ProcessApplied ProcessApplied)
        {
            Context.Entry(ProcessApplied).State = EntityState.Modified;
            Context.SaveChanges();
        }
    }
}
