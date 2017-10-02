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
                return gameCatalog.GetItem(id);
            });
            Get("/search/{searchTerm}", parameters => 
            {
                var searchTerm = (string) parameters.searchTerm;
                return gameCatalog.SearchItem(searchTerm);
            });
        }
    }
}