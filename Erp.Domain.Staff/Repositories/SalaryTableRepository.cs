using Erp.Domain.Staff.Entities;
using Erp.Domain.Staff.Interfaces;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity;
using System.Linq;

namespace Erp.Domain.Staff.Repositories
{
    public class SalaryTableRepository : GenericRepository<ErpStaffDbContext, SalaryTable>, ISalaryTableRepository
    {
        public SalaryTableRepository(ErpStaffDbContext context)
            : base(context)
        {

        }

        /// <summary>
        /// Get all SalaryTable
        /// </summary>
        /// <returns>SalaryTable list</returns>
        public IQueryable<SalaryTable> GetAllSalaryTable()
        {
            return Context.SalaryTable.Where(item => (item.IsDeleted == null || item.IsDeleted == false));
        }

        /// <summary>
        /// Get SalaryTable information by specific id
        /// </summary>
        /// <param name="SalaryTableId">Id of SalaryTable</param>
        /// <returns></returns>
        public SalaryTable GetSalaryTableById(int Id)
        {
            return Context.SalaryTable.SingleOrDefault(item => item.Id == Id && (item.IsDeleted == null || item.IsDeleted == false));
        }

        /// <summary>
        /// Insert SalaryTable into database
        /// </summary>
        /// <param name="SalaryTable">Object infomation</param>
        public void InsertSalaryTable(SalaryTable SalaryTable)
        {
            Context.SalaryTable.Add(SalaryTable);
            Context.Entry(SalaryTable).State = EntityState.Added;
            Context.SaveChanges();
        }

        /// <summary>
        /// Delete SalaryTable with specific id
        /// </summary>
        /// <param name="Id">SalaryTable Id</param>
        public void DeleteSalaryTable(int Id)
        {
            SalaryTable deletedSalaryTable = GetSalaryTableById(Id);
            Context.SalaryTable.Remove(deletedSalaryTable);
            Context.Entry(deletedSalaryTable).State = EntityState.Deleted;
            Context.SaveChanges();
        }
        
        /// <summary>
        /// Delete a SalaryTable with its Id and Update IsDeleted IF that SalaryTable has relationship with others
        /// </summary>
        /// <param name="SalaryTableId">Id of SalaryTable</param>
        public void DeleteSalaryTableRs(int Id)
        {
            SalaryTable deleteSalaryTableRs = GetSalaryTableById(Id);
            deleteSalaryTableRs.IsDeleted = true;
            UpdateSalaryTable(deleteSalaryTableRs);
        }

        /// <summary>
        /// Update SalaryTable into database
        /// </summary>
        /// <param name="SalaryTable">SalaryTable object</param>
        public void UpdateSalaryTable(SalaryTable SalaryTable)
        {
            Context.Entry(SalaryTable).State = EntityState.Modified;
            Context.SaveChanges();
        }
    }
}
