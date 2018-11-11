using Erp.Domain.Sale.Entities;
using Erp.Domain.Sale.Interfaces;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity;
using System.Linq;

namespace Erp.Domain.Sale.Repositories
{
    public class ServiceReminderGroupRepository : GenericRepository<ErpSaleDbContext, ServiceReminderGroup>, IServiceReminderGroupRepository
    {
        public ServiceReminderGroupRepository(ErpSaleDbContext context)
            : base(context)
        {

        }

        /// <summary>
        /// Get all ServiceReminderGroup
        /// </summary>
        /// <returns>ServiceReminderGroup list</returns>
        public IQueryable<ServiceReminderGroup> GetAllServiceReminderGroup()
        {
            return Context.ServiceReminderGroup.Where(item => (item.IsDeleted == null || item.IsDeleted == false));
        }
        public IQueryable<vwServiceReminderGroup> GetAllvwServiceReminderGroup()
        {
            return Context.vwServiceReminderGroup.Where(item => (item.IsDeleted == null || item.IsDeleted == false));
        }
        /// <summary>
        /// Get ServiceReminderGroup information by specific id
        /// </summary>
        /// <param name="ServiceReminderGroupId">Id of ServiceReminderGroup</param>
        /// <returns></returns>
        public ServiceReminderGroup GetServiceReminderGroupById(int Id)
        {
            return Context.ServiceReminderGroup.SingleOrDefault(item => item.Id == Id && (item.IsDeleted == null || item.IsDeleted == false));
        }
        public vwServiceReminderGroup GetvwServiceReminderGroupById(int Id)
        {
            return Context.vwServiceReminderGroup.SingleOrDefault(item => item.Id == Id && (item.IsDeleted == null || item.IsDeleted == false));
        }
        /// <summary>
        /// Insert ServiceReminderGroup into database
        /// </summary>
        /// <param name="ServiceReminderGroup">Object infomation</param>
        public void InsertServiceReminderGroup(ServiceReminderGroup ServiceReminderGroup)
        {
            Context.ServiceReminderGroup.Add(ServiceReminderGroup);
            Context.Entry(ServiceReminderGroup).State = EntityState.Added;
            Context.SaveChanges();
        }

        /// <summary>
        /// Delete ServiceReminderGroup with specific id
        /// </summary>
        /// <param name="Id">ServiceReminderGroup Id</param>
        public void DeleteServiceReminderGroup(int Id)
        {
            ServiceReminderGroup deletedServiceReminderGroup = GetServiceReminderGroupById(Id);
            Context.ServiceReminderGroup.Remove(deletedServiceReminderGroup);
            Context.Entry(deletedServiceReminderGroup).State = EntityState.Deleted;
            Context.SaveChanges();
        }
        
        /// <summary>
        /// Delete a ServiceReminderGroup with its Id and Update IsDeleted IF that ServiceReminderGroup has relationship with others
        /// </summary>
        /// <param name="ServiceReminderGroupId">Id of ServiceReminderGroup</param>
        public void DeleteServiceReminderGroupRs(int Id)
        {
            ServiceReminderGroup deleteServiceReminderGroupRs = GetServiceReminderGroupById(Id);
            deleteServiceReminderGroupRs.IsDeleted = true;
            UpdateServiceReminderGroup(deleteServiceReminderGroupRs);
        }

        /// <summary>
        /// Update ServiceReminderGroup into database
        /// </summary>
        /// <param name="ServiceReminderGroup">ServiceReminderGroup object</param>
        public void UpdateServiceReminderGroup(ServiceReminderGroup ServiceReminderGroup)
        {
            Context.Entry(ServiceReminderGroup).State = EntityState.Modified;
            Context.SaveChanges();
        }

        public void DeleteServiceReminderGroupList(IEnumerable<ServiceReminderGroup> list)
        {
            for (int i = 0; i < list.Count(); i++)
            {
                ServiceReminderGroup deletedServiceReminder = GetServiceReminderGroupById(list.ElementAt(i).Id);
                Context.ServiceReminderGroup.Remove(deletedServiceReminder);
                Context.Entry(deletedServiceReminder).State = EntityState.Deleted;
            }
            Context.SaveChanges();
        }
    }
}
