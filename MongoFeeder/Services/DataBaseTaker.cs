using System;
using MongoDB.Bson;
using MongoDB.Driver;
using System.Collections.Generic;
using ClassLibrary.Models;
using System.Linq;

namespace MongoFeeder.Services
{
    public class DataBaseTaker
    {
        public Car GerCarById(ObjectId id)
        {
            var db = DBConnection.DBConnectionInstance();
            var car = db.GetCollection<Car>("Cars");
            var result = car.AsQueryable().FirstOrDefault(c => c.Id == id);
            return result;
        }
        public List<Car> GetAllCars()
        {
            var db = DBConnection.DBConnectionInstance();
            var cars = db.GetCollection<Car>("Cars");
            var result = cars.AsQueryable().Select(c => new Car()
                {
                    Brand = c.Brand,
                    Languages = c.Languages,
                    Model = c.Model,
                    ProductionYear = c.ProductionYear

                }).ToList();
            return result;
        }
    }
}
