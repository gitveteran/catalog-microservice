namespace catalog_microservice.GameCatalog
{
    using System;
    using Nancy;

    public class GameCatalogModule : NancyModule
    {


        public GameCatalogModule(IGameCatalog gameCatalog) : base("/gameCatalog")
        {
            //Get("/{id}/{type}", parameters => { return parameters; });
            Get("/{id}/{gameName}", parameters =>
            {
                var id = (string)parameters.id;
                var gameName = (string)parameters.gameName;
                return gameCatalog.GetItem(id, gameName);
            });
        }
    }
}