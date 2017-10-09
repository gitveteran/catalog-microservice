namespace catalog_microservice.GameCatalog
{
    using System;
    using Nancy;

    public class GameCatalogModule : NancyModule
    {
        public GameCatalogModule(IGameCatalog gameCatalog) : base("/gameCatalog")
        {
            Get("/fetch/{id}", parameters =>
            {
                var id = (string) parameters.id;
                return Response.AsJson(gameCatalog.GetItem(id));
            });
            Get("/search/{searchTerm}", parameters => 
            {
                var searchTerm = (string) parameters.searchTerm;
                return Response.AsJson(gameCatalog.SearchItem(searchTerm));
            });
        }
    }
}