using Erp.Domain.Crm.Entities;
using Erp.Domain.Crm.Interfaces;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;

namespace Erp.Domain.Crm.Repositories
{
    public class ProcessActionRepository : GenericRepository<ErpCrmDbContext, ProcessAction>, IProcessActionRepository
    {
        public ProcessActionRepository(ErpCrmDbContext context)
            : base(context)
        {

        }

        /// <summary>
        /// Get all ProcessAction
        /// </summary>
        /// <returns>ProcessAction list</returns>
        public IQueryable<ProcessAction> GetAllProcessAction()
        {
            return Context.ProcessAction.Where(item => (item.IsDeleted == null || item.IsDeleted == false));
        }

        /// <summary>
        /// Get ProcessAction information by specific id
        /// </summary>
        /// <param name="ProcessActionId">Id of ProcessAction</param>
        /// <returns></returns>
        public ProcessAction GetProcessActionById(int Id)
        {
            return Context.ProcessAction.SingleOrDefault(item => item.Id == Id && (item.IsDeleted == null || item.IsDeleted == false));
        }

        /// <summary>
        /// Insert ProcessAction into database
        /// </summary>
        /// <param name="ProcessAction">Object infomation</param>
        public void InsertProcessAction(ProcessAction ProcessAction)
        {
            Context.ProcessAction.Add(ProcessAction);
            Context.Entry(ProcessAction).State = EntityState.Added;
            Context.SaveChanges();
        }

        /// <summary>
        /// Delete ProcessAction with specific id
        /// </summary>
        /// <param name="Id">ProcessAction Id</param>
        public void DeleteProcessAction(int Id)
        {
            ProcessAction deletedProcessAction = GetProcessActionById(Id);
            Context.ProcessAction.Remove(deletedProcessAction);
            Context.Entry(deletedProcessAction).State = EntityState.Deleted;
            Context.SaveChanges();
        }
        
        /// <summary>
        /// Delete a ProcessAction with its Id and Update IsDeleted IF that ProcessAction has relationship with others
        /// </summary>
        /// <param name="ProcessActionId">Id of ProcessAction</param>
        public void DeleteProcessActionRs(int Id)
        {
            ProcessAction deleteProcessActionRs = GetProcessActionById(Id);
            deleteProcessActionRs.IsDeleted = true;
            UpdateProcessAction(deleteProcessActionRs);
        }

        /// <summary>
        /// Update ProcessAction into database
        /// </summary>
        /// <param name="ProcessAction">ProcessAction object</param>
        public void UpdateProcessAction(ProcessAction ProcessAction)
        {
            Context.Entry(ProcessAction).State = EntityState.Modified;
            Context.SaveChanges();
        }
    }
}
