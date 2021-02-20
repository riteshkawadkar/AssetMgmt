using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WMS.Contracts;
using WMS.Data;

namespace WMS.Repository
{
    public class CompanyRepository : ICompanyRepository
    {
        private readonly ApplicationDbContext _db;

        public CompanyRepository(ApplicationDbContext db)
        {
            _db = db;
        }
        public async Task<bool> Create(Company entity)
        {
            await _db.Companies.AddAsync(entity);
            return await Save();
        }

        public async Task<bool> Delete(Company entity)
        {
            _db.Companies.Remove(entity);
            return await Save();
        }

        public async Task<IEnumerable<Company>> FindAll()
        {
            var companies = await _db.Companies.ToListAsync();
            return companies;
        }

        public async Task<Company> FindById(int id)
        {
            return await _db.Companies.FirstOrDefaultAsync(q => q.Id == id);
        }

        public async Task<bool> IsExists(int id)
        {
            var exists = await _db.Companies.AnyAsync(q => q.Id == id);
            return exists;
        }

        public async Task<bool> Save()
        {
            var changes = await _db.SaveChangesAsync();
            return changes > 0;
        }

        public async Task<bool> Update(Company entity)
        {
            _db.Companies.Update(entity);
            return await Save();
        }

    }
}
