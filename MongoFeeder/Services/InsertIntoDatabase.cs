using System;
using System.Collections.Generic;
using System.Text;
using MongoDB.Bson;
using MongoFeeder.Services;
using System.Linq;
using ClassLibrary.Models;

namespace MongoFeeder.Services
{
    public static class InsertIntoDatabase
    {
        public static void StartProgram()
        {
            var db = DBConnection.DBConnectionInstance();
            List<Car> cars = Cars.GetCars();
            db.InsertMany(cars.Select(c => c.ToBsonDocument()));
        }
    }

}
