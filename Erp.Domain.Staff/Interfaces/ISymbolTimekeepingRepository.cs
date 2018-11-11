using Erp.Domain.Staff.Entities;
using System.Collections.Generic;
using System.Linq;

namespace Erp.Domain.Staff.Interfaces
{
    public interface ISymbolTimekeepingRepository
    {
        /// <summary>
        /// Get all TypeDayOff
        /// </summary>
        /// <returns>TypeDayOff list</returns>
        IQueryable<SymbolTimekeeping> GetAllSymbolTimekeeping();

        /// <summary>
        /// Get TypeDayOff information by specific id
        /// </summary>
        /// <param name="Id">Id of TypeDayOff</param>
        /// <returns></returns>
        SymbolTimekeeping GetSymbolTimekeepingById(int Id);
        SymbolTimekeeping GetSymbolTimekeepingByCodeDefault(string Code);
        /// <summary>
        /// Insert TypeDayOff into database
        /// </summary>
        /// <param name="TypeDayOff">Object infomation</param>
        void InsertSymbolTimekeeping(SymbolTimekeeping SymbolTimekeeping);

        /// <summary>
        /// Delete TypeDayOff with specific id
        /// </summary>
        /// <param name="Id">TypeDayOff Id</param>
        void DeleteSymbolTimekeeping(int Id);

        /// <summary>
        /// Delete a TypeDayOff with its Id and Update IsDeleted IF that TypeDayOff has relationship with others
        /// </summary>
        /// <param name="Id">Id of TypeDayOff</param>
        void DeleteSymbolTimekeepingRs(int Id);

        /// <summary>
        /// Update TypeDayOff into database
        /// </summary>
        /// <param name="TypeDayOff">TypeDayOff object</param>
        void UpdateSymbolTimekeeping(SymbolTimekeeping SymbolTimekeeping);
    }
}
