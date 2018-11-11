using Erp.Domain.Staff.Entities;
using Erp.Domain.Staff.Interfaces;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity;
using System.Linq;

namespace Erp.Domain.Staff.Repositories
{
    public class SalaryAdvanceRepository : GenericRepository<ErpStaffDbContext, SalaryAdvance>, ISalaryAdvanceRepository
    {
        public SalaryAdvanceRepository(ErpStaffDbContext context)
            : base(context)
        {

        }

        /// <summary>
        /// Get all SalaryAdvance
        /// </summary>
        /// <returns>SalaryAdvance list</returns>
        public IQueryable<SalaryAdvance> GetAllSalaryAdvance()
        {
            return Context.SalaryAdvance.Where(item => (item.IsDeleted == null || item.IsDeleted == false));
        }
        public IQueryable<vwSalaryAdvance> GetAllvwSalaryAdvance()
        {
            return Context.vwSalaryAdvance.Where(item => (item.IsDeleted == null || item.IsDeleted == false));
        }
        /// <summary>
        /// Get SalaryAdvance information by specific id
        /// </summary>
        /// <param name="SalaryAdvanceId">Id of SalaryAdvance</param>
        /// <returns></returns>
        public SalaryAdvance GetSalaryAdvanceById(int Id)
        {
            return Context.SalaryAdvance.SingleOrDefault(item => item.Id == Id && (item.IsDeleted == null || item.IsDeleted == false));
        }
        public vwSalaryAdvance GetvwSalaryAdvanceById(int Id)
        {
            return Context.vwSalaryAdvance.SingleOrDefault(item => item.Id == Id && (item.IsDeleted == null || item.IsDeleted == false));
        }
        /// <summary>
        /// Insert SalaryAdvance into database
        /// </summary>
        /// <param name="SalaryAdvance">Object infomation</param>
        public void InsertSalaryAdvance(SalaryAdvance SalaryAdvance)
        {
            Context.SalaryAdvance.Add(SalaryAdvance);
            Context.Entry(SalaryAdvance).State = EntityState.Added;
            Context.SaveChanges();
        }

        /// <summary>
        /// Delete SalaryAdvance with specific id
        /// </summary>
        /// <param name="Id">SalaryAdvance Id</param>
        public void DeleteSalaryAdvance(int Id)
        {
            SalaryAdvance deletedSalaryAdvance = GetSalaryAdvanceById(Id);
            Context.SalaryAdvance.Remove(deletedSalaryAdvance);
            Context.Entry(deletedSalaryAdvance).State = EntityState.Deleted;
            Context.SaveChanges();
        }
        
        /// <summary>
        /// Delete a SalaryAdvance with its Id and Update IsDeleted IF that SalaryAdvance has relationship with others
        /// </summary>
        /// <param name="SalaryAdvanceId">Id of SalaryAdvance</param>
        public void DeleteSalaryAdvanceRs(int Id)
        {
            SalaryAdvance deleteSalaryAdvanceRs = GetSalaryAdvanceById(Id);
            deleteSalaryAdvanceRs.IsDeleted = true;
            UpdateSalaryAdvance(deleteSalaryAdvanceRs);
        }

        /// <summary>
        /// Update SalaryAdvance into database
        /// </summary>
        /// <param name="SalaryAdvance">SalaryAdvance object</param>
        public void UpdateSalaryAdvance(SalaryAdvance SalaryAdvance)
        {
            Context.Entry(SalaryAdvance).State = EntityState.Modified;
            Context.SaveChanges();
        }
    }
}
