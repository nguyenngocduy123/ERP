using Erp.Domain.Staff.Entities;
using Erp.Domain.Staff.Interfaces;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace Erp.Domain.Staff.Repositories
{
    public class WorkingProcessRepository : GenericRepository<ErpStaffDbContext, WorkingProcess>, IWorkingProcessRepository
    {
        public WorkingProcessRepository(ErpStaffDbContext context)
            : base(context)
        {

        }

        /// <summary>
        /// Get all WorkingProcess
        /// </summary>
        /// <returns>WorkingProcess list</returns>
        public IQueryable<WorkingProcess> GetAllWorkingProcess()
        {
            return Context.WorkingProcess.Where(item => (item.IsDeleted == null || item.IsDeleted == false));
        }

        /// <summary>
        /// Get WorkingProcess information by specific id
        /// </summary>
        /// <param name="WorkingProcessId">Id of WorkingProcess</param>
        /// <returns></returns>
        public WorkingProcess GetWorkingProcessById(int Id)
        {
            return Context.WorkingProcess.SingleOrDefault(item => item.Id == Id && (item.IsDeleted == null || item.IsDeleted == false));
        }

        /// <summary>
        /// Insert WorkingProcess into database
        /// </summary>
        /// <param name="WorkingProcess">Object infomation</param>
        public void InsertWorkingProcess(WorkingProcess WorkingProcess)
        {
            Context.WorkingProcess.Add(WorkingProcess);
            Context.Entry(WorkingProcess).State = EntityState.Added;
            Context.SaveChanges();
        }

        /// <summary>
        /// Delete WorkingProcess with specific id
        /// </summary>
        /// <param name="Id">WorkingProcess Id</param>
        public void DeleteWorkingProcess(int Id)
        {
            WorkingProcess deletedWorkingProcess = GetWorkingProcessById(Id);
            Context.WorkingProcess.Remove(deletedWorkingProcess);
            Context.Entry(deletedWorkingProcess).State = EntityState.Deleted;
            Context.SaveChanges();
        }
        
        /// <summary>
        /// Delete a WorkingProcess with its Id and Update IsDeleted IF that WorkingProcess has relationship with others
        /// </summary>
        /// <param name="WorkingProcessId">Id of WorkingProcess</param>
        public void DeleteWorkingProcessRs(int Id)
        {
            WorkingProcess deleteWorkingProcessRs = GetWorkingProcessById(Id);
            deleteWorkingProcessRs.IsDeleted = true;
            UpdateWorkingProcess(deleteWorkingProcessRs);
        }

        /// <summary>
        /// Update WorkingProcess into database
        /// </summary>
        /// <param name="WorkingProcess">WorkingProcess object</param>
        public void UpdateWorkingProcess(WorkingProcess WorkingProcess)
        {
            Context.Entry(WorkingProcess).State = EntityState.Modified;
            Context.SaveChanges();
        }
    }
}
