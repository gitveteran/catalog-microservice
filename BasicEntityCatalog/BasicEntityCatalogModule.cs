namespace catalog_microservice.BasicEntityCatalog
{
    using System;
    using Nancy;

    public class BasicEntityCatalogModule : NancyModule
    {
        public BasicEntityCatalogModule(IBasicEntityCatalog basicEntityCatalog) : base("/catalog")
        {
            Get("/fetch/{id}", async parameters =>
            {
                var id = (string)  parameters.id;
                var response = Response.AsJson( await basicEntityCatalog.GetBasicEntity(id));
                return response;
                
            });
            Get("/search/{searchTerm}", async parameters => 
            {
                var searchTerm = (string) parameters.searchTerm;
                var response = Response.AsJson( await basicEntityCatalog.SearchBasicEntity(searchTerm));
                return response;
            });
        }
    }
}