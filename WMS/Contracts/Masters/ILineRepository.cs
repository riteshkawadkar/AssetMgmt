using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WMS.Data;

namespace WMS.Contracts
{
    public interface ILineRepository : IRepositoryBase<Line>
    {
        Task<ICollection<Line>> GetLinesByRoom(int id);
    }
}
