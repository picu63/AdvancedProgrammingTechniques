using System;
using System.Collections.Generic;
using System.Text;
using MongoDB.Driver;
using MongoDB.Bson;

namespace MongoFeeder.Services
{
    public static class DBConnection
    {
        public static IMongoCollection<BsonDocument> DBConnectionInstance()
        {
            MongoClient client = new MongoClient();
            IMongoDatabase database = client.GetDatabase("MongoCarDB");
            IMongoCollection<BsonDocument> carsDB = database.GetCollection<BsonDocument>("DATABASE_CARS");

            return carsDB;
        }
    }
}
