using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WMS.Data;

namespace WMS.Contracts
{
    public interface ILocationRepository:IRepositoryBase<Location>
    {
        Task<ICollection<Location>> GetLocationsByCompany(int id);
        Task<ICollection<Location>> GetLocationsByDepartment(int id);
    }
}
