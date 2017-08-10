using Nancy;
using Nancy.Configuration;

namespace StockPurchaseMicroservice
{
    public class CustomBootsrapper : DefaultNancyBootstrapper
    {
        public override void Configure(INancyEnvironment environment)
        {
            environment.Tracing(false, true);
        }
    }
    
}