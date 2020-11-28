using System;
using System.Collections.Generic;
using System.Text;
using MongoDB.Bson;
using MongoFeeder.Services;
using System.Linq;
using ClassLibrary.Models;

namespace MongoFeeder.Services
{
    public class InsertIntoDatabase
    {
        public void InsertCars(List<Car> list)
        {
            var db = DBConnection.DBConnectionInstance();
            List<Car> cars = Cars.GetCars();
            var cars_document = db.GetCollection<BsonDocument>("Cars");
            cars_document.InsertMany(cars.Select(c => c.ToBsonDocument()));
        }
    }

}