using System.Net;
using System.Threading.Tasks;
using LF.Tenant.Abstractions;
using LF.Tenant.Abstractions.Models;
using Microsoft.AspNetCore.Mvc;

namespace LF_Tenant.Api.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    public class TenantController : Controller
    {
        private readonly ITenantService _tenantService;

        public TenantController(ITenantService tenantService)
        {
            _tenantService = tenantService;
        }
        
        [Route("tenants")]
        [HttpPost]
        [ProducesResponseType((int)HttpStatusCode.Created)]
        public async Task<IActionResult> Register([FromBody]TenantModel model)
        {
          // test comment
            var isValid = !string.IsNullOrWhiteSpace(model.Name)
                          && !string.IsNullOrWhiteSpace(model.Email)
                          && !string.IsNullOrWhiteSpace(model.Phone)
                          && !string.IsNullOrWhiteSpace(model.Website);
            if (!isValid)
                return BadRequest("Invalid Input");

               
            var result = await _tenantService.Register(model);
                
            return CreatedAtAction(nameof(GetById), new { id = model.Id }, null);
            
            
        }
        public async Task<IActionResult> Get()
        {
            var tenants = await _tenantService.GetAll();
            return Ok(tenants);
        }
        
       
        [HttpGet]
        [Route("tenants/{id}")]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(TenantModel),(int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetById(string id)
        {
            if (string.IsNullOrEmpty(id))
                return BadRequest();

            var tenant = await _tenantService.Get(id);
            if (tenant == null)
                return NotFound("Tenant not found");

            return Ok(tenant);
        }
        
       
        
        [Route("tenants")]
        [HttpPut]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType((int)HttpStatusCode.Created)]
        public async Task<IActionResult> Update([FromRoute]string id, [FromBody]TenantModel model)
        {
           

                var isValid = !string.IsNullOrWhiteSpace(model.Name)
                              && !string.IsNullOrWhiteSpace(model.Email)
                              && !string.IsNullOrWhiteSpace(model.Phone)
                              && !string.IsNullOrWhiteSpace(model.Website);
                if (!isValid)
                    return BadRequest("Invalid Input");

                var tenant = await _tenantService.Get(model.Id).ConfigureAwait(false);
                if (tenant == null)
                {
                    return NotFound(new {Message = $"Tenant with id {model.Id} not found."});
                }

                await _tenantService.Update(tenant).ConfigureAwait(false);
                //await EventHub.Publish("TenantUpdated", new TenantUpdated(tenant.Name, tenant));
                return CreatedAtAction(nameof(GetById), new {id = model.Id}, null);
            
        
            
        }
        
       
        [HttpPut("/{id}/activate/")]
        public async Task<IActionResult> Activate([FromRoute]string id)
        {
            if (string.IsNullOrEmpty(id))
                return BadRequest("Invalid Input");

            var tenant = await _tenantService.Get(id).ConfigureAwait(false);
            if (tenant == null)
            {
                return NotFound(new {Message = $"Tenant with id {id} not found."});
            }
            await _tenantService.Activate(id);
           // await EventHub.Publish("TenantActivated", new TenantActivated(id));
            return Ok();
        }
        
        [HttpPut("/{id}/deactivate/")]
        public async Task<IActionResult> Deactivate([FromRoute]string id)
        {
            if (string.IsNullOrEmpty(id))
                return BadRequest("Invalid Input");

            var tenant = await _tenantService.Get(id).ConfigureAwait(false);
            if (tenant == null)
            {
                return NotFound(new {Message = $"Tenant with id {id} not found."});
            }
            await _tenantService.Deactivate(id);
            // await EventHub.Publish("TenantActivated", new TenantActivated(id));
            return Ok();
        }

    }
}