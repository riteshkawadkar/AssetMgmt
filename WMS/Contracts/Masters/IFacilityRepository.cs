using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WMS.Data;

namespace WMS.Contracts
{
    public interface IFacilityRepository:IRepositoryBase<Facility>
    {
        Task<ICollection<Facility>> GetFacilitiesByLocation(int id);
    }
}
