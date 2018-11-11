using Erp.Domain.Staff.Entities;
using Erp.Domain.Staff.Interfaces;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity;
using System.Linq;

namespace Erp.Domain.Staff.Repositories
{
    public class SalaryTableDetail_StaffRepository : GenericRepository<ErpStaffDbContext, SalaryTableDetail_Staff>, ISalaryTableDetail_StaffRepository
    {
        public SalaryTableDetail_StaffRepository(ErpStaffDbContext context)
            : base(context)
        {

        }

        /// <summary>
        /// Get all SalaryTableDetail_Staff
        /// </summary>
        /// <returns>SalaryTableDetail_Staff list</returns>
        public IQueryable<SalaryTableDetail_Staff> GetAllSalaryTableDetail_Staff()
        {
            return Context.SalaryTableDetail_Staff.Where(item => (item.IsDeleted == null || item.IsDeleted == false));
        }

        /// <summary>
        /// Get SalaryTableDetail_Staff information by specific id
        /// </summary>
        /// <param name="SalaryTableDetail_StaffId">Id of SalaryTableDetail_Staff</param>
        /// <returns></returns>
        public SalaryTableDetail_Staff GetSalaryTableDetail_StaffById(int Id)
        {
            return Context.SalaryTableDetail_Staff.SingleOrDefault(item => item.Id == Id && (item.IsDeleted == null || item.IsDeleted == false));
        }
        public SalaryTableDetail_Staff GetSalaryTableDetail_StaffBySalaryTableId(int Id)
        {
            return Context.SalaryTableDetail_Staff.FirstOrDefault(item => item.SalaryTableId == Id && (item.IsDeleted == null || item.IsDeleted == false));
        }
        /// <summary>
        /// Insert SalaryTableDetail_Staff into database
        /// </summary>
        /// <param name="SalaryTableDetail_Staff">Object infomation</param>
        public void InsertSalaryTableDetail_Staff(SalaryTableDetail_Staff SalaryTableDetail_Staff)
        {
            Context.SalaryTableDetail_Staff.Add(SalaryTableDetail_Staff);
            Context.Entry(SalaryTableDetail_Staff).State = EntityState.Added;
            Context.SaveChanges();
        }

        /// <summary>
        /// Delete SalaryTableDetail_Staff with specific id
        /// </summary>
        /// <param name="Id">SalaryTableDetail_Staff Id</param>
        public void DeleteSalaryTableDetail_Staff(int Id)
        {
            SalaryTableDetail_Staff deletedSalaryTableDetail_Staff = GetSalaryTableDetail_StaffById(Id);
            Context.SalaryTableDetail_Staff.Remove(deletedSalaryTableDetail_Staff);
            Context.Entry(deletedSalaryTableDetail_Staff).State = EntityState.Deleted;
            Context.SaveChanges();
        }
        public void DeleteSalaryTableDetail_StaffBySalaryTableId(int SalaryTableId)
        {
            Helper.SqlHelper.ExecuteSQL("delete from Staff_SalaryTableDetail_Staff where SalaryTableId = @SalaryTableId", new { SalaryTableId = SalaryTableId });
        }
        /// <summary>
        /// Delete a SalaryTableDetail_Staff with its Id and Update IsDeleted IF that SalaryTableDetail_Staff has relationship with others
        /// </summary>
        /// <param name="SalaryTableDetail_StaffId">Id of SalaryTableDetail_Staff</param>
        public void DeleteSalaryTableDetail_StaffRs(int Id)
        {
            SalaryTableDetail_Staff deleteSalaryTableDetail_StaffRs = GetSalaryTableDetail_StaffById(Id);
            deleteSalaryTableDetail_StaffRs.IsDeleted = true;
            UpdateSalaryTableDetail_Staff(deleteSalaryTableDetail_StaffRs);
        }

        /// <summary>
        /// Update SalaryTableDetail_Staff into database
        /// </summary>
        /// <param name="SalaryTableDetail_Staff">SalaryTableDetail_Staff object</param>
        public void UpdateSalaryTableDetail_Staff(SalaryTableDetail_Staff SalaryTableDetail_Staff)
        {
            Context.Entry(SalaryTableDetail_Staff).State = EntityState.Modified;
            Context.SaveChanges();
        }
    }
}
