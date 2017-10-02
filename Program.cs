using System;
using System.IO;
using Microsoft.AspNetCore.Hosting;


namespace catalog_microservice
{
    public class Program
    {
        public static void Main(string[] args)
        {
            
            var host = new WebHostBuilder()
                .UseKestrel()
                .UseUrls(Conf.URL)
                .UseEnvironment(Conf.ENV)
                .UseContentRoot(Directory.GetCurrentDirectory())
                .UseIISIntegration()
                .UseStartup<Startup>()
                .Build();

            host.Run();
        }
    }
}
