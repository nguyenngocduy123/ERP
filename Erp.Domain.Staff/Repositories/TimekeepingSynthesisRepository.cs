using Erp.Domain.Staff.Entities;
using Erp.Domain.Staff.Interfaces;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity;
using System.Linq;

namespace Erp.Domain.Staff.Repositories
{
    public class TimekeepingSynthesisRepository : GenericRepository<ErpStaffDbContext, TimekeepingSynthesis>, ITimekeepingSynthesisRepository
    {
        public TimekeepingSynthesisRepository(ErpStaffDbContext context)
            : base(context)
        {

        }

        /// <summary>
        /// Get all TimekeepingSynthesis
        /// </summary>
        /// <returns>TimekeepingSynthesis list</returns>
        public IQueryable<TimekeepingSynthesis> GetAllTimekeepingSynthesis()
        {
            return Context.TimekeepingSynthesis.Where(item => (item.IsDeleted == null || item.IsDeleted == false));
        }
        public IQueryable<vwTimekeepingSynthesis> GetAllvwTimekeepingSynthesis()
        {
            return Context.vwTimekeepingSynthesis.Where(item => (item.IsDeleted == null || item.IsDeleted == false));
        }
        /// <summary>
        /// Get TimekeepingSynthesis information by specific id
        /// </summary>
        /// <param name="TimekeepingSynthesisId">Id of TimekeepingSynthesis</param>
        /// <returns></returns>
        public TimekeepingSynthesis GetTimekeepingSynthesisById(int Id)
        {
            return Context.TimekeepingSynthesis.SingleOrDefault(item => item.Id == Id && (item.IsDeleted == null || item.IsDeleted == false));
        }
        public TimekeepingSynthesis GetTimekeepingSynthesisByStaff(int StaffId, int Month, int Year)
        {
            return Context.TimekeepingSynthesis.SingleOrDefault(item => item.StaffId == StaffId&&item.Month==Month&&item.Year==Year && (item.IsDeleted == null || item.IsDeleted == false));
        }
        /// <summary>
        /// Insert TimekeepingSynthesis into database
        /// </summary>
        /// <param name="TimekeepingSynthesis">Object infomation</param>
        public void InsertTimekeepingSynthesis(TimekeepingSynthesis TimekeepingSynthesis)
        {
            Context.TimekeepingSynthesis.Add(TimekeepingSynthesis);
            Context.Entry(TimekeepingSynthesis).State = EntityState.Added;
            Context.SaveChanges();
        }

        /// <summary>
        /// Delete TimekeepingSynthesis with specific id
        /// </summary>
        /// <param name="Id">TimekeepingSynthesis Id</param>
        public void DeleteTimekeepingSynthesis(int Id)
        {
            TimekeepingSynthesis deletedTimekeepingSynthesis = GetTimekeepingSynthesisById(Id);
            Context.TimekeepingSynthesis.Remove(deletedTimekeepingSynthesis);
            Context.Entry(deletedTimekeepingSynthesis).State = EntityState.Deleted;
            Context.SaveChanges();
        }
        
        /// <summary>
        /// Delete a TimekeepingSynthesis with its Id and Update IsDeleted IF that TimekeepingSynthesis has relationship with others
        /// </summary>
        /// <param name="TimekeepingSynthesisId">Id of TimekeepingSynthesis</param>
        public void DeleteTimekeepingSynthesisRs(int Id)
        {
            TimekeepingSynthesis deleteTimekeepingSynthesisRs = GetTimekeepingSynthesisById(Id);
            deleteTimekeepingSynthesisRs.IsDeleted = true;
            UpdateTimekeepingSynthesis(deleteTimekeepingSynthesisRs);
        }

        /// <summary>
        /// Update TimekeepingSynthesis into database
        /// </summary>
        /// <param name="TimekeepingSynthesis">TimekeepingSynthesis object</param>
        public void UpdateTimekeepingSynthesis(TimekeepingSynthesis TimekeepingSynthesis)
        {
            Context.Entry(TimekeepingSynthesis).State = EntityState.Modified;
            Context.SaveChanges();
        }
    }
}
