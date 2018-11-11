using Erp.Domain.Staff.Entities;
using Erp.Domain.Staff.Interfaces;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity;
using System.Linq;

namespace Erp.Domain.Staff.Repositories
{
    public class SymbolTimekeepingRepository : GenericRepository<ErpStaffDbContext, SymbolTimekeeping>, ISymbolTimekeepingRepository
    {
        public SymbolTimekeepingRepository(ErpStaffDbContext context)
            : base(context)
        {

        }

        /// <summary>
        /// Get all TypeDayOff
        /// </summary>
        /// <returns>TypeDayOff list</returns>
        public IQueryable<SymbolTimekeeping> GetAllSymbolTimekeeping()
        {
            return Context.SymbolTimekeeping.Where(item => (item.IsDeleted == null || item.IsDeleted == false));
        }

        /// <summary>
        /// Get TypeDayOff information by specific id
        /// </summary>
        /// <param name="TypeDayOffId">Id of TypeDayOff</param>
        /// <returns></returns>
        public SymbolTimekeeping GetSymbolTimekeepingById(int Id)
        {
            return Context.SymbolTimekeeping.SingleOrDefault(item => item.Id == Id && (item.IsDeleted == null || item.IsDeleted == false));
        }
        public SymbolTimekeeping GetSymbolTimekeepingByCodeDefault(string Code)
        {
            return Context.SymbolTimekeeping.SingleOrDefault(item => item.CodeDefault == Code && (item.IsDeleted == null || item.IsDeleted == false));
        }
        /// <summary>
        /// Insert TypeDayOff into database
        /// </summary>
        /// <param name="TypeDayOff">Object infomation</param>
        public void InsertSymbolTimekeeping(SymbolTimekeeping SymbolTimekeeping)
        {
            Context.SymbolTimekeeping.Add(SymbolTimekeeping);
            Context.Entry(SymbolTimekeeping).State = EntityState.Added;
            Context.SaveChanges();
        }

        /// <summary>
        /// Delete TypeDayOff with specific id
        /// </summary>
        /// <param name="Id">TypeDayOff Id</param>
        public void DeleteSymbolTimekeeping(int Id)
        {
            SymbolTimekeeping deletedSymbolTimekeeping = GetSymbolTimekeepingById(Id);
            Context.SymbolTimekeeping.Remove(deletedSymbolTimekeeping);
            Context.Entry(deletedSymbolTimekeeping).State = EntityState.Deleted;
            Context.SaveChanges();
        }
        
        /// <summary>
        /// Delete a TypeDayOff with its Id and Update IsDeleted IF that TypeDayOff has relationship with others
        /// </summary>
        /// <param name="TypeDayOffId">Id of TypeDayOff</param>
        public void DeleteSymbolTimekeepingRs(int Id)
        {
            SymbolTimekeeping deleteSymbolTimekeepingRs = GetSymbolTimekeepingById(Id);
            deleteSymbolTimekeepingRs.IsDeleted = true;
            UpdateSymbolTimekeeping(deleteSymbolTimekeepingRs);
        }

        /// <summary>
        /// Update TypeDayOff into database
        /// </summary>
        /// <param name="TypeDayOff">TypeDayOff object</param>
        public void UpdateSymbolTimekeeping(SymbolTimekeeping SymbolTimekeeping)
        {
            Context.Entry(SymbolTimekeeping).State = EntityState.Modified;
            Context.SaveChanges();
        }
    }
}
