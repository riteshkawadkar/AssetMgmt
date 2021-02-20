using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WMS.Contracts;
using WMS.Data;

namespace WMS.Repository
{
    public class InstrumentRepository : IInstrumentRepository
    {
        private readonly ApplicationDbContext _db;

        public InstrumentRepository(ApplicationDbContext db)
        {
            _db = db;
        }

        public async Task<bool> Create(Instrument entity)
        {
            await _db.Instruments.AddAsync(entity);
            return await Save();
        }

        public async Task<bool> Delete(Instrument entity)
        {
            _db.Instruments.Remove(entity);
            return await Save();
        }

        public async Task<IEnumerable<Instrument>> FindAll()
        {
            var list = await _db.Instruments.Include(i => i.Line).ToListAsync();
            return list;
        }

        public async Task<Instrument> FindById(int id)
        {
            return await _db.Instruments.FirstOrDefaultAsync(q => q.Id == id);
        }

        public async Task<ICollection<Instrument>> GetInstrumentsByLine(int id)
        {
            var allocations = await FindAll();
            return allocations.Where(q => q.LineId == id)
                    .ToList();
        }

        public async Task<ICollection<Instrument>> GetInstrumentsByRoom(int id)
        {
            var allocations = await FindAll();
            return allocations.Where(q => q.RoomId == id)
                    .ToList();
        }

        public async Task<bool> IsExists(int id)
        {
            var exists = await _db.Instruments.AnyAsync(q => q.Id == id);
            return exists;
        }

        public async Task<bool> Save()
        {
            var changes = await _db.SaveChangesAsync();
            return changes > 0;
        }

        public async Task<bool> Update(Instrument entity)
        {
            _db.Instruments.Update(entity);
            return await Save();
        }
    }
}
