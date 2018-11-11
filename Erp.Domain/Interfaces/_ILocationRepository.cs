using Erp.Domain.Entities;
using System.Collections.Generic;

namespace Erp.Domain.Interfaces
{
    public interface ILocationRepository
    {
        IEnumerable<Location> GetList(string ParentId);
        IEnumerable<Location> GetProvinceList();

        IEnumerable<Location> GetDistrictList(string ParentId);

        IEnumerable<Location> GetWardList(string ParentId);
    }
}
