using System;
using Microsoft.Extensions.Configuration;
public static class Conf
{
    public static string URL;
    public static string ENV;
    public static string MONGODB_URI;
    public static string MONGODB_NAME;
    public static string MONGODB_COLLECTION;
    static Conf()
    {
        var config = new ConfigurationBuilder()
                .AddEnvironmentVariables("")
                .Build();

        URL = config["ASPNETCORE_URLS"] ?? "http://*:5000";
        ENV = config["ASPNETCORE_ENVIRONMENT"] ?? "Development";
        MONGODB_URI = config["MONGODB_URI"] ?? "mongodb://localhost:27017";
        MONGODB_NAME = config["MONGODB_NAME"] ?? "test";
        MONGODB_COLLECTION = config["MONGODB_COLLECTION"] ?? "gameItems";
    }
}