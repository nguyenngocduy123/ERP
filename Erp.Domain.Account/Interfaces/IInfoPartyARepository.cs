using Erp.Domain.Account.Entities;
using System.Collections.Generic;
using System.Linq;

namespace Erp.Domain.Account.Interfaces
{
    public interface IInfoPartyARepository
    {
        /// <summary>
        /// Get all InfoPartyA
        /// </summary>
        /// <returns>InfoPartyA list</returns>
        IQueryable<InfoPartyA> GetAllInfoPartyA();
        IQueryable<vwInfoPartyA> GetAllvwInfoPartyA();
        /// <summary>
        /// Get InfoPartyA information by specific id
        /// </summary>
        /// <param name="Id">Id of InfoPartyA</param>
        /// <returns></returns>
        InfoPartyA GetInfoPartyAById(int Id);
        vwInfoPartyA GetvwInfoPartyAById(int Id);
        /// <summary>
        /// Insert InfoPartyA into database
        /// </summary>
        /// <param name="InfoPartyA">Object infomation</param>
        void InsertInfoPartyA(InfoPartyA InfoPartyA);

        /// <summary>
        /// Delete InfoPartyA with specific id
        /// </summary>
        /// <param name="Id">InfoPartyA Id</param>
        void DeleteInfoPartyA(int Id);

        /// <summary>
        /// Delete a InfoPartyA with its Id and Update IsDeleted IF that InfoPartyA has relationship with others
        /// </summary>
        /// <param name="Id">Id of InfoPartyA</param>
        void DeleteInfoPartyARs(int Id);

        /// <summary>
        /// Update InfoPartyA into database
        /// </summary>
        /// <param name="InfoPartyA">InfoPartyA object</param>
        void UpdateInfoPartyA(InfoPartyA InfoPartyA);
    }
}
