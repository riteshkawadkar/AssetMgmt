using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WMS.Data;
using WMS.Data.Request;

namespace WMS.Contracts
{
    public interface IMyRequestRepository : IRepositoryBase<MyRequest>
    {
        Task<ICollection<MyRequest>> GetAllRequestsByEmployee(string employeeId);
    }
}
