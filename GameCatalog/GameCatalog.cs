using System;
using System.Collections.Generic;
using MongoDB.Bson;
using MongoDB.Driver;


// TO-DO: Replace all the Console.WriteLine statements with some sort of logging.

namespace catalog_microservice.GameCatalog
{
    class GameCatalog : IGameCatalog
    {
        //Setup variables,constants for the database

        //TO-DO: dynamically set-up or discover connection to DB
        //TO-DO: de-couple DB set-up logic with a separete Client component
        private IMongoClient _client;
        private IMongoDatabase _database;
        const string DATABASE = "test";
        const string COLLECTION = "gameItems";
        string CONNECTION = Conf.ENV == "Docker" ? "mongodb://172.17.0.2:27017" : "mongodb://localhost:27017";

        public GameCatalog()
        {
            initializeDB();
        }

        public GameItem GetItem(string id)
        {
            return fetchGameItem(id);
        }
        public List<GameItem> SearchItem(string searchTerm)
        {
            return searchGameItem(searchTerm);   
        }

        private GameItem fetchGameItem(string id)
        { 
            
            var collection = _database.GetCollection<BsonDocument>(COLLECTION);
            var filter = Builders<BsonDocument>.Filter.Eq("_id", ObjectId.Parse(id));
            var document = collection.Find(filter).First();

            Console.WriteLine(document);

            //TO-DO: Create Policy/Exception handles
            if(document == null) 
            {
                return null;
            }

            return deserializeResult(document);
        }

        private List<GameItem> searchGameItem(string searchTerm)
        {
            Console.WriteLine("Searching, " + "searchTerm is: " + searchTerm);

            var collection = _database.GetCollection<BsonDocument>(COLLECTION);
            //TO-DO: find out how to optimize this heavy perforamnce regex (should I add figure names in db as additional lower-case)
            var filter = Builders<BsonDocument>.Filter.Regex("name", new BsonRegularExpression(searchTerm,"i"));
            var cursor = collection.Find(filter).ToCursor();

            List<GameItem> results = new List<GameItem>();

            //TO-DO: Create Policy/Exception handles
            if(collection.Find(filter).Count() > 0)
            {
            foreach(var document in cursor.ToEnumerable())
                {
                    Console.WriteLine(document);

                    results.Add(deserializeResult(document));
                }
            }
            else 
            {
                return null;
            }
            return results;
        }

        private GameItem deserializeResult(BsonDocument document)
        {
            GameItem returnedItem = new GameItem();

            returnedItem.Id = document.GetValue("_id").ToString();
            document.Remove("_id");

            var dResults =  MongoDB.Bson.Serialization.BsonSerializer.
                            Deserialize<System.Collections.Generic.Dictionary<string,string>>(document);

            returnedItem.Properties = dResults;

            return returnedItem;
        }
        private void initializeDB()
        {
            Console.WriteLine("Operating System is : " + Environment.OSVersion.ToString());
            
            Console.WriteLine("Initializing DB...");

            _client = new MongoClient(CONNECTION);
            _database = _client.GetDatabase(DATABASE);
            bool isDBLive = _database.RunCommandAsync((Command<BsonDocument>)"{ping:1}").Wait(1000);
            
            if(isDBLive)
            {
                Console.WriteLine("Finished! DB Initialized");
                //write further logic, if needed
            }
            else
            {
                Console.WriteLine("Error! database not initialized!");
                //Add logging logic here
                System.Environment.Exit(1);
            } 
        }
    }
}