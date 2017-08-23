namespace catalog_microservice.GameCatalog
{
    using System;
    using Nancy;
    using MongoDB.Bson;

    public class GameCatalogModule : NancyModule
    {


        public GameCatalogModule(IGameCatalog gameCatalog) : base("/gameCatalog")
        {
            //Get("/{id}/{type}", parameters => { return parameters; });
            Get("/{id}", parameters =>
            {
                var id = (string) parameters.id;
                //var gameName = (string)parameters.gameName;
                return gameCatalog.GetItem(id);
            });
        }
    }
}