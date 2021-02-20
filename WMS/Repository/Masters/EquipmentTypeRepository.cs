using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WMS.Contracts;
using WMS.Data;

namespace WMS.Repository
{
    public class EquipmentTypeRepository : IEquipmentTypeRepository
    {
        private readonly ApplicationDbContext _db;

        public EquipmentTypeRepository(ApplicationDbContext db)
        {
            _db = db;
        }

        public async Task<bool> Create(EquipmentType entity)
        {
            await _db.EquipmentTypes.AddAsync(entity);
            return await Save();
        }

        public async Task<bool> Delete(EquipmentType entity)
        {
            _db.EquipmentTypes.Remove(entity);
            return await Save();
        }

        public async Task<IEnumerable<EquipmentType>> FindAll()
        {
            var list = await _db.EquipmentTypes.ToListAsync();
            return list;
        }

        public async Task<EquipmentType> FindById(int id)
        {
            return await _db.EquipmentTypes.FirstOrDefaultAsync(q => q.Id == id);
        }

        public async Task<bool> IsExists(int id)
        {
            var exists = await _db.EquipmentTypes.AnyAsync(q => q.Id == id);
            return exists;
        }

        public async Task<bool> Save()
        {
            var changes = await _db.SaveChangesAsync();
            return changes > 0;
        }

        public async Task<bool> Update(EquipmentType entity)
        {
            _db.EquipmentTypes.Update(entity);
            return await Save();
        }

    }
}
