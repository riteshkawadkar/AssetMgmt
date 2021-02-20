using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WMS.Contracts;
using WMS.Data;
using WMS.Data.Request;

namespace WMS.Repository
{
    public class MyRequestRepository : IMyRequestRepository
    {
        private readonly ApplicationDbContext _db;

        public MyRequestRepository(ApplicationDbContext db)
        {
            _db = db;
        }

        public async Task<bool> Create(MyRequest entity)
        {
            await _db.MyRequests.AddAsync(entity);
            return await Save();
        }

        public async Task<bool> Delete(MyRequest entity)
        {
             _db.MyRequests.Remove(entity);
            return await Save();
        }

        public async Task<IEnumerable<MyRequest>> FindAll()
        {
            var list = await _db.MyRequests
                        .Include(i => i.RequestingEmployee)
                        .Include(i => i.ApprovedBy)
                        .Include(i => i.RequestType)
                        .ToListAsync();
            return list;
        }

        public async Task<MyRequest> FindById(int id)
        {
            return await _db.MyRequests
                .Include(i => i.RequestingEmployee)
                .Include(i => i.ApprovedBy)
                .Include(i => i.RequestType)
                .FirstOrDefaultAsync(q => q.Id == id);
        }

        public async Task<ICollection<MyRequest>> GetAllRequestsByEmployee(string employeeId)
        {
            var allocations = await FindAll();
            return allocations.Where(q => q.RequestingEmployeeId == employeeId)
                    .ToList();
        }

        public async Task<bool> IsExists(int id)
        {
            var exists = await _db.MyRequests.AnyAsync(q => q.Id == id);
            return exists;
        }

        public async Task<bool> Save()
        {
            var changes = await _db.SaveChangesAsync();
            return changes > 0;
        }

        public async Task<bool> Update(MyRequest entity)
        {
            _db.MyRequests.Update(entity);
            return await Save();
        }

    }
}
