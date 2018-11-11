using Erp.Domain.Staff.Entities;
using Erp.Domain.Staff.Interfaces;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace Erp.Domain.Staff.Repositories
{
    public class HolidaysRepository : GenericRepository<ErpStaffDbContext, Holidays>, IHolidaysRepository
    {
        public HolidaysRepository(ErpStaffDbContext context)
            : base(context)
        {

        }

        /// <summary>
        /// Get all Holidays
        /// </summary>
        /// <returns>Holidays list</returns>
        public IQueryable<Holidays> GetAllHolidays()
        {
            return Context.Holidays.Where(item => (item.IsDeleted == null || item.IsDeleted == false));
        }

        /// <summary>
        /// Get Holidays information by specific id
        /// </summary>
        /// <param name="HolidaysId">Id of Holidays</param>
        /// <returns></returns>
        public Holidays GetHolidaysById(int Id)
        {
            return Context.Holidays.SingleOrDefault(item => item.Id == Id && (item.IsDeleted == null || item.IsDeleted == false));
        }

        /// <summary>
        /// Insert Holidays into database
        /// </summary>
        /// <param name="Holidays">Object infomation</param>
        public void InsertHolidays(Holidays Holidays)
        {
            Context.Holidays.Add(Holidays);
            Context.Entry(Holidays).State = EntityState.Added;
            Context.SaveChanges();
        }

        /// <summary>
        /// Delete Holidays with specific id
        /// </summary>
        /// <param name="Id">Holidays Id</param>
        public void DeleteHolidays(int Id)
        {
            Holidays deletedHolidays = GetHolidaysById(Id);
            Context.Holidays.Remove(deletedHolidays);
            Context.Entry(deletedHolidays).State = EntityState.Deleted;
            Context.SaveChanges();
        }
        
        /// <summary>
        /// Delete a Holidays with its Id and Update IsDeleted IF that Holidays has relationship with others
        /// </summary>
        /// <param name="HolidaysId">Id of Holidays</param>
        public void DeleteHolidaysRs(int Id)
        {
            Holidays deleteHolidaysRs = GetHolidaysById(Id);
            deleteHolidaysRs.IsDeleted = true;
            UpdateHolidays(deleteHolidaysRs);
        }

        /// <summary>
        /// Update Holidays into database
        /// </summary>
        /// <param name="Holidays">Holidays object</param>
        public void UpdateHolidays(Holidays Holidays)
        {
            Context.Entry(Holidays).State = EntityState.Modified;
            Context.SaveChanges();
        }
    }
}
