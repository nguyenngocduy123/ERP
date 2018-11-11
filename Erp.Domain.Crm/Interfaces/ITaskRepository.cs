using Erp.Domain.Crm.Entities;
using System.Collections.Generic;
using System.Linq;

namespace Erp.Domain.Crm.Interfaces
{
    public interface ITaskRepository
    {
        /// <summary>
        /// Get all Task
        /// </summary>
        /// <returns>Task list</returns>
        IQueryable<Task> GetAllTask();
        IQueryable<vwTask> GetAllvwTaskFull();
        IQueryable<vwTask> GetAllvwTask();
        IQueryable<Task> GetAllTaskFull();
        /// <summary>
        /// Get Task information by specific id
        /// </summary>
        /// <param name="Id">Id of Task</param>
        /// <returns></returns>
        Task GetTaskById(int Id);
        Task GetTaskFullById(int Id);
        vwTask GetvwTaskById(int Id);
        /// <summary>
        /// Insert Task into database
        /// </summary>
        /// <param name="Task">Object infomation</param>
        void InsertTask(Task Task);

        /// <summary>
        /// Delete Task with specific id
        /// </summary>
        /// <param name="Id">Task Id</param>
        void DeleteTask(int Id);

        /// <summary>
        /// Delete a Task with its Id and Update IsDeleted IF that Task has relationship with others
        /// </summary>
        /// <param name="Id">Id of Task</param>
        void DeleteTaskRs(int Id);

        /// <summary>
        /// Update Task into database
        /// </summary>
        /// <param name="Task">Task object</param>
        void UpdateTask(Task Task);
    }
}
