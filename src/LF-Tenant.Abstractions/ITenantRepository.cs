using System.Collections.Generic;
using System.Threading.Tasks;
using LF.Tenant.Abstractions.Models;

namespace LF.Tenant.Abstractions
{
    public interface ITenantRepository
    {
        Task<IReadOnlyCollection<TenantModel>> GetAll();

        Task<TenantModel> Get(string id);

        Task<TenantModel> Create(TenantModel tenant);

        Task<bool> Update(TenantModel tenant);
    }
}