using System.Collections.Generic;
using System.Threading.Tasks;
using LF.Tenant.Abstractions.Models;

namespace LF.Tenant.Abstractions
{
    public interface ITenantService
    {
        Task<IReadOnlyCollection<TenantModel>> GetAll();

        Task<TenantModel> Get(string id);

        Task<TenantModel> Register(TenantModel tenant);

        Task<bool> Update(TenantModel tenant);

        Task<bool> Activate(string id);

        Task<bool> Deactivate(string id);
    }
}