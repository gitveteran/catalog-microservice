using System.Collections.Generic;
using System.Threading.Tasks;

namespace catalog_microservice.BasicEntityCatalog
{
    public interface IBasicEntityCatalog {
        Task<BasicEntity> GetBasicEntity(string id);
        Task<List<BasicEntity>> SearchBasicEntity(string searchTerm);
    }
}