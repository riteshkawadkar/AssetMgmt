using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WMS.Contracts;
using WMS.Data;

namespace WMS.Repository
{
    public class AreaRepository : IAreaRepository
    {
        private readonly ApplicationDbContext _db;

        public AreaRepository(ApplicationDbContext db)
        {
            _db = db;
        }

        public async Task<bool> Create(DArea entity)
        {
            await _db.DAreas.AddAsync(entity);
            return await Save();
        }

        public async Task<bool> Delete(DArea entity)
        {
             _db.DAreas.Remove(entity);
            return await Save();
        }

        public async Task<IEnumerable<DArea>> FindAll()
        {
            var list = await _db.DAreas
                .Include(i => i.Department).ToListAsync();
            return list;
        }

        public async Task<DArea> FindById(int id)
        {
            return await _db.DAreas.FirstOrDefaultAsync(q => q.Id == id);
        }

        public async Task<ICollection<DArea>> GetAreasByDepartment(int id)
        {
            var allocations = await FindAll();
            return allocations.Where(q => q.DepartmentId == id)
                    .ToList();
     
        }

        public async Task<bool> IsExists(int id)
        {
            var exists = await _db.DAreas.AnyAsync(q => q.Id == id);
            return exists;
        }

        public async Task<bool> Save()
        {
            var changes = await _db.SaveChangesAsync();
            return changes > 0;
        }

        public async Task<bool> Update(DArea entity)
        {
            _db.DAreas.Update(entity);
            return await Save();
        }


    }
}
