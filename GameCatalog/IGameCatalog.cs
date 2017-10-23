using System.Collections.Generic;
using System.Threading.Tasks;

namespace catalog_microservice.GameCatalog
{
    public interface IGameCatalog
    {
        Task<GameItem> GetItem(string id);
        Task<List<GameItem>> SearchItem(string searchTerm);
    }
}