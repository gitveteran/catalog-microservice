using System;
using MongoDB.Bson;
using MongoDB.Driver;

namespace catalog_microservice.GameCatalog
{
    class GameCatalog : IGameCatalog
    {


        public GameItem GetItem(string id, string gameName)
        {

            //throw new NotImplementedException();
            connectDB();


            if (id == "1" && gameName == "HeroClix")
            {
                GameItem item1 = new GameItem();
                item1.Id = id;
                item1.GameName = gameName;

                return item1;
            }

            return null;
        }

        private void connectDB()
        {
            var client = new MongoClient("mongodb://localhost:27017");
            var database = client.GetDatabase("test");
            Console.WriteLine(client.ListDatabases());
        }
    }
}