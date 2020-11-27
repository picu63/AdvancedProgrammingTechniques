using MongoFeeder.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace MongoFeeder.Services
{
    public static class Cars
    {
        public static List<Car> GetCars()
        {
            Car bmw = new Car
            {
                Mark = "BMW",
                Model = "e36",
                ProductionYear = 1998,
                Languages = new Dictionary<string, string>
                {
                    {"PL", "Kupa złomu"},
                    {"EN", "Piece of shit"},
                    {"DE", "Szmeterling"}
                }
            };

            Car audi = new Car
            {
                Mark = "Audi",
                Model = "a4",
                ProductionYear = 2007,
                Languages = new Dictionary<string, string>
                {
                    {"PL", "Jest ok"},
                    {"EN", "This is fine"},
                    {"DE", "szmeterling"}
                }
            };

            Car hyundai = new Car
            {
                Mark = "Hyundai",
                Model = "Coupe",
                ProductionYear = 2003,
                Languages = new Dictionary<string, string>
                {
                    {"PL", "Wykurwisty samochód"},
                    {"EN", "Very good car"},
                    {"DE", "Szmeterling1"}
                }
            };

            List<Car> cars = new List<Car>();
            cars.Add(bmw);
            cars.Add(hyundai);
            cars.Add(audi);
            return cars;
        }
    }

}
