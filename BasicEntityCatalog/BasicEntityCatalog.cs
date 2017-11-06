using System;
using System.Collections.Generic;
using MongoDB.Bson;
using MongoDB.Driver;
using System.Threading.Tasks;

// TO-DO: Replace all the Console.WriteLine statements with some sort of logging.

namespace catalog_microservice.BasicEntityCatalog
{
    class BasicEntityCatalog : IBasicEntityCatalog
    {
        //TO-DO: de-couple DB set-up logic with a separete Client component
        private IMongoClient _client;
        private IMongoDatabase _database;

        public BasicEntityCatalog()
        {
            initializeDB();
        }

        public async Task<BasicEntity> GetBasicEntity(string id)
        {
            return await basicEntityFetch(id);
        }
        public async Task<List<BasicEntity>> SearchBasicEntity(string searchTerm)
        {
            return await basicEntitySearch(searchTerm);   
        }

        private async Task<BasicEntity> basicEntityFetch(string id)
        { 
            var collection = _database.GetCollection<BsonDocument>(Conf.MONGODB_COLLECTION);
            var filter = Builders<BsonDocument>.Filter.Eq("_id", ObjectId.Parse(id));
            var document = await collection.Find(filter).FirstAsync();

            Console.WriteLine(document);

            //TO-DO: Create Policy/Exception handles
            if(document == null) 
            {
                return null;
            }

            return deserializeResult(document);
        } 

        private async Task<List<BasicEntity>> basicEntitySearch(string searchTerm)
        {
            Console.WriteLine("Searching, " + "searchTerm is: " + searchTerm);

            var collection = _database.GetCollection<BsonDocument>(Conf.MONGODB_COLLECTION);
            //TO-DO: find out how to optimize this heavy perforamnce regex (should I add figure names in db as additional lower-case)
            var filter = Builders<BsonDocument>.Filter.Regex("name", new BsonRegularExpression(searchTerm,"i"));
            var cursor = await collection.Find(filter).ToCursorAsync();

            List<BasicEntity> results = new List<BasicEntity>();

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

        private BasicEntity deserializeResult(BsonDocument document)
        {
            BasicEntity returnedItem = new BasicEntity();

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
            Console.WriteLine("Initializing DB... from " + Conf.ENV + " environment.");
            Console.WriteLine("Using MONGODB_URI: " + Conf.MONGODB_URI);
            Console.WriteLine("Using MONGODB_NAME: " + Conf.MONGODB_NAME);
            Console.WriteLine("Using MONGODB_COLLECTION: " + Conf.MONGODB_COLLECTION);

            _client = new MongoClient(Conf.MONGODB_URI);
            _database = _client.GetDatabase(Conf.MONGODB_NAME);       
            
            Console.WriteLine("Cluster Health: " + _client.Cluster.Description.State);

            bool isDBLive = _database.RunCommandAsync((Command<BsonDocument>)"{ping:1}").Wait(7000);
            if(isDBLive)
            {
                Console.WriteLine("Finished! DB Initialized");
                //write further logic, if needed
            }
            else
            {
                Console.WriteLine("Error! database not initialized OR ping did not reach it in time!");
                //Add logging logic here
                System.Environment.Exit(1);
            } 
        }
    }
}