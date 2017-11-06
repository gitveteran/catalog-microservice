using System.Collections.Generic;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace catalog_microservice.BasicEntityCatalog
{
    public class BasicEntity
    {
        
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        public Dictionary<string , string> Properties { get; set; }

    }
}