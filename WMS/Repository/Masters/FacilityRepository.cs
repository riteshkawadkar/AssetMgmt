using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WMS.Contracts;
using WMS.Data;

namespace WMS.Repository
{
    public class FacilityRepository : IFacilityRepository
    {
        private readonly ApplicationDbContext _db;

        public FacilityRepository(ApplicationDbContext db)
        {
            _db = db;
        }
        public async Task<bool> Create(Facility entity)
        {
            await _db.Facilities.AddAsync(entity);
            return await Save();
        }

        public async Task<bool> Delete(Facility entity)
        {
            _db.Facilities.Remove(entity);
            return await Save();
        }

        public async Task<IEnumerable<Facility>> FindAll()
        {
            //included comapny as it is a nvaigation property and i tmight be required
            var facilities = await _db.Facilities.Include(i => i.Location).ToListAsync();
            return facilities;
        }

        public async Task<Facility> FindById(int id)
        {
            return await _db.Facilities.FirstOrDefaultAsync(q => q.Id == id);
        }

        public async Task<ICollection<Facility>> GetFacilitiesByLocation(int id)
        {
            var allocations = await FindAll();
            return allocations.Where(q => q.LocationId == id)
                    .ToList();
        }

        public async Task<bool> IsExists(int id)
        {
            var exists = await _db.Facilities.AnyAsync(q => q.Id == id);
            return exists;
        }

        public async Task<bool> Save()
        {
            var changes = await _db.SaveChangesAsync();
            return changes > 0;
        }

        public async Task<bool> Update(Facility entity)
        {
            _db.Facilities.Update(entity);
            return await Save();
        }
    }
}
