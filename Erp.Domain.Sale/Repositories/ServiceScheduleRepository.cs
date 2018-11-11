using Erp.Domain.Sale.Entities;
using Erp.Domain.Sale.Interfaces;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity;
using System.Linq;

namespace Erp.Domain.Sale.Repositories
{
    public class ServiceScheduleRepository : GenericRepository<ErpSaleDbContext, ServiceSchedule>, IServiceScheduleRepository
    {
        public ServiceScheduleRepository(ErpSaleDbContext context)
            : base(context)
        {

        }

        /// <summary>
        /// Get all ServiceSchedule
        /// </summary>
        /// <returns>ServiceSchedule list</returns>
        public IQueryable<ServiceSchedule> GetAllServiceSchedule()
        {
            return Context.ServiceSchedule.Where(item => (item.IsDeleted == null || item.IsDeleted == false));
        }
        public IQueryable<vwServiceSchedule> GetAllvwServiceSchedule()
        {
            return Context.vwServiceSchedule.Where(item => (item.IsDeleted == null || item.IsDeleted == false));
        }
        /// <summary>
        /// Get ServiceSchedule information by specific id
        /// </summary>
        /// <param name="ServiceScheduleId">Id of ServiceSchedule</param>
        /// <returns></returns>
        public ServiceSchedule GetServiceScheduleById(int Id)
        {
            return Context.ServiceSchedule.SingleOrDefault(item => item.Id == Id && (item.IsDeleted == null || item.IsDeleted == false));
        }
        public vwServiceSchedule GetvwServiceScheduleById(int Id)
        {
            return Context.vwServiceSchedule.SingleOrDefault(item => item.Id == Id && (item.IsDeleted == null || item.IsDeleted == false));
        }
        /// <summary>
        /// Insert ServiceSchedule into database
        /// </summary>
        /// <param name="ServiceSchedule">Object infomation</param>
        public void InsertServiceSchedule(ServiceSchedule ServiceSchedule)
        {
            Context.ServiceSchedule.Add(ServiceSchedule);
            Context.Entry(ServiceSchedule).State = EntityState.Added;
            Context.SaveChanges();
        }

        /// <summary>
        /// Delete ServiceSchedule with specific id
        /// </summary>
        /// <param name="Id">ServiceSchedule Id</param>
        public void DeleteServiceSchedule(int Id)
        {
            ServiceSchedule deletedServiceSchedule = GetServiceScheduleById(Id);
            Context.ServiceSchedule.Remove(deletedServiceSchedule);
            Context.Entry(deletedServiceSchedule).State = EntityState.Deleted;
            Context.SaveChanges();
        }
        
        /// <summary>
        /// Delete a ServiceSchedule with its Id and Update IsDeleted IF that ServiceSchedule has relationship with others
        /// </summary>
        /// <param name="ServiceScheduleId">Id of ServiceSchedule</param>
        public void DeleteServiceScheduleRs(int Id)
        {
            ServiceSchedule deleteServiceScheduleRs = GetServiceScheduleById(Id);
            deleteServiceScheduleRs.IsDeleted = true;
            UpdateServiceSchedule(deleteServiceScheduleRs);
        }

        /// <summary>
        /// Update ServiceSchedule into database
        /// </summary>
        /// <param name="ServiceSchedule">ServiceSchedule object</param>
        public void UpdateServiceSchedule(ServiceSchedule ServiceSchedule)
        {
            Context.Entry(ServiceSchedule).State = EntityState.Modified;
            Context.SaveChanges();
        }
    }
}
