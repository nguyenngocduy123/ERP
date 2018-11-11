using Erp.Domain.Account.Entities;
using System.Collections.Generic;
using System.Linq;

namespace Erp.Domain.Account.Interfaces
{
    public interface IMemberCardRepository
    {
        /// <summary>
        /// Get all MemberCard
        /// </summary>
        /// <returns>MemberCard list</returns>
        IQueryable<MemberCard> GetAllMemberCard();
        IQueryable<vwMemberCard> GetAllvwMemberCard();
        /// <summary>
        /// Get MemberCard information by specific id
        /// </summary>
        /// <param name="Id">Id of MemberCard</param>
        /// <returns></returns>
        MemberCard GetMemberCardById(int Id);
        vwMemberCard GetvwMemberCardById(int Id);
        /// <summary>
        /// Insert MemberCard into database
        /// </summary>
        /// <param name="MemberCard">Object infomation</param>
        void InsertMemberCard(MemberCard MemberCard);

        /// <summary>
        /// Delete MemberCard with specific id
        /// </summary>
        /// <param name="Id">MemberCard Id</param>
        void DeleteMemberCard(int Id);

        /// <summary>
        /// Delete a MemberCard with its Id and Update IsDeleted IF that MemberCard has relationship with others
        /// </summary>
        /// <param name="Id">Id of MemberCard</param>
        void DeleteMemberCardRs(int Id);

        /// <summary>
        /// Update MemberCard into database
        /// </summary>
        /// <param name="MemberCard">MemberCard object</param>
        void UpdateMemberCard(MemberCard MemberCard);
    }
}
