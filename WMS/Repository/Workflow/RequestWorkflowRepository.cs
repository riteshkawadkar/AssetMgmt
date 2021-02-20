using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WMS.Contracts.Workflow;
using WMS.Data;
using WMS.Data.Workflow;

namespace WMS.Repository.Workflow
{
    public class RequestWorkflowRepository : IRequestWorfklowRepository
    {
        private readonly ApplicationDbContext _db;

        public RequestWorkflowRepository(ApplicationDbContext db)
        {
            _db = db;
        }

        public async Task<bool> Create(RequestWorkflow entity)
        {
            await _db.RequestWorkflows.AddAsync(entity);
            return await Save();
        }

        public async Task<bool> Delete(RequestWorkflow entity)
        {
            _db.RequestWorkflows.Remove(entity);
            return await Save();
        }

        public async Task<IEnumerable<RequestWorkflow>> FindAll()
        {
            var list = await _db.RequestWorkflows
                        .Include(i => i.WorkflowGrantAccess)
                        .Include(i => i.WorkflowPasswordChange)
                        .ToListAsync();
            return list;
        }

        public async Task<RequestWorkflow> FindById(int id)
        {
            return await _db.RequestWorkflows
                .Include(i => i.WorkflowGrantAccess)
                .Include(i => i.WorkflowPasswordChange)
                .FirstOrDefaultAsync(q => q.Id == id);
        }

        public async Task<bool> IsExists(int id)
        {
            var exists = await _db.RequestWorkflows.AnyAsync(q => q.Id == id);
            return exists;
        }

        public async Task<bool> Save()
        {
            var changes = await _db.SaveChangesAsync();
            return changes > 0;
        }

        public async Task<bool> Update(RequestWorkflow entity)
        {
            _db.RequestWorkflows.Update(entity);
            return await Save();
        }
    }
}
