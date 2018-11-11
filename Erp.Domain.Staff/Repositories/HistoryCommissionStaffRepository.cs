using Erp.Domain.Staff.Entities;
using Erp.Domain.Staff.Interfaces;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity;
using System.Linq;

namespace Erp.Domain.Staff.Repositories
{
    public class HistoryCommissionStaffRepository : GenericRepository<ErpStaffDbContext, HistoryCommissionStaff>, IHistoryCommissionStaffRepository
    {
        public HistoryCommissionStaffRepository(ErpStaffDbContext context)
            : base(context)
        {

        }

        /// <summary>
        /// Get all HistoryCommissionStaff
        /// </summary>
        /// <returns>HistoryCommissionStaff list</returns>
        public IQueryable<HistoryCommissionStaff> GetAllHistoryCommissionStaff()
        {
            return Context.HistoryCommissionStaff.Where(item => (item.IsDeleted == null || item.IsDeleted == false));
        }
        public IQueryable<HistoryCommissionStaff> GetAllHistoryCommissionStaffFull()
        {
            return Context.HistoryCommissionStaff;
        }
        /// <summary>
        /// Get HistoryCommissionStaff information by specific id
        /// </summary>
        /// <param name="HistoryCommissionStaffId">Id of HistoryCommissionStaff</param>
        /// <returns></returns>
        public HistoryCommissionStaff GetHistoryCommissionStaffById(int Id)
        {
            return Context.HistoryCommissionStaff.SingleOrDefault(item => item.Id == Id && (item.IsDeleted == null || item.IsDeleted == false));
        }
        public HistoryCommissionStaff GetHistoryCommissionStaffFullById(int Id)
        {
            return Context.HistoryCommissionStaff.SingleOrDefault(item => item.Id == Id);
        }
        /// <summary>
        /// Insert HistoryCommissionStaff into database
        /// </summary>
        /// <param name="HistoryCommissionStaff">Object infomation</param>
        public void InsertHistoryCommissionStaff(HistoryCommissionStaff HistoryCommissionStaff)
        {
            Context.HistoryCommissionStaff.Add(HistoryCommissionStaff);
            Context.Entry(HistoryCommissionStaff).State = EntityState.Added;
            Context.SaveChanges();
        }

        /// <summary>
        /// Delete HistoryCommissionStaff with specific id
        /// </summary>
        /// <param name="Id">HistoryCommissionStaff Id</param>
        public void DeleteHistoryCommissionStaff(int Id)
        {
            HistoryCommissionStaff deletedHistoryCommissionStaff = GetHistoryCommissionStaffById(Id);
            Context.HistoryCommissionStaff.Remove(deletedHistoryCommissionStaff);
            Context.Entry(deletedHistoryCommissionStaff).State = EntityState.Deleted;
            Context.SaveChanges();
        }
        
        /// <summary>
        /// Delete a HistoryCommissionStaff with its Id and Update IsDeleted IF that HistoryCommissionStaff has relationship with others
        /// </summary>
        /// <param name="HistoryCommissionStaffId">Id of HistoryCommissionStaff</param>
        public void DeleteHistoryCommissionStaffRs(int Id)
        {
            HistoryCommissionStaff deleteHistoryCommissionStaffRs = GetHistoryCommissionStaffById(Id);
            deleteHistoryCommissionStaffRs.IsDeleted = true;
            UpdateHistoryCommissionStaff(deleteHistoryCommissionStaffRs);
        }

        /// <summary>
        /// Update HistoryCommissionStaff into database
        /// </summary>
        /// <param name="HistoryCommissionStaff">HistoryCommissionStaff object</param>
        public void UpdateHistoryCommissionStaff(HistoryCommissionStaff HistoryCommissionStaff)
        {
            Context.Entry(HistoryCommissionStaff).State = EntityState.Modified;
            Context.SaveChanges();
        }
    }
}
