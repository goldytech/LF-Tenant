using MongoDB.Bson.Serialization.Attributes;

namespace LF.Tenant.Abstractions.Models
{
    public class TenantModel
    
    {
        [BsonId]
        public string Id { get; set; }

        public string Name { get; set; }

        public string Email { get; set; }

        public string Phone { get; set; }

        public string Website { get; set; }

        public bool IsActive { get; set; }
    }
}