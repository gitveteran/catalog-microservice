namespace catalog_microservice.GameCatalog
{
    using System;
    using Nancy;

    public class GameCatalogModule : NancyModule
    {
        public GameCatalogModule(IGameCatalog gameCatalog) : base("/gameCatalog")
        {
            Get("/fetch/{id}", async parameters =>
            {
                var id = (string)  parameters.id;
                var response = Response.AsJson( await gameCatalog.GetItem(id));
                return response;
                
            });
            Get("/search/{searchTerm}", async parameters => 
            {
                var searchTerm = (string) parameters.searchTerm;
                var response = Response.AsJson( await gameCatalog.SearchItem(searchTerm));
                return response;
            });
        }
    }
}