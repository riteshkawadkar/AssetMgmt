using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WMS.Data;

namespace WMS.Contracts
{
    public interface IEquipmentRepository : IRepositoryBase<Equipment>
    {
        Task<ICollection<Equipment>> GetEquipmentsByLine(int id);
    }
}
