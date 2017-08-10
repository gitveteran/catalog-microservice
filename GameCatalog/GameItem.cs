using System.Collections.Generic;

namespace catalog_microservice.GameCatalog
{
    public class GameItem
    {
        public string Id { get; set; }
        public string GameName { get; set; }
        public List<string> Properties { get; set; }
    }
}