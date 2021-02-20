using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WMS.Contracts;
using WMS.Data;

namespace WMS.Repository
{
    public class LocationRepository : ILocationRepository
    {
        private readonly ApplicationDbContext _db;

        public LocationRepository(ApplicationDbContext db)
        {
            _db = db;
        }
        public async Task<bool> Create(Location entity)
        {
            await _db.Locations.AddAsync(entity);
            return await Save();
        }

        public async Task<bool> Delete(Location entity)
        {
            _db.Locations.Remove(entity);
            return await Save();
        }

        public async Task<IEnumerable<Location>> FindAll()
        {
            //included comapny as it is a nvaigation property and i tmight be required
            var locations = await _db.Locations.Include(i => i.Company).ToListAsync();
            return locations;
        }

        public async Task<Location> FindById(int id)
        {
            return await _db.Locations.FirstOrDefaultAsync(q => q.Id == id);
        }

        public async Task<ICollection<Location>> GetLocationsByCompany(int companyId)
        {
            var allocations = await FindAll();
            return allocations.Where(q => q.CompanyID == companyId)
                    .ToList();
        }

        public async Task<ICollection<Location>> GetLocationsByDepartment(int id)
        {
            var allocations = await FindAll();
            return allocations.Where(q => q.Id == id)
                    .ToList();
        }

        public async Task<bool> IsExists(int id)
        {
            var exists = await _db.Locations.AnyAsync(q => q.Id == id);
            return exists;
        }

        public async Task<bool> Save()
        {
            var changes = await _db.SaveChangesAsync();
            return changes > 0;
        }

        public async Task<bool> Update(Location entity)
        {
            _db.Locations.Update(entity);
            return await Save();
        }
    }
}
