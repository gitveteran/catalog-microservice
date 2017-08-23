using System;
using MongoDB.Bson;
using MongoDB.Driver;

namespace catalog_microservice.GameCatalog
{
    class GameCatalog : IGameCatalog
    {


        public GameItem GetItem(string id)
        {

            return findGameItem(id);
        }

        private GameItem findGameItem(string id)
        {
            var client = new MongoClient("mongodb://localhost:27017");
            var database = client.GetDatabase("test");
            var collection = database.GetCollection<BsonDocument>("gameItems");
            
            var filter = Builders<BsonDocument>.Filter.Eq("_id", ObjectId.Parse(id));

            var document = collection.Find(filter).First();

            Console.WriteLine(document);

            if(document == null) 
            {
                return null;
            }
            
            GameItem returnedItem = new GameItem();

            
            returnedItem.Id = document.GetValue("_id").ToString();
            document.Remove("_id");

            var dResults =  MongoDB.Bson.Serialization.BsonSerializer.Deserialize<System.Collections.Generic.Dictionary<string,string>>(document);

            returnedItem.Properties = dResults;

            //returnedItem.Id = document.GetValue("collectorsNumber").AsString;
            //returnedItem.GameName = document.GetValue("game").AsString;
            
            /* for(var i = 0; i < document.ElementCount; i++)
            {
                returnedItem.Properties.
            } */



            return returnedItem;

            //var document = collection.Find(new BsonDocument()).FirstOrDefault();
            //Console.WriteLine(client.ListDatabases());
        }
    }
}