using Erp.Domain.Staff.Entities;
using System.Collections.Generic;
using System.Linq;

namespace Erp.Domain.Staff.Interfaces
{
    public interface ISalaryAdvanceRepository
    {
        /// <summary>
        /// Get all SalaryAdvance
        /// </summary>
        /// <returns>SalaryAdvance list</returns>
        IQueryable<SalaryAdvance> GetAllSalaryAdvance();
        IQueryable<vwSalaryAdvance> GetAllvwSalaryAdvance();
        /// <summary>
        /// Get SalaryAdvance information by specific id
        /// </summary>
        /// <param name="Id">Id of SalaryAdvance</param>
        /// <returns></returns>
        SalaryAdvance GetSalaryAdvanceById(int Id);
        vwSalaryAdvance GetvwSalaryAdvanceById(int Id);
        /// <summary>
        /// Insert SalaryAdvance into database
        /// </summary>
        /// <param name="SalaryAdvance">Object infomation</param>
        void InsertSalaryAdvance(SalaryAdvance SalaryAdvance);

        /// <summary>
        /// Delete SalaryAdvance with specific id
        /// </summary>
        /// <param name="Id">SalaryAdvance Id</param>
        void DeleteSalaryAdvance(int Id);

        /// <summary>
        /// Delete a SalaryAdvance with its Id and Update IsDeleted IF that SalaryAdvance has relationship with others
        /// </summary>
        /// <param name="Id">Id of SalaryAdvance</param>
        void DeleteSalaryAdvanceRs(int Id);

        /// <summary>
        /// Update SalaryAdvance into database
        /// </summary>
        /// <param name="SalaryAdvance">SalaryAdvance object</param>
        void UpdateSalaryAdvance(SalaryAdvance SalaryAdvance);
    }
}
