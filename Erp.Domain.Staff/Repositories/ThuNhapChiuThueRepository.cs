using Erp.Domain.Staff.Entities;
using Erp.Domain.Staff.Interfaces;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity;
using System.Linq;

namespace Erp.Domain.Staff.Repositories
{
    public class ThuNhapChiuThueRepository : GenericRepository<ErpStaffDbContext, ThuNhapChiuThue>, IThuNhapChiuThueRepository
    {
        public ThuNhapChiuThueRepository(ErpStaffDbContext context)
            : base(context)
        {

        }

        /// <summary>
        /// Get all ThuNhapChiuThue
        /// </summary>
        /// <returns>ThuNhapChiuThue list</returns>
        public IQueryable<ThuNhapChiuThue> GetAllThuNhapChiuThue()
        {
            return Context.ThuNhapChiuThue.Where(item => (item.IsDeleted == null || item.IsDeleted == false));
        }

        /// <summary>
        /// Get ThuNhapChiuThue information by specific id
        /// </summary>
        /// <param name="ThuNhapChiuThueId">Id of ThuNhapChiuThue</param>
        /// <returns></returns>
        public ThuNhapChiuThue GetThuNhapChiuThueById(int Id)
        {
            return Context.ThuNhapChiuThue.SingleOrDefault(item => item.Id == Id && (item.IsDeleted == null || item.IsDeleted == false));
        }

        /// <summary>
        /// Insert ThuNhapChiuThue into database
        /// </summary>
        /// <param name="ThuNhapChiuThue">Object infomation</param>
        public void InsertThuNhapChiuThue(ThuNhapChiuThue ThuNhapChiuThue)
        {
            Context.ThuNhapChiuThue.Add(ThuNhapChiuThue);
            Context.Entry(ThuNhapChiuThue).State = EntityState.Added;
            Context.SaveChanges();
        }

        /// <summary>
        /// Delete ThuNhapChiuThue with specific id
        /// </summary>
        /// <param name="Id">ThuNhapChiuThue Id</param>
        public void DeleteThuNhapChiuThue(int Id)
        {
            ThuNhapChiuThue deletedThuNhapChiuThue = GetThuNhapChiuThueById(Id);
            Context.ThuNhapChiuThue.Remove(deletedThuNhapChiuThue);
            Context.Entry(deletedThuNhapChiuThue).State = EntityState.Deleted;
            Context.SaveChanges();
        }
        
        /// <summary>
        /// Delete a ThuNhapChiuThue with its Id and Update IsDeleted IF that ThuNhapChiuThue has relationship with others
        /// </summary>
        /// <param name="ThuNhapChiuThueId">Id of ThuNhapChiuThue</param>
        public void DeleteThuNhapChiuThueRs(int Id)
        {
            ThuNhapChiuThue deleteThuNhapChiuThueRs = GetThuNhapChiuThueById(Id);
            deleteThuNhapChiuThueRs.IsDeleted = true;
            UpdateThuNhapChiuThue(deleteThuNhapChiuThueRs);
        }

        /// <summary>
        /// Update ThuNhapChiuThue into database
        /// </summary>
        /// <param name="ThuNhapChiuThue">ThuNhapChiuThue object</param>
        public void UpdateThuNhapChiuThue(ThuNhapChiuThue ThuNhapChiuThue)
        {
            Context.Entry(ThuNhapChiuThue).State = EntityState.Modified;
            Context.SaveChanges();
        }

        public void DeleteThuNhapChiuThueByTaxId(int TaxId)
        {
            Helper.SqlHelper.ExecuteSQL("delete from Staff_ThuNhapChiuThue where TaxId = @TaxId", new { TaxId = TaxId });
        }
    }
}
