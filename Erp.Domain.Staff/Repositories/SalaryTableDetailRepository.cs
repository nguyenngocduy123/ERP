using Erp.Domain.Staff.Entities;
using Erp.Domain.Staff.Interfaces;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace Erp.Domain.Staff.Repositories
{
    public class SalaryTableDetailRepository : GenericRepository<ErpStaffDbContext, SalaryTableDetail>, ISalaryTableDetailRepository
    {
        public SalaryTableDetailRepository(ErpStaffDbContext context)
            : base(context)
        {

        }

        /// <summary>
        /// Get all SalaryTableDetail
        /// </summary>
        /// <returns>SalaryTableDetail list</returns>
        public IQueryable<SalaryTableDetail> GetAllSalaryTableDetail()
        {
            return Context.SalaryTableDetail;
        }

        ///// <summary>
        ///// Get SalaryTableDetail information by specific id
        ///// </summary>
        ///// <param name="SalaryTableDetailId">Id of SalaryTableDetail</param>
        ///// <returns></returns>
        //public SalaryTableDetail GetSalaryTableDetailById(int Id)
        //{
        //    return Context.SalaryTableDetail.SingleOrDefault(item => item.Id == Id && (item.IsDeleted == null || item.IsDeleted == false));
        //}

        /// <summary>
        /// Insert SalaryTableDetail into database
        /// </summary>
        /// <param name="SalaryTableDetail">Object infomation</param>
        public void InsertSalaryTableDetail(SalaryTableDetail SalaryTableDetail)
        {
            Context.SalaryTableDetail.Add(SalaryTableDetail);
            Context.Entry(SalaryTableDetail).State = EntityState.Added;
            Context.SaveChanges();
        }

        /// <summary>
        /// Delete SalaryTableDetail with specific id
        /// </summary>
        /// <param name="Id">SalaryTableDetail Id</param>
        public void DeleteSalaryTableDetail(int Id)
        {
            Helper.SqlHelper.ExecuteSQL("delete from Staff_SalaryTableDetail where SalaryTableId = @SalaryTableId", new { SalaryTableId = Id });
        }

        /// <summary>
        /// Update SalaryTableDetail into database
        /// </summary>
        /// <param name="SalaryTableDetail">SalaryTableDetail object</param>
        public void UpdateSalaryTableDetail(SalaryTableDetail SalaryTableDetail)
        {
            Context.Entry(SalaryTableDetail).State = EntityState.Modified;
            Context.SaveChanges();
        }
    }
}
