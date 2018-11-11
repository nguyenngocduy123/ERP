using Erp.Domain.Account.Entities;
using System.Collections.Generic;
using System.Linq;

namespace Erp.Domain.Account.Interfaces
{
    public interface IMemberCardDetailRepository
    {
        /// <summary>
        /// Get all MemberCardDetail
        /// </summary>
        /// <returns>MemberCardDetail list</returns>
        IQueryable<MemberCardDetail> GetAllMemberCardDetail();

        /// <summary>
        /// Get MemberCardDetail information by specific id
        /// </summary>
        /// <param name="Id">Id of MemberCardDetail</param>
        /// <returns></returns>
        MemberCardDetail GetMemberCardDetailById(int Id);

        /// <summary>
        /// Insert MemberCardDetail into database
        /// </summary>
        /// <param name="MemberCardDetail">Object infomation</param>
        void InsertMemberCardDetail(MemberCardDetail MemberCardDetail);

        /// <summary>
        /// Delete MemberCardDetail with specific id
        /// </summary>
        /// <param name="Id">MemberCardDetail Id</param>
        void DeleteMemberCardDetail(int Id);

        /// <summary>
        /// Delete a MemberCardDetail with its Id and Update IsDeleted IF that MemberCardDetail has relationship with others
        /// </summary>
        /// <param name="Id">Id of MemberCardDetail</param>
        void DeleteMemberCardDetailRs(int Id);

        /// <summary>
        /// Update MemberCardDetail into database
        /// </summary>
        /// <param name="MemberCardDetail">MemberCardDetail object</param>
        void UpdateMemberCardDetail(MemberCardDetail MemberCardDetail);
    }
}
