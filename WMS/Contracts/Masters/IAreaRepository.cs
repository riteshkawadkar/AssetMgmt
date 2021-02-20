using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WMS.Data;

namespace WMS.Contracts
{
    public interface IAreaRepository:IRepositoryBase<DArea>
    {
        Task<ICollection<DArea>> GetAreasByDepartment(int id);
    }
}
