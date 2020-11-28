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
        public List<Car> GerRecordByKeyAndId(ObjectId id, string key)
        {
            var db = DBConnection.DBConnectionInstance();
            var car = db.GetCollection<Car>("Cars");
            var result = car.AsQueryable().Where(x => x.Id == id).ToList();
            return result;
        }
        public List<Car> GetAllRecord()
        {
            var db = DBConnection.DBConnectionInstance();
            var car = db.GetCollection<Car>("Cars");
            var result = car.AsQueryable().ToList();
            return result;
        }
    }
}
