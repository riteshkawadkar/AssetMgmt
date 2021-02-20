using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WMS.Contracts;
using WMS.Data;

namespace WMS.Repository
{
    public class EquipmentRepository : IEquipmentRepository
    {
        private readonly ApplicationDbContext _db;

        public EquipmentRepository(ApplicationDbContext db)
        {
            _db = db;
        }

        public async Task<bool> Create(Equipment entity)
        {
            await _db.Equipments.AddAsync(entity);
            return await Save();
        }

        public async Task<bool> Delete(Equipment entity)
        {
            _db.Equipments.Remove(entity);
            return await Save();
        }

        public async Task<IEnumerable<Equipment>> FindAll()
        {
            var list = await _db.Equipments.Include(i => i.Line).ToListAsync();
            return list;
        }

        public async Task<Equipment> FindById(int id)
        {
            return await _db.Equipments.FirstOrDefaultAsync(q => q.Id == id);
        }

        public async Task<ICollection<Equipment>> GetEquipmentsByLine(int id)
        {
            var allocations = await FindAll();
            return allocations.Where(q => q.LineId == id)
                    .ToList();
        }

        public async Task<bool> IsExists(int id)
        {
            var exists = await _db.Equipments.AnyAsync(q => q.Id == id);
            return exists;
        }

        public async Task<bool> Save()
        {
            var changes = await _db.SaveChangesAsync();
            return changes > 0;
        }

        public async Task<bool> Update(Equipment entity)
        {
            _db.Equipments.Update(entity);
            return await Save();
        }
    }
}
