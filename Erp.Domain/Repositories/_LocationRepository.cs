using Erp.Domain.Entities;
using Erp.Domain.Interfaces;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace Erp.Domain.Repositories
{
    public class LocationRepository : GenericRepository<ErpDbContext, Location>, ILocationRepository
    {
        public LocationRepository(ErpDbContext context)
            : base(context)
        {

        }

        public IEnumerable<Location> GetList(string ParentId)
        {
            return Context.Location.Where(item => item.ParentId == ParentId).OrderBy(item => item.Name);
        }

        public IEnumerable<Location> GetProvinceList()
        {
            return Context.Location.Where(item => item.Group == "Province" && item.ParentId == "0");
        }

        public IEnumerable<Location> GetDistrictList(string ParentId)
        {
            return Context.Location.Where(item => item.Group == "District" && item.ParentId == ParentId);
        }

        public IEnumerable<Location> GetWardList(string ParentId)
        {
            return Context.Location.Where(item => item.Group == "Ward" && item.ParentId == ParentId);
        }
    }
}
