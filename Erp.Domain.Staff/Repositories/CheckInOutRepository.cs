using Erp.Domain.Staff.Entities;
using Erp.Domain.Staff.Interfaces;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity;
using System.Linq;

namespace Erp.Domain.Staff.Repositories
{
    public class CheckInOutRepository : GenericRepository<ErpStaffDbContext, CheckInOut>, ICheckInOutRepository
    {
        public CheckInOutRepository(ErpStaffDbContext context)
            : base(context)
        {

        }

        /// <summary>
        /// Get all CheckInOut
        /// </summary>
        /// <returns>CheckInOut list</returns>
        public IQueryable<CheckInOut> GetAllCheckInOut()
        {
            return Context.CheckInOut;
        }
        public IQueryable<vwCheckInOut> GetAllvwCheckInOut()
        {
            return Context.vwCheckInOut;
        }
        /// <summary>
        /// Get CheckInOut information by specific id
        /// </summary>
        /// <param name="CheckInOutId">Id of CheckInOut</param>
        /// <returns></returns>
        public CheckInOut GetCheckInOutById(int Id)
        {
            return Context.CheckInOut.SingleOrDefault(item => item.Id == Id);
        }
        public vwCheckInOut GetvwCheckInOutById(int Id)
        {
            return Context.vwCheckInOut.SingleOrDefault(item => item.Id == Id);
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
        /// Insert CheckInOut into database
        /// </summary>
        /// <param name="CheckInOut">Object infomation</param>
        public void InsertCheckInOut(CheckInOut CheckInOut)
        {
            Context.CheckInOut.Add(CheckInOut);
            Context.Entry(CheckInOut).State = EntityState.Added;
            Context.SaveChanges();
        }

        public void InsertFingerPrint(FingerPrint FingerPrint)
        {
            Context.FingerPrint.Add(FingerPrint);
            Context.Entry(FingerPrint).State = EntityState.Added;
            Context.SaveChanges();
        }

        /// <summary>
        /// Delete CheckInOut with specific id
        /// </summary>
        /// <param name="Id">CheckInOut Id</param>
        public void DeleteCheckInOut(int Id)
        {
            CheckInOut deletedCheckInOut = GetCheckInOutById(Id);
            Context.CheckInOut.Remove(deletedCheckInOut);
            Context.Entry(deletedCheckInOut).State = EntityState.Deleted;
            Context.SaveChanges();
        }
        
        /// <summary>
        /// Delete a CheckInOut with its Id and Update IsDeleted IF that CheckInOut has relationship with others
        /// </summary>
        /// <param name="CheckInOutId">Id of CheckInOut</param>
        public void DeleteCheckInOutRs(int Id)
        {
            //CheckInOut deleteCheckInOutRs = GetCheckInOutById(Id);
            //deleteCheckInOutRs.IsDeleted = true;
            //UpdateCheckInOut(deleteCheckInOutRs);
        }

        /// <summary>
        /// Update CheckInOut into database
        /// </summary>
        /// <param name="CheckInOut">CheckInOut object</param>
        public void UpdateCheckInOut(CheckInOut CheckInOut)
        {
            Context.Entry(CheckInOut).State = EntityState.Modified;
            Context.SaveChanges();
        }

        public void UpdateFingerPrint(FingerPrint FingerPrint)
        {
            Context.Entry(FingerPrint).State = EntityState.Modified;
            Context.SaveChanges();
        }
    }
}
