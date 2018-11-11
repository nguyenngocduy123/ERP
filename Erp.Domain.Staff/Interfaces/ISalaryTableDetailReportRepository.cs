using Erp.Domain.Staff.Entities;
using System.Collections.Generic;
using System.Linq;

namespace Erp.Domain.Staff.Interfaces
{
    public interface ISalaryTableDetailReportRepository
    {
        /// <summary>
        /// Get all SalaryTableDetailReport
        /// </summary>
        /// <returns>SalaryTableDetailReport list</returns>
        IQueryable<SalaryTableDetailReport> GetAllSalaryTableDetailReport();

        /// <summary>
        /// Get SalaryTableDetailReport information by specific id
        /// </summary>
        /// <param name="Id">Id of SalaryTableDetailReport</param>
        /// <returns></returns>
        SalaryTableDetailReport GetSalaryTableDetailReportById(int Id);

        SalaryTableDetailReport GetSalaryTableDetailReportByStaffId_Name(int salarytableId, int staffId, string name);

        /// <summary>
        /// Insert SalaryTableDetailReport into database
        /// </summary>
        /// <param name="SalaryTableDetailReport">Object infomation</param>
        void InsertSalaryTableDetailReport(SalaryTableDetailReport SalaryTableDetailReport);

        /// <summary>
        /// Delete SalaryTableDetailReport with specific id
        /// </summary>
        /// <param name="Id">SalaryTableDetailReport Id</param>
        void DeleteSalaryTableDetailReport(int Id);

        /// <summary>
        /// Delete a SalaryTableDetailReport with its Id and Update IsDeleted IF that SalaryTableDetailReport has relationship with others
        /// </summary>
        /// <param name="Id">Id of SalaryTableDetailReport</param>
        void DeleteSalaryTableDetailReportRs(int Id);

        /// <summary>
        /// Update SalaryTableDetailReport into database
        /// </summary>
        /// <param name="SalaryTableDetailReport">SalaryTableDetailReport object</param>
        void UpdateSalaryTableDetailReport(SalaryTableDetailReport SalaryTableDetailReport);

        IQueryable<SalaryTableDetailReport> GetSalaryTableDetailReportBySalaryTableId(int SalaryTableId);

        IQueryable<SalaryTableDetailReport> GetSalaryTableDetailReportByStaffId(int StaffId);

        //IQueryable<vwStaff_SalaryTableDetail_Report> GetAllViewSalaryTableDetail_Report();
        //IQueryable<vwStaff_SalaryTableDetail_Report> GetSalaryTableDetail_ReportBySalaryTableId(int salaryTableId);
        //IQueryable<vwStaff_SalaryTableDetail_Report> GetSalaryTableDetail_ReportByListSalaryTableId(List<int> lstSalaryTableId);
    }
}
