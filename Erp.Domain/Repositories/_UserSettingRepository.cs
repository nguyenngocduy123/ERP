using Erp.Domain.Entities;
using Erp.Domain.Interfaces;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace Erp.Domain.Repositories
{
    public class UserSettingRepository : GenericRepository<ErpDbContext, UserSetting>, IUserSettingRepository
    {
        public UserSettingRepository(ErpDbContext context)
            : base(context)
        {

        }

        /// <summary>
        /// Get all UserSetting
        /// </summary>
        /// <returns>UserSetting list</returns>
        public IQueryable<UserSetting> GetAllUserSettings()
        {
            return Context.UserSetting;
        }

        /// <summary>
        /// Get UserSetting information by specific id
        /// </summary>
        /// <param name="UserSettingId">Id of UserSetting</param>
        /// <returns></returns>
        public UserSetting GetUserSettingById(int Id)
        {
            return Context.UserSetting.SingleOrDefault(item => item.Id == Id);
        }

        public string GetUserSettingByKey(string Key, int UserId)
        {
            try
            {
                int SettingId = Context.Setting.SingleOrDefault(item => item.Key == Key).Id;
                return Context.UserSetting.SingleOrDefault(item => item.UserId == UserId && item.SettingId == SettingId).Value;
            }
            catch { }

            return null;
        }

        public void SetUserSettingByKey(string Key, int UserId, string Value)
        {
            try
            {
                int SettingId = Context.Setting.SingleOrDefault(item => item.Key == Key).Id;
                var userSetting = Context.UserSetting.SingleOrDefault(item => item.UserId == UserId && item.SettingId == SettingId);

                if (userSetting == null)
                {
                    UserSetting item = new UserSetting { 
                        UserId = UserId,
                        SettingId = SettingId,
                        Value = Value
                    };
                    InsertUserSetting(item);
                    return;
                }

                userSetting.Value = Value;

                UpdateUserSetting(userSetting);
                return;
            }
            catch { }
        }

        /// <summary>
        /// Insert UserSetting into database
        /// </summary>
        /// <param name="UserSetting">Object infomation</param>
        public void InsertUserSetting(UserSetting UserSetting)
        {
            Context.UserSetting.Add(UserSetting);
            Context.Entry(UserSetting).State = EntityState.Added;
            Context.SaveChanges();
        }

        /// <summary>
        /// Delete UserSetting with specific id
        /// </summary>
        /// <param name="Id">UserSetting Id</param>
        public void DeleteUserSetting(int Id)
        {
            UserSetting deletedUserSetting = GetUserSettingById(Id);
            Context.UserSetting.Remove(deletedUserSetting);
            Context.Entry(deletedUserSetting).State = EntityState.Deleted;
            Context.SaveChanges();
        }
        
        /// <summary>
        /// Delete a UserSetting with its Id and Update IsDeleted IF that UserSetting has relationship with others
        /// </summary>
        /// <param name="UserSettingId">Id of UserSetting</param>
        public void DeleteUserSettingRs(int Id)
        {
            UserSetting deleteUserSettingRs = GetUserSettingById(Id);
            UpdateUserSetting(deleteUserSettingRs);
        }

        /// <summary>
        /// Update UserSetting into database
        /// </summary>
        /// <param name="UserSetting">UserSetting object</param>
        public void UpdateUserSetting(UserSetting UserSetting)
        {
            Context.Entry(UserSetting).State = EntityState.Modified;
            Context.SaveChanges();
        }
    }
}
