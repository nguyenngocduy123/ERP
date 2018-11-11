using Erp.Domain.Staff.Entities;
using System.Collections.Generic;
using System.Linq;

namespace Erp.Domain.Staff.Interfaces
{
    public interface ISalaryTableRepository
    {
        /// <summary>
        /// Get all SalaryTable
        /// </summary>
        /// <returns>SalaryTable list</returns>
        IQueryable<SalaryTable> GetAllSalaryTable();

        /// <summary>
        /// Get SalaryTable information by specific id
        /// </summary>
        /// <param name="Id">Id of SalaryTable</param>
        /// <returns></returns>
        SalaryTable GetSalaryTableById(int Id);

        /// <summary>
        /// Insert SalaryTable into database
        /// </summary>
        /// <param name="SalaryTable">Object infomation</param>
        void InsertSalaryTable(SalaryTable SalaryTable);

        /// <summary>
        /// Delete SalaryTable with specific id
        /// </summary>
        /// <param name="Id">SalaryTable Id</param>
        void DeleteSalaryTable(int Id);

        /// <summary>
        /// Delete a SalaryTable with its Id and Update IsDeleted IF that SalaryTable has relationship with others
        /// </summary>
        /// <param name="Id">Id of SalaryTable</param>
        void DeleteSalaryTableRs(int Id);

        /// <summary>
        /// Update SalaryTable into database
        /// </summary>
        /// <param name="SalaryTable">SalaryTable object</param>
        void UpdateSalaryTable(SalaryTable SalaryTable);
    }
}
