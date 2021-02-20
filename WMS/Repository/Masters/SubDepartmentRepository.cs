using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WMS.Contracts;
using WMS.Data;

namespace WMS.Repository
{
    public class SubDepartmentRepository : ISubDepartmentRepository
    {
        private readonly ApplicationDbContext _db;

        public SubDepartmentRepository(ApplicationDbContext db)
        {
            _db = db;
        }
        public async Task<bool> Create(SubDepartment entity)
        {
            await _db.SubDepartments.AddAsync(entity);
            return await Save();
        }

        public async Task<bool> Delete(SubDepartment entity)
        {
            _db.SubDepartments.Remove(entity);
            return await Save();
        }

        public async Task<IEnumerable<SubDepartment>> FindAll()
        {
            var list = await _db.SubDepartments
                .Include(i => i.Department)
                .ToListAsync();
            return list;
            
        }



        public async Task<SubDepartment> FindById(int id)
        {
            return await _db.SubDepartments.FirstOrDefaultAsync(q => q.Id == id);
        }


        public async Task<ICollection<SubDepartment>> GetSubDepartmentsByDept(int id)
        {
            var allocations = await FindAll();
            return allocations.Where(q => q.DepartmentId == id)
                    .ToList();
        }

        public async Task<bool> IsExists(int id)
        {
            var exists = await _db.SubDepartments.AnyAsync(q => q.Id == id);
            return exists;
        }

        public async Task<bool> Save()
        {
            var changes = await _db.SaveChangesAsync();
            return changes > 0;
        }

        public async Task<bool> Update(SubDepartment entity)
        {
            _db.SubDepartments.Update(entity);
            return await Save();
        }


    }
}
