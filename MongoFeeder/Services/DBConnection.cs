using System;
using System.Collections.Generic;
using System.Text;
using ClassLibrary.Models;
using MongoDB.Driver;
using MongoDB.Bson;

namespace MongoFeeder.Services
{
    public static class DBConnection
    {
        public static IMongoDatabase DBConnectionInstance()
        {
            MongoClient client = new MongoClient();
            IMongoDatabase database = client.GetDatabase("MongoCarDB");
            return database;
        }
    }
}
