using Erp.Domain.Staff.Entities;
using Erp.Domain.Staff.Interfaces;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity;
using System.Linq;

namespace Erp.Domain.Staff.Repositories
{
    public class TimekeepingListRepository : GenericRepository<ErpStaffDbContext, TimekeepingList>, ITimekeepingListRepository
    {
        public TimekeepingListRepository(ErpStaffDbContext context)
            : base(context)
        {

        }

        /// <summary>
        /// Get all TimekeepingList
        /// </summary>
        /// <returns>TimekeepingList list</returns>
        public IQueryable<TimekeepingList> GetAllTimekeepingList()
        {
            return Context.TimekeepingList.Where(item => (item.IsDeleted == null || item.IsDeleted == false));
        }
        public IQueryable<vwTimekeepingList> GetAllvwTimekeepingList()
        {
            return Context.vwTimekeepingList.Where(item => (item.IsDeleted == null || item.IsDeleted == false));
        }
        /// <summary>
        /// Get TimekeepingList information by specific id
        /// </summary>
        /// <param name="TimekeepingListId">Id of TimekeepingList</param>
        /// <returns></returns>
        public TimekeepingList GetTimekeepingListById(int Id)
        {
            return Context.TimekeepingList.SingleOrDefault(item => item.Id == Id && (item.IsDeleted == null || item.IsDeleted == false));
        }
        public vwTimekeepingList GetvwTimekeepingListById(int Id)
        {
            return Context.vwTimekeepingList.SingleOrDefault(item => item.Id == Id && (item.IsDeleted == null || item.IsDeleted == false));
        }
        /// <summary>
        /// Insert TimekeepingList into database
        /// </summary>
        /// <param name="TimekeepingList">Object infomation</param>
        public void InsertTimekeepingList(TimekeepingList TimekeepingList)
        {
            Context.TimekeepingList.Add(TimekeepingList);
            Context.Entry(TimekeepingList).State = EntityState.Added;
            Context.SaveChanges();
        }

        /// <summary>
        /// Delete TimekeepingList with specific id
        /// </summary>
        /// <param name="Id">TimekeepingList Id</param>
        public void DeleteTimekeepingList(int Id)
        {
            TimekeepingList deletedTimekeepingList = GetTimekeepingListById(Id);
            Context.TimekeepingList.Remove(deletedTimekeepingList);
            Context.Entry(deletedTimekeepingList).State = EntityState.Deleted;
            Context.SaveChanges();
        }
        
        /// <summary>
        /// Delete a TimekeepingList with its Id and Update IsDeleted IF that TimekeepingList has relationship with others
        /// </summary>
        /// <param name="TimekeepingListId">Id of TimekeepingList</param>
        public void DeleteTimekeepingListRs(int Id)
        {
            TimekeepingList deleteTimekeepingListRs = GetTimekeepingListById(Id);
            deleteTimekeepingListRs.IsDeleted = true;
            UpdateTimekeepingList(deleteTimekeepingListRs);
        }

        /// <summary>
        /// Update TimekeepingList into database
        /// </summary>
        /// <param name="TimekeepingList">TimekeepingList object</param>
        public void UpdateTimekeepingList(TimekeepingList TimekeepingList)
        {
            Context.Entry(TimekeepingList).State = EntityState.Modified;
            Context.SaveChanges();
        }
    }
}
