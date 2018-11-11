using Erp.Domain.Staff.Entities;
using Erp.Domain.Staff.Interfaces;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity;
using System.Linq;

namespace Erp.Domain.Staff.Repositories
{
    public class GiamTruThueTNCNRepository : GenericRepository<ErpStaffDbContext, GiamTruThueTNCN>, IGiamTruThueTNCNRepository
    {
        public GiamTruThueTNCNRepository(ErpStaffDbContext context)
            : base(context)
        {

        }

        /// <summary>
        /// Get all GiamTruThueTNCN
        /// </summary>
        /// <returns>GiamTruThueTNCN list</returns>
        public IQueryable<GiamTruThueTNCN> GetAllGiamTruThueTNCN()
        {
            return Context.GiamTruThueTNCN.Where(item => (item.IsDeleted == null || item.IsDeleted == false));
        }

        /// <summary>
        /// Get GiamTruThueTNCN information by specific id
        /// </summary>
        /// <param name="GiamTruThueTNCNId">Id of GiamTruThueTNCN</param>
        /// <returns></returns>
        public GiamTruThueTNCN GetGiamTruThueTNCNById(int Id)
        {
            return Context.GiamTruThueTNCN.SingleOrDefault(item => item.Id == Id && (item.IsDeleted == null || item.IsDeleted == false));
        }

        /// <summary>
        /// Insert GiamTruThueTNCN into database
        /// </summary>
        /// <param name="GiamTruThueTNCN">Object infomation</param>
        public void InsertGiamTruThueTNCN(GiamTruThueTNCN GiamTruThueTNCN)
        {
            Context.GiamTruThueTNCN.Add(GiamTruThueTNCN);
            Context.Entry(GiamTruThueTNCN).State = EntityState.Added;
            Context.SaveChanges();
        }

        /// <summary>
        /// Delete GiamTruThueTNCN with specific id
        /// </summary>
        /// <param name="Id">GiamTruThueTNCN Id</param>
        public void DeleteGiamTruThueTNCN(int Id)
        {
            GiamTruThueTNCN deletedGiamTruThueTNCN = GetGiamTruThueTNCNById(Id);
            Context.GiamTruThueTNCN.Remove(deletedGiamTruThueTNCN);
            Context.Entry(deletedGiamTruThueTNCN).State = EntityState.Deleted;
            Context.SaveChanges();
        }
        
        /// <summary>
        /// Delete a GiamTruThueTNCN with its Id and Update IsDeleted IF that GiamTruThueTNCN has relationship with others
        /// </summary>
        /// <param name="GiamTruThueTNCNId">Id of GiamTruThueTNCN</param>
        public void DeleteGiamTruThueTNCNRs(int Id)
        {
            GiamTruThueTNCN deleteGiamTruThueTNCNRs = GetGiamTruThueTNCNById(Id);
            deleteGiamTruThueTNCNRs.IsDeleted = true;
            UpdateGiamTruThueTNCN(deleteGiamTruThueTNCNRs);
        }

        /// <summary>
        /// Update GiamTruThueTNCN into database
        /// </summary>
        /// <param name="GiamTruThueTNCN">GiamTruThueTNCN object</param>
        public void UpdateGiamTruThueTNCN(GiamTruThueTNCN GiamTruThueTNCN)
        {
            Context.Entry(GiamTruThueTNCN).State = EntityState.Modified;
            Context.SaveChanges();
        }

        public void DeleteGiamTruThueTNCNByTaxId(int TaxId)
        {
            Helper.SqlHelper.ExecuteSQL("delete from Staff_GiamTruThueTNCN where TaxId = @TaxId", new { TaxId = TaxId });
        }
    }
}
