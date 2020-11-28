using System;
using System.Collections.Generic;
using System.Text;
using ClassLibrary.Models;
using MongoFeeder.Services;
using System.Linq;
using MongoDB.Bson;

namespace MongoFeeder
{
    public class CarTaker
    {
        public Car GetCar(ObjectId id, string key)
        {
            var inserter = new InsertIntoDatabase();
            var cars_document = Cars.GetCars();
            inserter.InsertCars(cars_document);
            var getter = new DataBaseTaker();
            var cars = getter.GerRecordByKeyAndId(id, key);
            var myCar = cars.First();
            return myCar;
        }

        public IEnumerable<Car> GetCars()
        {
            var inserter = new InsertIntoDatabase();
            var cars_document = Cars.GetCars();
            inserter.InsertCars(cars_document);
            var getter = new DataBaseTaker();
            var cars = getter.GetAllRecord();
            return cars;
        }
    }
}
