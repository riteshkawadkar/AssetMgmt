using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WMS.Contracts;
using WMS.Data;

namespace WMS.Repository
{
    public class LineRepository : ILineRepository
    {
        private readonly ApplicationDbContext _db;

        public LineRepository(ApplicationDbContext db)
        {
            _db = db;
        }

        public async Task<bool> Create(Line entity)
        {
            await _db.Lines.AddAsync(entity);
            return await Save();
        }

        public async Task<bool> Delete(Line entity)
        {
            _db.Lines.Remove(entity);
            return await Save();
        }

        public async Task<IEnumerable<Line>> FindAll()
        {
            var list = await _db.Lines.Include(i => i.Room).ToListAsync();
            return list;
        }

        public async Task<Line> FindById(int id)
        {
            return await _db.Lines.FirstOrDefaultAsync(q => q.Id == id);
        }

        public async Task<ICollection<Line>> GetLinesByRoom(int id)
        {
            var allocations = await FindAll();
            return allocations.Where(q => q.RoomId == id)
                    .ToList();
        }

        public async Task<bool> IsExists(int id)
        {
            var exists = await _db.Lines.AnyAsync(q => q.Id == id);
            return exists;
        }

        public async Task<bool> Save()
        {
            var changes = await _db.SaveChangesAsync();
            return changes > 0;
        }

        public async Task<bool> Update(Line entity)
        {
            _db.Lines.Update(entity);
            return await Save();
        }
    }
}
