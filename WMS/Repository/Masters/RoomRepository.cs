using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WMS.Contracts;
using WMS.Data;

namespace WMS.Repository
{
    public class RoomRepository : IRoomRepository
    {
        private readonly ApplicationDbContext _db;

        public RoomRepository(ApplicationDbContext db)
        {
            _db = db;
        }

        public async Task<bool> Create(Room entity)
        {
            await _db.Rooms.AddAsync(entity);
            return await Save();
        }

        public async Task<bool> Delete(Room entity)
        {
            _db.Rooms.Remove(entity);
            return await Save();
        }

        public async Task<IEnumerable<Room>> FindAll()
        {
            var list = await _db.Rooms.Include(i => i.Location).ToListAsync();
            return list;
        }

        public async Task<Room> FindById(int id)
        {
            return await _db.Rooms.FirstOrDefaultAsync(q => q.Id == id);
        }

        public async Task<ICollection<Room>> GetRoomsByArea(int id)
        {
            var allocations = await FindAll();
            return allocations.Where(q => q.AreaId == id)
                    .ToList();
        }

        public async Task<bool> IsExists(int id)
        {
            var exists = await _db.Rooms.AnyAsync(q => q.Id == id);
            return exists;
        }

        public async Task<bool> Save()
        {
            var changes = await _db.SaveChangesAsync();
            return changes > 0;
        }

        public async Task<bool> Update(Room entity)
        {
            _db.Rooms.Update(entity);
            return await Save();
        }
    }
}
