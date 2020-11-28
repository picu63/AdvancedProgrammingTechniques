using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ClassLibrary.Models;
using MongoDB.Bson;

namespace Multilanguage.Controllers
{
    [ApiController]
    [Route("CarsApi")]
    public class CarsApiController : ControllerBase
    {
        private static readonly Car[] Brands = new[]
        {
            new Car()
            {
                Brand = "Alfa Romeo",
                Model = "159",
                ProductionYear = 2011,
                Languages = new Dictionary<string, string>()
                {
                    {
                        "pl_PL",
                        "Samochód osobowy klasy średniej produkowany przez włoską markę Alfa Romeo w latach 2005 – 201"
                    },
                    {
                        "en_EN",
                        "The Alfa Romeo 159 (Type 939) is a compact executive car produced by Italian automobile manufacturer Alfa Romeo between 2005 and 2011. It was introduced at the 2005 Geneva Motor Show, as a replacement for the 156. The 159 used the GM/Fiat Premium platform, shared with the Alfa Romeo Brera and Spider production cars, and with the Kamal and Visconti concept cars."
                    }
                }
            },
            new Car()
            {
                Brand = "Opel",
                Model = "Meriva",
                ProductionYear = 2010,
                Languages = new Dictionary<string, string>()
                {
                    {
                        "pl_PL",
                        "Najmniejszy z minivanów (segment K) w ofercie Opla, produkowany od 2002 roku." +
                        " Obie generacje Merivy (A i B) zbudowano na bazie modelu Corsa C. Auta dzielą między innymi tę samą platformę podłogową." +
                        " Opel zdecydował się na wprowadzenie Merivy na rynek po sukcesie większego z minivanów marki, Zafiry. W 2005 roku model został zmodernizowany." +
                        " W 2010 roku debiutowała druga generacja Merivy zaprojektowana w stylu znanym wcześniej z modeli Insignia oraz Astra IV. Na innych rynkach auto" +
                        " oferowane jest również jako Vauxhall (Wielka Brytania) lub Chevrolet Meriva (Ameryka Południowa)."
                    },
                    {
                        "en_EN", 
                        "The Opel Meriva is a front-engined, front-wheel-drive five door, five passenger MPV manufactured and marketed by the German automaker Opel on its Corsa" +
                        " platform, from May 2003 until June 2017 across two generations — as a mini MPV in its first generation under the Meriva A nameplate and in its second" +
                        " generation as a compact MPV, the latter as the Meriva B."
                    }
                }
            }, 
            //"Opel", "Ford", "BMW", "Honda", "Lamborghini", "Bentley", "Mazda", "Tesla", "Peugot", "Audi"
        };

        private readonly ILogger<CarsApiController> _logger;

        public CarsApiController(ILogger<CarsApiController> logger)
        {
            _logger = logger;
        }
        //
        [HttpGet("carId={carId}&languageKey={languageKey}")]
        public string GetDescriptionByLanguageKey(string carId, string languageKey)
        {
            var taker = new MongoFeeder.CarTaker();
            var car = taker.GetCar(new ObjectId("5fc20f28ac0e29f8b4fc2756"), "Audi");
            return car.Languages.ToString();
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
