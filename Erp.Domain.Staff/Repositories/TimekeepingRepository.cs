using Erp.Domain.Staff.Entities;
using Erp.Domain.Staff.Interfaces;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity;
using System.Linq;

namespace Erp.Domain.Staff.Repositories
{
    public class TimekeepingRepository : GenericRepository<ErpStaffDbContext, Timekeeping>, ITimekeepingRepository
    {
        public TimekeepingRepository(ErpStaffDbContext context)
            : base(context)
        {

        }

        /// <summary>
        /// Get all Timekeeping
        /// </summary>
        /// <returns>Timekeeping list</returns>
        public IQueryable<Timekeeping> GetAllTimekeeping()
        {
            return Context.Timekeeping.Where(item => (item.IsDeleted == null || item.IsDeleted == false));
        }
        public IQueryable<vwTimekeeping> GetvwAllTimekeeping()
        {
            return Context.vwTimekeeping;
        }
        public IQueryable<vwTotalTimekeeping> GetAllvwTimekeepingSynthesis()
        {
            return Context.vwTotalTimekeeping;
        }
       
        /// <summary>
        /// Get Timekeeping information by specific id
        /// </summary>
        /// <param name="TimekeepingId">Id of Timekeeping</param>
        /// <returns></returns>
        public Timekeeping GetTimekeepingById(int Id)
        {
            return Context.Timekeeping.SingleOrDefault(item => item.Id == Id && (item.IsDeleted == null || item.IsDeleted == false));
        }
        public vwTimekeeping GetvwTimekeepingById(int TimekeepingId)
        {
            return Context.vwTimekeeping.SingleOrDefault(item => item.TimekeepingId == TimekeepingId);
        }
        public vwTimekeeping GetvwTimekeepingByWorkSchedulesId(int WorkSchedulesId)
        {
            return Context.vwTimekeeping.SingleOrDefault(item => item.WorkSchedulesId == WorkSchedulesId);
        }
        /// <summary>
        /// Insert Timekeeping into database
        /// </summary>
        /// <param name="Timekeeping">Object infomation</param>
        public void InsertTimekeeping(Timekeeping Timekeeping)
        {
            Context.Timekeeping.Add(Timekeeping);
            Context.Entry(Timekeeping).State = EntityState.Added;
            Context.SaveChanges();
        }

        /// <summary>
        /// Delete Timekeeping with specific id
        /// </summary>
        /// <param name="Id">Timekeeping Id</param>
        public void DeleteTimekeeping(int Id)
        {
            Timekeeping deletedTimekeeping = GetTimekeepingById(Id);
            Context.Timekeeping.Remove(deletedTimekeeping);
            Context.Entry(deletedTimekeeping).State = EntityState.Deleted;
            Context.SaveChanges();
        }
        
        /// <summary>
        /// Delete a Timekeeping with its Id and Update IsDeleted IF that Timekeeping has relationship with others
        /// </summary>
        /// <param name="TimekeepingId">Id of Timekeeping</param>
        public void DeleteTimekeepingRs(int Id)
        {
            Timekeeping deleteTimekeepingRs = GetTimekeepingById(Id);
            deleteTimekeepingRs.IsDeleted = true;
            UpdateTimekeeping(deleteTimekeepingRs);
        }

        /// <summary>
        /// Update Timekeeping into database
        /// </summary>
        /// <param name="Timekeeping">Timekeeping object</param>
        public void UpdateTimekeeping(Timekeeping Timekeeping)
        {
            Context.Entry(Timekeeping).State = EntityState.Modified;
            Context.SaveChanges();
        }
    }
}
