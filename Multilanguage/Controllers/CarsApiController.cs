using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ClassLibrary.Models;
using LanguageProvider;
using MongoDB.Bson;
using MongoDB.Driver.Core.Operations;

namespace Multilanguage.Controllers
{
    [ApiController]
    [Route("CarsApi")]
    public class CarsApiController : ControllerBase
    {
        private readonly GivenLanguageHandler givenLanguage;
        private readonly EnglishLanguageHandler englishLanguage;
        private readonly PolishLanguageHandler polishLanguage;
        private readonly AnyLanguageHandler anyLanguage;

        public CarsApiController()
        {
            givenLanguage = new GivenLanguageHandler();
            englishLanguage = new EnglishLanguageHandler();
            polishLanguage = new PolishLanguageHandler();
            anyLanguage = new AnyLanguageHandler();
            givenLanguage
                .SetNext(englishLanguage)
                .SetNext(polishLanguage)
                .SetNext(anyLanguage);
        }
        [HttpGet("carId={carId}&languageKey={languageKey}")]
        public Car GetDescriptionByLanguageKey(string carId, string languageKey)
        {
            var taker = new MongoFeeder.CarTaker();
            var car = taker.GetCar(new ObjectId(carId), "Audi");

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