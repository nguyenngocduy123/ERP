using Erp.Domain.Staff.Entities;
using System.Collections.Generic;
using System.Linq;

namespace Erp.Domain.Staff.Interfaces
{
    public interface ICheckInOutRepository
    {
        /// <summary>
        /// Get all CheckInOut
        /// </summary>
        /// <returns>CheckInOut list</returns>
        IQueryable<CheckInOut> GetAllCheckInOut();
        IQueryable<vwCheckInOut> GetAllvwCheckInOut();
        /// <summary>
        /// Get CheckInOut information by specific id
        /// </summary>
        /// <param name="Id">Id of CheckInOut</param>
        /// <returns></returns>
        CheckInOut GetCheckInOutById(int Id);
        vwCheckInOut GetvwCheckInOutById(int Id);

        IQueryable<FingerPrint> GetAllFingerPrint();
        IQueryable<vwFingerPrint> GetAllvwFingerPrint();
        /// <summary>
        /// Insert CheckInOut into database
        /// </summary>
        /// <param name="CheckInOut">Object infomation</param>
        void InsertCheckInOut(CheckInOut CheckInOut);

        void InsertFingerPrint(FingerPrint FingerPrint);

        /// <summary>
        /// Delete CheckInOut with specific id
        /// </summary>
        /// <param name="Id">CheckInOut Id</param>
        void DeleteCheckInOut(int Id);

        /// <summary>
        /// Delete a CheckInOut with its Id and Update IsDeleted IF that CheckInOut has relationship with others
        /// </summary>
        /// <param name="Id">Id of CheckInOut</param>
        void DeleteCheckInOutRs(int Id);

        /// <summary>
        /// Update CheckInOut into database
        /// </summary>
        /// <param name="CheckInOut">CheckInOut object</param>
        void UpdateCheckInOut(CheckInOut CheckInOut);
    }
}
