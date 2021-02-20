using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WMS.Data;

namespace WMS.Contracts
{
    public interface IInstrumentRepository : IRepositoryBase<Instrument>
    {
        Task<ICollection<Instrument>> GetInstrumentsByRoom(int id);
    }
}
