using System.Collections.Generic;
using System.Threading.Tasks;
using LF.Tenant.Abstractions;
using LF.Tenant.Abstractions.Models;

namespace LF.Tenant.Business
{
    public class TenantService :ITenantService
    {
        private readonly ITenantRepository _tenantRepository;

        public TenantService(ITenantRepository tenantRepository)
        {
            _tenantRepository = tenantRepository;
        }
        public async Task<IReadOnlyCollection<TenantModel>> GetAll() => await _tenantRepository.GetAll().ConfigureAwait(false);

        public async Task<TenantModel> Get(string id) => await _tenantRepository.Get(id).ConfigureAwait(false);

        public async Task<TenantModel> Register(TenantModel tenant) => await _tenantRepository.Create(tenant).ConfigureAwait(false);

        public async Task<bool> Update(TenantModel tenant) => await _tenantRepository.Update(tenant).ConfigureAwait(false);

        public async Task<bool> Activate(string id)
        {
            var tenant = await Get(id).ConfigureAwait(false);
            tenant.IsActive = true;
            return await _tenantRepository.Update(tenant).ConfigureAwait(false);
        }

        public async Task<bool> Deactivate(string id)
        {
            var tenant = await Get(id);
            tenant.IsActive = false;
            return await _tenantRepository.Update(tenant).ConfigureAwait(false);
        }
    }
}