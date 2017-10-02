using System;
using Microsoft.Extensions.Configuration;
public static class Conf
{
    public static string URL;
    public static string ENV;
    static Conf()
    {
        var config = new ConfigurationBuilder()
                .AddEnvironmentVariables("")
                .Build();

        URL = config["ASPNETCORE_URLS"] ?? "http://*:5000";
        ENV = config["ASPNETCORE_ENVIRONMENT"] ?? "Development";
    }
}