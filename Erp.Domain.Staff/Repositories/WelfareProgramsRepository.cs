using Erp.Domain.Staff.Entities;
using Erp.Domain.Staff.Interfaces;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity;
using System.Linq;

namespace Erp.Domain.Staff.Repositories
{
    public class WelfareProgramsRepository : GenericRepository<ErpStaffDbContext, WelfarePrograms>, IWelfareProgramsRepository
    {
        public WelfareProgramsRepository(ErpStaffDbContext context)
            : base(context)
        {

        }

        /// <summary>
        /// Get all WelfarePrograms
        /// </summary>
        /// <returns>WelfarePrograms list</returns>
        public IQueryable<WelfarePrograms> GetAllWelfarePrograms()
        {
            return Context.WelfarePrograms.Where(item => (item.IsDeleted == null || item.IsDeleted == false));
        }

        /// <summary>
        /// Get WelfarePrograms information by specific id
        /// </summary>
        /// <param name="WelfareProgramsId">Id of WelfarePrograms</param>
        /// <returns></returns>
        public WelfarePrograms GetWelfareProgramsById(int Id)
        {
            return Context.WelfarePrograms.SingleOrDefault(item => item.Id == Id && (item.IsDeleted == null || item.IsDeleted == false));
        }

        /// <summary>
        /// Insert WelfarePrograms into database
        /// </summary>
        /// <param name="WelfarePrograms">Object infomation</param>
        public void InsertWelfarePrograms(WelfarePrograms WelfarePrograms)
        {
            Context.WelfarePrograms.Add(WelfarePrograms);
            Context.Entry(WelfarePrograms).State = EntityState.Added;
            Context.SaveChanges();
        }

        /// <summary>
        /// Delete WelfarePrograms with specific id
        /// </summary>
        /// <param name="Id">WelfarePrograms Id</param>
        public void DeleteWelfarePrograms(int Id)
        {
            WelfarePrograms deletedWelfarePrograms = GetWelfareProgramsById(Id);
            Context.WelfarePrograms.Remove(deletedWelfarePrograms);
            Context.Entry(deletedWelfarePrograms).State = EntityState.Deleted;
            Context.SaveChanges();
        }
        
        /// <summary>
        /// Delete a WelfarePrograms with its Id and Update IsDeleted IF that WelfarePrograms has relationship with others
        /// </summary>
        /// <param name="WelfareProgramsId">Id of WelfarePrograms</param>
        public void DeleteWelfareProgramsRs(int Id)
        {
            WelfarePrograms deleteWelfareProgramsRs = GetWelfareProgramsById(Id);
            deleteWelfareProgramsRs.IsDeleted = true;
            UpdateWelfarePrograms(deleteWelfareProgramsRs);
        }

        /// <summary>
        /// Update WelfarePrograms into database
        /// </summary>
        /// <param name="WelfarePrograms">WelfarePrograms object</param>
        public void UpdateWelfarePrograms(WelfarePrograms WelfarePrograms)
        {
            Context.Entry(WelfarePrograms).State = EntityState.Modified;
            Context.SaveChanges();
        }
    }
}
