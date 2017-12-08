using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using LF.Tenant.Abstractions;
using LF.Tenant.Abstractions.Models;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace LF.Tenant.Data
{
    public class TenantRepository :ITenantRepository
    {
        private readonly TenantDbContext _tenantDbContext;
        public TenantRepository(IOptions<Settings> settings)
        {
            _tenantDbContext = new TenantDbContext(settings);
        }
        public async Task<IReadOnlyCollection<TenantModel>> GetAll()
        {
            var cursor = await _tenantDbContext.Tenant.FindAsync(_ => true);
            var result = new ReadOnlyCollection<TenantModel>(cursor.ToList());
            cursor.Dispose();
            return result;
        }

        public async Task<TenantModel> Get(string id)
        {
            var cursor = await _tenantDbContext.Tenant.FindAsync(t => t.Id == id);
            var result = cursor.FirstOrDefault();
            cursor.Dispose();
            return result;
        }

        public async Task<TenantModel> Create(TenantModel tenant)
        {
            await _tenantDbContext.Tenant.InsertOneAsync(tenant);
            return tenant;
        }

        public async Task<bool> Update(TenantModel tenant)
        {
           var result = await _tenantDbContext.Tenant.ReplaceOneAsync(t => t.Id == tenant.Id, tenant);
           return result.IsAcknowledged && result.MatchedCount == result.ModifiedCount && result.ModifiedCount == 1;
        }
    }
}