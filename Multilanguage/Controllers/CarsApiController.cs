using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ClassLibrary.Models;
using LanguageProvider;
using MongoDB.Bson;

namespace Multilanguage.Controllers
{
    [ApiController]
    [Route("CarsApi")]
    public class CarsApiController : ControllerBase
    {
        [HttpGet("carId={carId}&languageKey={languageKey}")]
        public object GetDescriptionByLanguageKey(string carId, string languageKey)
        {
            var taker = new MongoFeeder.CarTaker();
            var car = taker.GetCar(new ObjectId(carId), "Audi");
            var givenLanguage = new GivenLanguageHandler();
            var englishLanguage = new EnglishLanguageHandler();
            var polishLanguage = new PolishLanguageHandler();
            var anyLanguage = new AnyLanguageHandler();
            givenLanguage
                .SetNext(englishLanguage)
                .SetNext(polishLanguage)
                .SetNext(anyLanguage);

            var carDescription = givenLanguage.Handle(car.Languages, languageKey);

            car.Languages = new Dictionary<string, string>(){{carDescription.Key, carDescription.Value}};
            return car;
        }

        [HttpGet]
        public IEnumerable<Car> Get()
        {
            var taker = new MongoFeeder.CarTaker();
            var cars = taker.GetCars();
            return cars;
        }
    }
}