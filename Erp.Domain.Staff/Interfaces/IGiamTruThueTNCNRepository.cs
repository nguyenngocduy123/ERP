using Erp.Domain.Staff.Entities;
using System.Collections.Generic;
using System.Linq;

namespace Erp.Domain.Staff.Interfaces
{
    public interface IGiamTruThueTNCNRepository
    {
        /// <summary>
        /// Get all GiamTruThueTNCN
        /// </summary>
        /// <returns>GiamTruThueTNCN list</returns>
        IQueryable<GiamTruThueTNCN> GetAllGiamTruThueTNCN();

        /// <summary>
        /// Get GiamTruThueTNCN information by specific id
        /// </summary>
        /// <param name="Id">Id of GiamTruThueTNCN</param>
        /// <returns></returns>
        GiamTruThueTNCN GetGiamTruThueTNCNById(int Id);

        /// <summary>
        /// Insert GiamTruThueTNCN into database
        /// </summary>
        /// <param name="GiamTruThueTNCN">Object infomation</param>
        void InsertGiamTruThueTNCN(GiamTruThueTNCN GiamTruThueTNCN);

        /// <summary>
        /// Delete GiamTruThueTNCN with specific id
        /// </summary>
        /// <param name="Id">GiamTruThueTNCN Id</param>
        void DeleteGiamTruThueTNCN(int Id);

        /// <summary>
        /// Delete a GiamTruThueTNCN with its Id and Update IsDeleted IF that GiamTruThueTNCN has relationship with others
        /// </summary>
        /// <param name="Id">Id of GiamTruThueTNCN</param>
        void DeleteGiamTruThueTNCNRs(int Id);

        /// <summary>
        /// Update GiamTruThueTNCN into database
        /// </summary>
        /// <param name="GiamTruThueTNCN">GiamTruThueTNCN object</param>
        void UpdateGiamTruThueTNCN(GiamTruThueTNCN GiamTruThueTNCN);

        void DeleteGiamTruThueTNCNByTaxId(int TaxId);
    }
}
