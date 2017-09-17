using MongoDB.Bson;
using System.Collections.Generic;

namespace catalog_microservice.GameCatalog
{
    public interface IGameCatalog
    {
        GameItem GetItem(string id);
        List<GameItem> SearchItem(string searchTerm);
    }
}