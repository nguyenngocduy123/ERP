using Erp.Domain.Staff.Entities;
using System.Collections.Generic;
using System.Linq;

namespace Erp.Domain.Staff.Interfaces
{
    public interface ISalaryTableDetailRepository
    {
        /// <summary>
        /// Get all SalaryTableDetail
        /// </summary>
        /// <returns>SalaryTableDetail list</returns>
        IQueryable<SalaryTableDetail> GetAllSalaryTableDetail();

        /// <summary>
        /// Get SalaryTableDetail information by specific id
        /// </summary>
        /// <param name="Id">Id of SalaryTableDetail</param>
        /// <returns></returns>
        //SalaryTableDetail GetSalaryTableDetailById(int Id);

        /// <summary>
        /// Insert SalaryTableDetail into database
        /// </summary>
        /// <param name="SalaryTableDetail">Object infomation</param>
        void InsertSalaryTableDetail(SalaryTableDetail SalaryTableDetail);

        /// <summary>
        /// Delete SalaryTableDetail with specific id
        /// </summary>
        /// <param name="Id">SalaryTableDetail Id</param>
        void DeleteSalaryTableDetail(int Id);

        /// <summary>
        /// Update SalaryTableDetail into database
        /// </summary>
        /// <param name="SalaryTableDetail">SalaryTableDetail object</param>
        void UpdateSalaryTableDetail(SalaryTableDetail SalaryTableDetail);
    }
}
