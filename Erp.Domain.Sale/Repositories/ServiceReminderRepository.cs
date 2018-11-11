using Erp.Domain.Sale.Entities;
using Erp.Domain.Sale.Interfaces;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity;
using System.Linq;

namespace Erp.Domain.Sale.Repositories
{
    public class ServiceReminderRepository : GenericRepository<ErpSaleDbContext, ServiceReminder>, IServiceReminderRepository
    {
        public ServiceReminderRepository(ErpSaleDbContext context)
            : base(context)
        {

        }

        /// <summary>
        /// Get all ServiceReminder
        /// </summary>
        /// <returns>ServiceReminder list</returns>
        public IQueryable<ServiceReminder> GetAllServiceReminder()
        {
            return Context.ServiceReminder.Where(item => (item.IsDeleted == null || item.IsDeleted == false));
        }

        /// <summary>
        /// Get ServiceReminder information by specific id
        /// </summary>
        /// <param name="ServiceReminderId">Id of ServiceReminder</param>
        /// <returns></returns>
        public ServiceReminder GetServiceReminderById(int Id)
        {
            return Context.ServiceReminder.SingleOrDefault(item => item.Id == Id && (item.IsDeleted == null || item.IsDeleted == false));
        }

        /// <summary>
        /// Insert ServiceReminder into database
        /// </summary>
        /// <param name="ServiceReminder">Object infomation</param>
        public void InsertServiceReminder(ServiceReminder ServiceReminder)
        {
            Context.ServiceReminder.Add(ServiceReminder);
            Context.Entry(ServiceReminder).State = EntityState.Added;
            Context.SaveChanges();
        }

        /// <summary>
        /// Delete ServiceReminder with specific id
        /// </summary>
        /// <param name="Id">ServiceReminder Id</param>
        public void DeleteServiceReminder(int Id)
        {
            ServiceReminder deletedServiceReminder = GetServiceReminderById(Id);
            Context.ServiceReminder.Remove(deletedServiceReminder);
            Context.Entry(deletedServiceReminder).State = EntityState.Deleted;
            Context.SaveChanges();
        }
        
        /// <summary>
        /// Delete a ServiceReminder with its Id and Update IsDeleted IF that ServiceReminder has relationship with others
        /// </summary>
        /// <param name="ServiceReminderId">Id of ServiceReminder</param>
        public void DeleteServiceReminderRs(int Id)
        {
            ServiceReminder deleteServiceReminderRs = GetServiceReminderById(Id);
            deleteServiceReminderRs.IsDeleted = true;
            UpdateServiceReminder(deleteServiceReminderRs);
        }

        /// <summary>
        /// Update ServiceReminder into database
        /// </summary>
        /// <param name="ServiceReminder">ServiceReminder object</param>
        public void UpdateServiceReminder(ServiceReminder ServiceReminder)
        {
            Context.Entry(ServiceReminder).State = EntityState.Modified;
            Context.SaveChanges();
        }
    }
}
