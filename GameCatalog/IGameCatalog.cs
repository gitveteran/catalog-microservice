using MongoDB.Bson;

namespace catalog_microservice.GameCatalog
{
    public interface IGameCatalog
    {
        GameItem GetItem(string id);
    }
}