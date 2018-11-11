using Erp.Domain.Crm.Entities;
using Erp.Domain.Crm.Interfaces;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;

namespace Erp.Domain.Crm.Repositories
{
    public class TaskRepository : GenericRepository<ErpCrmDbContext, Task>, ITaskRepository
    {
        public TaskRepository(ErpCrmDbContext context)
            : base(context)
        {

        }

        /// <summary>
        /// Get all Task
        /// </summary>
        /// <returns>Task list</returns>
        public IQueryable<Task> GetAllTask()
        {
            return Context.Task.Where(item => (item.IsDeleted == null || item.IsDeleted == false));
        }
        public IQueryable<vwTask> GetAllvwTask()
        {
            return Context.vwTask.Where(item => (item.IsDeleted == null || item.IsDeleted == false));
        }
        public IQueryable<vwTask> GetAllvwTaskFull()
        {
            return Context.vwTask;
        }
        public IQueryable<Task> GetAllTaskFull()
        {
            return Context.Task;
        }
        /// <summary>
        /// Get Task information by specific id
        /// </summary>
        /// <param name="TaskId">Id of Task</param>
        /// <returns></returns>
        public Task GetTaskById(int Id)
        {
            return Context.Task.SingleOrDefault(item => item.Id == Id && (item.IsDeleted == null || item.IsDeleted == false));
        }
        public Task GetTaskFullById(int Id)
        {
            return Context.Task.SingleOrDefault(item => item.Id == Id);
        }
        public vwTask GetvwTaskById(int Id)
        {
            return Context.vwTask.SingleOrDefault(item => item.Id == Id && (item.IsDeleted == null || item.IsDeleted == false));
        }
        /// <summary>
        /// Insert Task into database
        /// </summary>
        /// <param name="Task">Object infomation</param>
        public void InsertTask(Task Task)
        {
            Context.Task.Add(Task);
            Context.Entry(Task).State = EntityState.Added;
            Context.SaveChanges();
        }

        /// <summary>
        /// Delete Task with specific id
        /// </summary>
        /// <param name="Id">Task Id</param>
        public void DeleteTask(int Id)
        {
            Task deletedTask = GetTaskFullById(Id);
            Context.Task.Remove(deletedTask);
            Context.Entry(deletedTask).State = EntityState.Deleted;
            Context.SaveChanges();
        }
        
        /// <summary>
        /// Delete a Task with its Id and Update IsDeleted IF that Task has relationship with others
        /// </summary>
        /// <param name="TaskId">Id of Task</param>
        public void DeleteTaskRs(int Id)
        {
            Task deleteTaskRs = GetTaskById(Id);
            deleteTaskRs.IsDeleted = true;
            UpdateTask(deleteTaskRs);
        }

        /// <summary>
        /// Update Task into database
        /// </summary>
        /// <param name="Task">Task object</param>
        public void UpdateTask(Task Task)
        {
            Context.Entry(Task).State = EntityState.Modified;
            Context.SaveChanges();
        }
    }
}
