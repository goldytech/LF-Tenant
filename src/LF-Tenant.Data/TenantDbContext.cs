using LF.Tenant.Abstractions.Models;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace LF.Tenant.Data
{
    public class TenantDbContext
    {
        private readonly IMongoDatabase _database;

        public TenantDbContext(IOptions<Settings> settings)
        {
            var client = new MongoClient(settings.Value.ConnectionString);
            _database = client.GetDatabase(settings.Value.Database);
        }
        
        public IMongoCollection<TenantModel> Tenant => _database.GetCollection<TenantModel>("new-tenant");
    }
    
}