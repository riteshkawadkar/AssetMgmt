using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WMS.Data;

namespace WMS.Contracts
{
    public interface IRoomRepository : IRepositoryBase<Room>
    {
        Task<ICollection<Room>> GetRoomsByArea(int id);
    }
}
