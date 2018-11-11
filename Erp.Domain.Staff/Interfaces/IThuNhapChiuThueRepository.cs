using Erp.Domain.Staff.Entities;
using System.Collections.Generic;
using System.Linq;

namespace Erp.Domain.Staff.Interfaces
{
    public interface IThuNhapChiuThueRepository
    {
        /// <summary>
        /// Get all ThuNhapChiuThue
        /// </summary>
        /// <returns>ThuNhapChiuThue list</returns>
        IQueryable<ThuNhapChiuThue> GetAllThuNhapChiuThue();

        /// <summary>
        /// Get ThuNhapChiuThue information by specific id
        /// </summary>
        /// <param name="Id">Id of ThuNhapChiuThue</param>
        /// <returns></returns>
        ThuNhapChiuThue GetThuNhapChiuThueById(int Id);

        /// <summary>
        /// Insert ThuNhapChiuThue into database
        /// </summary>
        /// <param name="ThuNhapChiuThue">Object infomation</param>
        void InsertThuNhapChiuThue(ThuNhapChiuThue ThuNhapChiuThue);

        /// <summary>
        /// Delete ThuNhapChiuThue with specific id
        /// </summary>
        /// <param name="Id">ThuNhapChiuThue Id</param>
        void DeleteThuNhapChiuThue(int Id);

        /// <summary>
        /// Delete a ThuNhapChiuThue with its Id and Update IsDeleted IF that ThuNhapChiuThue has relationship with others
        /// </summary>
        /// <param name="Id">Id of ThuNhapChiuThue</param>
        void DeleteThuNhapChiuThueRs(int Id);

        /// <summary>
        /// Update ThuNhapChiuThue into database
        /// </summary>
        /// <param name="ThuNhapChiuThue">ThuNhapChiuThue object</param>
        void UpdateThuNhapChiuThue(ThuNhapChiuThue ThuNhapChiuThue);
        void DeleteThuNhapChiuThueByTaxId(int TaxId);
    }
}
