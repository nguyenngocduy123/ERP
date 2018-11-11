using Erp.Domain.Staff.Entities;
using Erp.Domain.Staff.Interfaces;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity;
using System.Linq;

namespace Erp.Domain.Staff.Repositories
{
    public class SalaryTableDetailReportRepository : GenericRepository<ErpStaffDbContext, SalaryTableDetailReport>, ISalaryTableDetailReportRepository
    {
        public SalaryTableDetailReportRepository(ErpStaffDbContext context)
            : base(context)
        {

        }

        //public IQueryable<vwStaff_SalaryTableDetail_Report> GetAllViewSalaryTableDetail_Report()
        //{
        //    return Context.vwStaff_SalaryTableDetail_Report.Where(item => (item.IsDeleted == null || item.IsDeleted == false));
        //}

        //public IQueryable<vwStaff_SalaryTableDetail_Report> GetSalaryTableDetail_ReportBySalaryTableId(int salaryTableId)
        //{
        //    return Context.vwStaff_SalaryTableDetail_Report.Where(item => (item.IsDeleted == null || item.IsDeleted == false) && item.SalaryTableId == salaryTableId);
        //}

        //public IQueryable<vwStaff_SalaryTableDetail_Report> GetSalaryTableDetail_ReportByListSalaryTableId(List<int> lstSalaryTableId)
        //{
        //    return Context.vwStaff_SalaryTableDetail_Report.Where(item => (item.IsDeleted == null || item.IsDeleted == false) && lstSalaryTableId.Contains(item.SalaryTableId.Value));
        //}


        /// <summary>
        /// Get all SalaryTableDetailReport
        /// </summary>
        /// <returns>SalaryTableDetailReport list</returns>
        public IQueryable<SalaryTableDetailReport> GetAllSalaryTableDetailReport()
        {
            return Context.SalaryTableDetailReport.Where(item => (item.IsDeleted == null || item.IsDeleted == false));
        }

        /// <summary>
        /// Get SalaryTableDetailReport information by specific id
        /// </summary>
        /// <param name="SalaryTableDetailReportId">Id of SalaryTableDetailReport</param>
        /// <returns></returns>
        public SalaryTableDetailReport GetSalaryTableDetailReportById(int Id)
        {
            return Context.SalaryTableDetailReport.SingleOrDefault(item => item.Id == Id && (item.IsDeleted == null || item.IsDeleted == false));
        }

        public SalaryTableDetailReport GetSalaryTableDetailReportByStaffId_Name(int salarytableId,int staffId, string name) 
        {
            return Context.SalaryTableDetailReport.SingleOrDefault(item => item.SalaryTableId.Value == salarytableId  && item.StaffId.Value == staffId && item.ColumName.Equals(name) &&  (item.IsDeleted == null || item.IsDeleted == false));
        }

        public IQueryable<SalaryTableDetailReport> GetSalaryTableDetailReportBySalaryTableId(int SalaryTableId)
        {
            return Context.SalaryTableDetailReport.Where(item => (item.IsDeleted == null || item.IsDeleted == false) && item.SalaryTableId == SalaryTableId);
        }

        public IQueryable<SalaryTableDetailReport> GetSalaryTableDetailReportByStaffId(int StaffId)
        {
            return Context.SalaryTableDetailReport.Where(item => (item.IsDeleted == null || item.IsDeleted == false) && item.StaffId == StaffId);
        }

      
        /// <summary>
        /// Insert SalaryTableDetailReport into database
        /// </summary>
        /// <param name="SalaryTableDetailReport">Object infomation</param>
        public void InsertSalaryTableDetailReport(SalaryTableDetailReport SalaryTableDetailReport)
        {
            Context.SalaryTableDetailReport.Add(SalaryTableDetailReport);
            Context.Entry(SalaryTableDetailReport).State = EntityState.Added;
            Context.SaveChanges();
        }

        /// <summary>
        /// Delete SalaryTableDetailReport with specific id
        /// </summary>
        /// <param name="Id">SalaryTableDetailReport Id</param>
        public void DeleteSalaryTableDetailReport(int Id)
        {
            Helper.SqlHelper.ExecuteSQL("delete from Staff_SalaryTableDetail_Report where SalaryTableId = @SalaryTableId", new { SalaryTableId = Id });
        }
        
        /// <summary>
        /// Delete a SalaryTableDetailReport with its Id and Update IsDeleted IF that SalaryTableDetailReport has relationship with others
        /// </summary>
        /// <param name="SalaryTableDetailReportId">Id of SalaryTableDetailReport</param>
        public void DeleteSalaryTableDetailReportRs(int Id)
        {
            SalaryTableDetailReport deleteSalaryTableDetailReportRs = GetSalaryTableDetailReportById(Id);
            deleteSalaryTableDetailReportRs.IsDeleted = true;
            UpdateSalaryTableDetailReport(deleteSalaryTableDetailReportRs);
        }

        /// <summary>
        /// Update SalaryTableDetailReport into database
        /// </summary>
        /// <param name="SalaryTableDetailReport">SalaryTableDetailReport object</param>
        public void UpdateSalaryTableDetailReport(SalaryTableDetailReport SalaryTableDetailReport)
        {
            Context.Entry(SalaryTableDetailReport).State = EntityState.Modified;
            Context.SaveChanges();
        }
    }
}
