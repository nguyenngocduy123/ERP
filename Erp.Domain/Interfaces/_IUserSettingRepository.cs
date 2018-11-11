using Erp.Domain.Entities;
using System.Collections.Generic;
using System.Linq;

namespace Erp.Domain.Interfaces
{
    public interface IUserSettingRepository
    {
        /// <summary>
        /// Get all UserSetting
        /// </summary>
        /// <returns>UserSetting list</returns>
        IQueryable<UserSetting> GetAllUserSettings();

        /// <summary>
        /// Get UserSetting information by specific id
        /// </summary>
        /// <param name="Id">Id of UserSetting</param>
        /// <returns></returns>
        UserSetting GetUserSettingById(int Id);

        string GetUserSettingByKey(string Key, int UserId);

        void SetUserSettingByKey(string Key, int UserId, string Value);

        /// <summary>
        /// Insert UserSetting into database
        /// </summary>
        /// <param name="UserSetting">Object infomation</param>
        void InsertUserSetting(UserSetting UserSetting);

        /// <summary>
        /// Delete UserSetting with specific id
        /// </summary>
        /// <param name="Id">UserSetting Id</param>
        void DeleteUserSetting(int Id);

        /// <summary>
        /// Delete a UserSetting with its Id and Update IsDeleted IF that UserSetting has relationship with others
        /// </summary>
        /// <param name="Id">Id of UserSetting</param>
        void DeleteUserSettingRs(int Id);

        /// <summary>
        /// Update UserSetting into database
        /// </summary>
        /// <param name="UserSetting">UserSetting object</param>
        void UpdateUserSetting(UserSetting UserSetting);
    }
}
