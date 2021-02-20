using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WMS.Contracts;
using WMS.Data;

namespace WMS.Repository
{
    public class DepartmentRepository : IDepartmentRepository
    {
        private readonly ApplicationDbContext _db;

        public DepartmentRepository(ApplicationDbContext db)
        {
            _db = db;
        }
        public async Task<bool> Create(Department entity)
        {
            await _db.Departments.AddAsync(entity);
            return await Save();
        }

        public async Task<bool> Delete(Department entity)
        {
            _db.Departments.Remove(entity);
            return await Save();
        }

        public async Task<Department> FindById(int id)
        {
            return await _db.Departments.FirstOrDefaultAsync(q => q.Id == id);
        }

        public async Task<ICollection<Department>> GetDepartmentsByLocation(int id)
        {
            var allocations = await FindAll();
            return allocations.Where(q => q.LocationId == id)
                    .ToList();
        }

        public async Task<ICollection<Department>> GetDepartmentsByFacility(int id)
        {
            var allocations = await FindAll();
            return allocations.Where(q => q.FacilityId == id)
                    .ToList();
        }

        public async Task<bool> IsExists(int id)
        {
            var exists = await _db.Departments.AnyAsync(q => q.Id == id);
            return exists;
        }

        public async Task<bool> Save()
        {
            var changes = await _db.SaveChangesAsync();
            return changes > 0;
        }

        public async Task<bool> Update(Department entity)
        {
            _db.Departments.Update(entity);
            return await Save();
        }

        public async Task<IEnumerable<Department>> FindAll()
        {
            var list = await _db.Departments.Include(i => i.Location).Include(f => f.Facility).ToListAsync();
            return list;
        }

        public async Task<ICollection<Department>> GetFilteredDepartmentByDepatmentId(int deptId)
        {
            var depts = await FindAll();
            return depts.Where(q => q.LocationId == deptId)
                    .ToList();
        }
    }
}
