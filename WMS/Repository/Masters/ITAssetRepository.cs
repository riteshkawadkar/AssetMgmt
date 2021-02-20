using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WMS.Contracts;
using WMS.Data;

namespace WMS.Repository
{
    public class ITAssetRepository : IITAssestRepository
    {
        private readonly ApplicationDbContext _db;

        public ITAssetRepository(ApplicationDbContext db)
        {
            _db = db;
        }

        public async Task<bool> Create(ITAsset entity)
        {
            await _db.ITAssets.AddAsync(entity);
            return await Save();
        }

        public async Task<bool> Delete(ITAsset entity)
        {
            _db.ITAssets.Remove(entity);
            return await Save();
        }

        public async Task<IEnumerable<ITAsset>> FindAll()
        {
            var list = await _db.ITAssets.Include(i => i.DArea).ToListAsync();
            return list;
        }

        public async Task<ITAsset> FindById(int id)
        {
            return await _db.ITAssets.FirstOrDefaultAsync(q => q.Id == id);
        }

        public async Task<ICollection<ITAsset>> GetITAssetsByArea(int id)
        {
            var allocations = await FindAll();
            return allocations.Where(q => q.AreaId == id)
                    .ToList();
        }

        public async Task<bool> IsExists(int id)
        {
            var exists = await _db.ITAssets.AnyAsync(q => q.Id == id);
            return exists;
        }

   

        public async Task<bool> IsHostnameAvaiable(string hostname)
        {
            return await _db.ITAssets.AnyAsync(x => x.HostName.ToUpper() == hostname.ToUpper());
        }

   

        public async Task<bool> IsIPAddressAvaiable(string ipaddress)
        {
            return await _db.ITAssets.AnyAsync(x => x.IPAddress.ToUpper() == ipaddress.ToUpper());
        }

        public async Task<bool> Save()
        {
            var changes = await _db.SaveChangesAsync();
            return changes > 0;
        }

        public async Task<bool> Update(ITAsset entity)
        {
            _db.ITAssets.Update(entity);
            return await Save();
        }
    }
}
