using Erp.Domain.Crm.Entities;
using Erp.Domain.Crm.Interfaces;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;

namespace Erp.Domain.Crm.Repositories
{
    public class ProcessStageRepository : GenericRepository<ErpCrmDbContext, ProcessStage>, IProcessStageRepository
    {
        public ProcessStageRepository(ErpCrmDbContext context)
            : base(context)
        {

        }

        /// <summary>
        /// Get all ProcessStage
        /// </summary>
        /// <returns>ProcessStage list</returns>
        public IQueryable<ProcessStage> GetAllProcessStage()
        {
            return Context.ProcessStage.Where(item => (item.IsDeleted == null || item.IsDeleted == false));
        }

        /// <summary>
        /// Get ProcessStage information by specific id
        /// </summary>
        /// <param name="ProcessStageId">Id of ProcessStage</param>
        /// <returns></returns>
        public ProcessStage GetProcessStageById(int Id)
        {
            return Context.ProcessStage.SingleOrDefault(item => item.Id == Id && (item.IsDeleted == null || item.IsDeleted == false));
        }

        /// <summary>
        /// Insert ProcessStage into database
        /// </summary>
        /// <param name="ProcessStage">Object infomation</param>
        public void InsertProcessStage(ProcessStage ProcessStage)
        {
            Context.ProcessStage.Add(ProcessStage);
            Context.Entry(ProcessStage).State = EntityState.Added;
            Context.SaveChanges();
        }

        /// <summary>
        /// Delete ProcessStage with specific id
        /// </summary>
        /// <param name="Id">ProcessStage Id</param>
        public void DeleteProcessStage(int Id)
        {
            ProcessStage deletedProcessStage = GetProcessStageById(Id);
            Context.ProcessStage.Remove(deletedProcessStage);
            Context.Entry(deletedProcessStage).State = EntityState.Deleted;
            Context.SaveChanges();
        }
        
        /// <summary>
        /// Delete a ProcessStage with its Id and Update IsDeleted IF that ProcessStage has relationship with others
        /// </summary>
        /// <param name="ProcessStageId">Id of ProcessStage</param>
        public void DeleteProcessStageRs(int Id)
        {
            ProcessStage deleteProcessStageRs = GetProcessStageById(Id);
            deleteProcessStageRs.IsDeleted = true;
            UpdateProcessStage(deleteProcessStageRs);
        }

        /// <summary>
        /// Update ProcessStage into database
        /// </summary>
        /// <param name="ProcessStage">ProcessStage object</param>
        public void UpdateProcessStage(ProcessStage ProcessStage)
        {
            Context.Entry(ProcessStage).State = EntityState.Modified;
            Context.SaveChanges();
        }
    }
}
