using Erp.Domain.Staff.Entities;
using Erp.Domain.Staff.Interfaces;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity;
using System.Linq;

namespace Erp.Domain.Staff.Repositories
{
    public class ShiftsRepository : GenericRepository<ErpStaffDbContext, Shifts>, IShiftsRepository
    {
        public ShiftsRepository(ErpStaffDbContext context)
            : base(context)
        {

        }

        /// <summary>
        /// Get all Shifts
        /// </summary>
        /// <returns>Shifts list</returns>
        public IQueryable<Shifts> GetAllShifts()
        {
            return Context.Shifts.Where(item => (item.IsDeleted == null || item.IsDeleted == false));
        }

        /// <summary>
        /// Get Shifts information by specific id
        /// </summary>
        /// <param name="ShiftsId">Id of Shifts</param>
        /// <returns></returns>
        public Shifts GetShiftsById(int Id)
        {
            return Context.Shifts.SingleOrDefault(item => item.Id == Id && (item.IsDeleted == null || item.IsDeleted == false));
        }

        /// <summary>
        /// Insert Shifts into database
        /// </summary>
        /// <param name="Shifts">Object infomation</param>
        public void InsertShifts(Shifts Shifts)
        {
            Context.Shifts.Add(Shifts);
            Context.Entry(Shifts).State = EntityState.Added;
            Context.SaveChanges();
        }

        /// <summary>
        /// Delete Shifts with specific id
        /// </summary>
        /// <param name="Id">Shifts Id</param>
        public void DeleteShifts(int Id)
        {
            Shifts deletedShifts = GetShiftsById(Id);
            Context.Shifts.Remove(deletedShifts);
            Context.Entry(deletedShifts).State = EntityState.Deleted;
            Context.SaveChanges();
        }
        
        /// <summary>
        /// Delete a Shifts with its Id and Update IsDeleted IF that Shifts has relationship with others
        /// </summary>
        /// <param name="ShiftsId">Id of Shifts</param>
        public void DeleteShiftsRs(int Id)
        {
            Shifts deleteShiftsRs = GetShiftsById(Id);
            deleteShiftsRs.IsDeleted = true;
            UpdateShifts(deleteShiftsRs);
        }

        /// <summary>
        /// Update Shifts into database
        /// </summary>
        /// <param name="Shifts">Shifts object</param>
        public void UpdateShifts(Shifts Shifts)
        {
            Context.Entry(Shifts).State = EntityState.Modified;
            Context.SaveChanges();
        }
    }
}
