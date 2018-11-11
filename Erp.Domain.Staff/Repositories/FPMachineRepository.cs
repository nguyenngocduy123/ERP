using Erp.Domain.Staff.Entities;
using Erp.Domain.Staff.Interfaces;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity;
using System.Linq;

namespace Erp.Domain.Staff.Repositories
{
    public class FPMachineRepository : GenericRepository<ErpStaffDbContext, FPMachine>, IFPMachineRepository
    {
        public FPMachineRepository(ErpStaffDbContext context)
            : base(context)
        {

        }

        /// <summary>
        /// Get all FPMachine
        /// </summary>
        /// <returns>FPMachine list</returns>
        public IQueryable<FPMachine> GetAllFPMachine()
        {
            return Context.FPMachine.Where(item => (item.IsDeleted == null || item.IsDeleted == false));
        }
        public IQueryable<FingerPrint> GetAllFingerPrint()
        {
            return Context.FingerPrint;
        }
        public IQueryable<vwFingerPrint> GetAllvwFingerPrint()
        {
            return Context.vwFingerPrint;
        }
        /// <summary>
        /// Get FPMachine information by specific id
        /// </summary>
        /// <param name="FPMachineId">Id of FPMachine</param>
        /// <returns></returns>
        public FPMachine GetFPMachineById(int Id)
        {
            return Context.FPMachine.SingleOrDefault(item => item.Id == Id && (item.IsDeleted == null || item.IsDeleted == false));
        }

        /// <summary>
        /// Insert FPMachine into database
        /// </summary>
        /// <param name="FPMachine">Object infomation</param>
        public void InsertFPMachine(FPMachine FPMachine)
        {
            Context.FPMachine.Add(FPMachine);
            Context.Entry(FPMachine).State = EntityState.Added;
            Context.SaveChanges();
        }

        /// <summary>
        /// Delete FPMachine with specific id
        /// </summary>
        /// <param name="Id">FPMachine Id</param>
        public void DeleteFPMachine(int Id)
        {
            FPMachine deletedFPMachine = GetFPMachineById(Id);
            Context.FPMachine.Remove(deletedFPMachine);
            Context.Entry(deletedFPMachine).State = EntityState.Deleted;
            Context.SaveChanges();
        }
        
        /// <summary>
        /// Delete a FPMachine with its Id and Update IsDeleted IF that FPMachine has relationship with others
        /// </summary>
        /// <param name="FPMachineId">Id of FPMachine</param>
        public void DeleteFPMachineRs(int Id)
        {
            FPMachine deleteFPMachineRs = GetFPMachineById(Id);
            deleteFPMachineRs.IsDeleted = true;
            UpdateFPMachine(deleteFPMachineRs);
        }

        /// <summary>
        /// Update FPMachine into database
        /// </summary>
        /// <param name="FPMachine">FPMachine object</param>
        public void UpdateFPMachine(FPMachine FPMachine)
        {
            Context.Entry(FPMachine).State = EntityState.Modified;
            Context.SaveChanges();
        }
        public void UpdateFingerPrint(FingerPrint FingerPrint)
        {
            Context.Entry(FingerPrint).State = EntityState.Modified;
            Context.SaveChanges();
        }
    }
}
