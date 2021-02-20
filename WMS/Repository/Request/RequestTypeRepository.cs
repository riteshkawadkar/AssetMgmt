using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using NPOI.OpenXmlFormats.Wordprocessing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WMS.Contracts;
using WMS.Data;
using WMS.Data.Request;

namespace WMS.Repository
{
    public class RequestTypeRepository : IRequestTypeRepository
    {
        private readonly ApplicationDbContext _db;

        public RequestTypeRepository(ApplicationDbContext db)
        {
            _db = db;
        }

        public async Task<bool> Create(RequestType entity)
        {
            await _db.RequestTypes.AddAsync(entity);
            return await Save();
        }

        public async Task<bool> Delete(RequestType entity)
        {
            _db.RequestTypes.Remove(entity);
            return await Save();
        }

        public async Task<IEnumerable<RequestType>> FindAll()
        {
            var list = await _db.RequestTypes
                .ToListAsync();
            return list;
        }

        public async Task<RequestType> FindById(int id)
        {
            return await _db.RequestTypes.FirstOrDefaultAsync(q => q.Id == id);
        }

        public async Task<bool> IsExists(int id)
        {
            var exists = await _db.RequestTypes.AnyAsync(q => q.Id == id);
            return exists;
        }

        public async Task<bool> Save()
        {
            var changes = await _db.SaveChangesAsync();
            return changes > 0;
        }

        public async Task<bool> Update(RequestType entity)
        {
            _db.RequestTypes.Update(entity);
            return await Save();
        }

   

    }
}
