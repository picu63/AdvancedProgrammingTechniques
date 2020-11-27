using System;
using System.Collections.Generic;
using System.Text;
using MongoDB.Bson;
using MongoFeeder.Models;
using MongoFeeder.Services;
using System.Linq;

namespace MongoFeeder.Services
{
    public static class InsertIntoDatabase
    {
        public static void StartProgram()
        {
            MongoDB.Driver.IMongoCollection<MongoDB.Bson.BsonDocument> db = DBConnection.DBConnectionInstance();
            List<Car> cars = Cars.GetCars();
            db.InsertMany(cars.Select(c => c.ToBsonDocument()));
        }
    }

}
