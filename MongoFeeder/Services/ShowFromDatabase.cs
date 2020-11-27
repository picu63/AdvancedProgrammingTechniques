using System;
using MongoDB.Bson;
using MongoDB.Driver;

namespace MongoFeeder.Services
{
    public static class ShowFromDatabase
    {
        public static void ShowAllRecords()
        {
            var db = DBConnection.DBConnectionInstance();
            var list = db.Find(new BsonDocument()).ToList().ToJson();
            Console.WriteLine(list);
        }
    }
}
