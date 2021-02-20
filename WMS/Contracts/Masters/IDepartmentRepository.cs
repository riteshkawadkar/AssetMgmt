using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WMS.Data;


namespace WMS.Contracts
{
    public interface IDepartmentRepository:IRepositoryBase<Department>
    {
         Task<ICollection<Department>> GetDepartmentsByLocation(int id);
         Task<ICollection<Department>> GetDepartmentsByFacility(int id);
         Task<ICollection<Department>> GetFilteredDepartmentByDepatmentId(int deptId);



    }
}
