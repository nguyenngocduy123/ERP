using <APP_NAME>.Domain.<AREA_NAME>.Entities;
using System.Collections.Generic;
using System.Linq;

namespace <APP_NAME>.Domain.<AREA_NAME>.Interfaces
{
    public interface I<MODULE_NAME>Repository
    {
        /// <summary>
        /// Get all <MODULE_NAME>
        /// </summary>
        /// <returns><MODULE_NAME> list</returns>
        IQueryable<<MODULE_NAME>> GetAll<MODULE_NAME>();

        /// <summary>
        /// Get <MODULE_NAME> information by specific id
        /// </summary>
        /// <param name="Id">Id of <MODULE_NAME></param>
        /// <returns></returns>
        <MODULE_NAME> Get<MODULE_NAME>ById(int Id);

        /// <summary>
        /// Insert <MODULE_NAME> into database
        /// </summary>
        /// <param name="<MODULE_NAME>">Object infomation</param>
        void Insert<MODULE_NAME>(<MODULE_NAME> <MODULE_NAME>);

        /// <summary>
        /// Delete <MODULE_NAME> with specific id
        /// </summary>
        /// <param name="Id"><MODULE_NAME> Id</param>
        void Delete<MODULE_NAME>(int Id);

        /// <summary>
        /// Delete a <MODULE_NAME> with its Id and Update IsDeleted IF that <MODULE_NAME> has relationship with others
        /// </summary>
        /// <param name="Id">Id of <MODULE_NAME></param>
        void Delete<MODULE_NAME>Rs(int Id);

        /// <summary>
        /// Update <MODULE_NAME> into database
        /// </summary>
        /// <param name="<MODULE_NAME>"><MODULE_NAME> object</param>
        void Update<MODULE_NAME>(<MODULE_NAME> <MODULE_NAME>);
    }
}
