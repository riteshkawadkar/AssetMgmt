using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WMS.Data;

namespace WMS.Contracts
{
    public interface ISubDepartmentRepository:IRepositoryBase<SubDepartment>
    {
        Task<ICollection<SubDepartment>> GetSubDepartmentsByDept(int id);
    }
}
