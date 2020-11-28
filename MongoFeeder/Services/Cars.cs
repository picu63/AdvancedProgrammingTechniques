using System;
using System.Collections.Generic;
using System.Text;
using ClassLibrary.Models;

namespace MongoFeeder.Services
{
    public static class Cars
    {
        public static List<Car> GetCars()
        {
            Car bmw = new Car
            {
                Brand = "BMW",
                Model = "e36",
                ProductionYear = 1998,
                Languages = new Dictionary<string, string>
                {
                    {"pl_PL", "Idealny dla dresiarzy"},
                    {"en_US", "Perfect for people walking in sweatpants"},
                    {"de_DE", "Perfekt für Menschen, die in Jogginghosen laufen"},
                    {"zh_CN", "非常適合穿著運動褲的人"}
                }
            };
            Car audi = new Car
            {
                Brand = "Audi",
                Model = "a4",
                ProductionYear = 2007,
                Languages = new Dictionary<string, string>
                {
                    {"pl_PL", "Niemiec płakał jak sprzedawał"},
                    {"en_US", "This is fine"},
                    {"zh_CN", "德國人賣掉時哭了"}
                }
            };
            Car hyundai = new Car
            {
                Brand = "Hyundai",
                Model = "Coupe",
                ProductionYear = 2003,
                Languages = new Dictionary<string, string>
                {
                    {"pl_PL", "Sportowy wygląd ale mało miejsca w środku"},
                    {"en_US", "Sporty look but little space inside"},
                }
            };
            Car opel = new Car
            {
                Brand = "Opel",
                Model = "Corsa",
                ProductionYear = 2005,
                Languages = new Dictionary<string, string>
                {
                    {"en_US", "Just a regular everyday normal car"},
                    {"de_DE", "Nur ein normales normales Alltagsauto"},
                    {"zh_CN", "只是普通的日常普通車"}
                }
            };
            Car renault = new Car
            {
                Brand = "Renault",
                Model = "Megane",
                ProductionYear = 2000,
                Languages = new Dictionary<string, string>
                {
                    {"pl_PL", "Idealny dla rodzin"},
                    {"en_US", "Perfect for family"},
                }
            };
            Car skoda = new Car
            {
                Brand = "Skoda",
                Model = "Fabia",
                ProductionYear = 2001,
                Languages = new Dictionary<string, string>
                {
                    {"pl_PL", "Zawsze o takim marzyłeś"},
                    {"de_DE", "Du hast immer davon geträumt"},
                }
            };
            Car toyota = new Car
            {
                Brand = "Toyota",
                Model = "RAV4",
                ProductionYear = 2008,
                Languages = new Dictionary<string, string>
                {
                    {"zh_CN", "該國最好的地區"},
                    {"en_US", "The best dieles in this part of the country"},
                    {"de_DE", "Die besten Dieles in diesem Teil des Landes"},
                }
            };
            Car mercedes = new Car
            {
                Brand = "Mercedes",
                Model = "Sprinter",
                ProductionYear = 2010,
                Languages = new Dictionary<string, string>
                {
                    {"pl_PL", "Pojemny silnik i nowoczesne technologie"},
                    {"en_US", "Capacious engine and modern technologies"},
                    {"zh_CN", "寬敞的引擎和現代技術"}
                }
            };
            Car bentley = new Car
            {
                Brand = "Bentley",
                Model = "Bentayga",
                ProductionYear = 2013,
                Languages = new Dictionary<string, string>
                {
                    {"zh_CN", "你買不到更好的"},
                }
            };
            Car jeep = new Car
            {
                Brand = "Jeep",
                Model = "Compass",
                ProductionYear = 2009,
                Languages = new Dictionary<string, string>
                {
                    {"pl_PL", "Teren dla niego to pestka"},
                    {"de_DE", "Land ist für ihn ein Kinderspiel"},
                }
            };
            Car ford = new Car
            {
                Brand = "Ford",
                Model = "Edge",
                ProductionYear = 2010,
                Languages = new Dictionary<string, string>
                {
                    {"zh_CN", "驚人的外觀"},
                    {"de_DE", "Erstaunlicher Blick"},
                    {"en_US", "Amazing look"},
                }
            };
            Car mazda = new Car
            {
                Brand = "Mazda",
                Model = "RX-8",
                ProductionYear = 2003,
                Languages = new Dictionary<string, string>
                {
                    {"pl_PL", "Niesamowite auto z silnikiem wankla"},
                    {"de_DE", "Ein tolles Auto mit einem Wankelmotor"},
                }
            };
            Car alfa = new Car
            {
                Brand = "Alfa-Romeo",
                Model = "Giulietta",
                ProductionYear = 2007,
                Languages = new Dictionary<string, string>
                {
                    {"de_DE", "Nie gebrochen"},
                }
            };
            Car dacia = new Car
            {
                Brand = "Dacia",
                Model = "Dokker",
                ProductionYear = 2003,
                Languages = new Dictionary<string, string>
                {
                    {"pl_PL", "Najlepsza oferta dla firm"},
                    {"en_US", "The best offer for companies"},
                    {"de_DE", "Das beste Angebot für Unternehmen"},
                    {"zh_CN", "公司的最佳報價"},
                }
            };
            Car daewoo = new Car
            {
                Brand = "Daewoo",
                Model = "Matiz",
                ProductionYear = 1997,
                Languages = new Dictionary<string, string>
                {
                    {"pl_PL", "Stary poczciwy matiz"},
                }
            };
            Car fiat = new Car
            {
                Brand = "Fiat",
                Model = "Panda",
                ProductionYear = 2005,
                Languages = new Dictionary<string, string>
                {
                    {"en_US", "Do not confuse it with an animal"},
                    {"de_DE", "Verwechseln Sie es nicht mit einem Tier"},
                    {"zh_CN", "不要將它與動物混淆"},
                }
            };
            Car nissan = new Car
            {
                Brand = "Nissan",
                Model = "Juke",
                ProductionYear = 2003,
                Languages = new Dictionary<string, string>
                {
                    {"zh_CN", "這不是一個玩笑"},
                    {"en_US", "It's not a JOKE!"},
                }
            };
            Car peugeot = new Car
            {
                Brand = "Peugeot",
                Model = "Boxer",
                ProductionYear = 2011,
                Languages = new Dictionary<string, string>
                {
                    {"pl_PL", "Zawsze gotowe do akcji"},
                    {"de_DE", "Immer einsatzbereit"},
                }
            };
            Car ferrari = new Car
            {
                Brand = "Ferrari",
                Model = "360",
                ProductionYear = 2002,
                Languages = new Dictionary<string, string>
                {
                    {"en_US", "Faster than light"},
                }
            };

            List<Car> cars = new List<Car>();
            cars.Add(bmw);
            cars.Add(hyundai);
            cars.Add(opel);
            cars.Add(renault);
            cars.Add(skoda);
            cars.Add(ferrari);
            cars.Add(mazda);
            cars.Add(peugeot);
            cars.Add(dacia);
            cars.Add(nissan);
            cars.Add(fiat);
            cars.Add(daewoo);
            cars.Add(alfa);
            cars.Add(ford);
            cars.Add(jeep);
            cars.Add(bentley);
            cars.Add(mercedes);
            cars.Add(toyota);
            return cars;
        }
    }
}
