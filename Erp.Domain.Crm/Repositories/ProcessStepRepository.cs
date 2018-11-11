using Erp.Domain.Crm.Entities;
using Erp.Domain.Crm.Interfaces;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;

namespace Erp.Domain.Crm.Repositories
{
    public class ProcessStepRepository : GenericRepository<ErpCrmDbContext, ProcessStep>, IProcessStepRepository
    {
        public ProcessStepRepository(ErpCrmDbContext context)
            : base(context)
        {

        }

        /// <summary>
        /// Get all ProcessStep
        /// </summary>
        /// <returns>ProcessStep list</returns>
        public IQueryable<ProcessStep> GetAllProcessStep()
        {
            return Context.ProcessStep.Where(item => (item.IsDeleted == null || item.IsDeleted == false));
        }

        /// <summary>
        /// Get ProcessStep information by specific id
        /// </summary>
        /// <param name="ProcessStepId">Id of ProcessStep</param>
        /// <returns></returns>
        public ProcessStep GetProcessStepById(int Id)
        {
            return Context.ProcessStep.SingleOrDefault(item => item.Id == Id && (item.IsDeleted == null || item.IsDeleted == false));
        }

        /// <summary>
        /// Insert ProcessStep into database
        /// </summary>
        /// <param name="ProcessStep">Object infomation</param>
        public void InsertProcessStep(ProcessStep ProcessStep)
        {
            Context.ProcessStep.Add(ProcessStep);
            Context.Entry(ProcessStep).State = EntityState.Added;
            Context.SaveChanges();
        }

        /// <summary>
        /// Delete ProcessStep with specific id
        /// </summary>
        /// <param name="Id">ProcessStep Id</param>
        public void DeleteProcessStep(int Id)
        {
            ProcessStep deletedProcessStep = GetProcessStepById(Id);
            Context.ProcessStep.Remove(deletedProcessStep);
            Context.Entry(deletedProcessStep).State = EntityState.Deleted;
            Context.SaveChanges();
        }
        
        /// <summary>
        /// Delete a ProcessStep with its Id and Update IsDeleted IF that ProcessStep has relationship with others
        /// </summary>
        /// <param name="ProcessStepId">Id of ProcessStep</param>
        public void DeleteProcessStepRs(int Id)
        {
            ProcessStep deleteProcessStepRs = GetProcessStepById(Id);
            deleteProcessStepRs.IsDeleted = true;
            UpdateProcessStep(deleteProcessStepRs);
        }

        /// <summary>
        /// Update ProcessStep into database
        /// </summary>
        /// <param name="ProcessStep">ProcessStep object</param>
        public void UpdateProcessStep(ProcessStep ProcessStep)
        {
            Context.Entry(ProcessStep).State = EntityState.Modified;
            Context.SaveChanges();
        }
    }
}
