using Nancy;
using Nancy.Configuration;

namespace catalog_microservice
{
    public class CustomBootsrapper : DefaultNancyBootstrapper
    {
        public override void Configure(INancyEnvironment environment)
        {
            environment.Tracing(false, true);
        }
    }
    
}