using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.InteropServices;
using System.Text;
using CsvHelper;
using CsvHelper.Configuration.Attributes;

namespace Scheduler.Models
{
    public class Order
    {
        [Index(0)]
        public int Id { get; set; }
        [Index(1)]
        public string Imie { get; set; }
        [Index(2)]
        public string Nazwisko { get; set; }
        [Index(3)]
        public int NrZamowienia { get; set; }
        [Index(4)]
        public decimal Cena { get; set; }
        [Index(5)]
        public string NumerPaczki { get; set; }
        [Index(6)]
        public decimal Ilosc { get; set; }
        [Index(7)]
        public string AdresZamowienia { get; set; }
        [Index(8)]
        public string email { get; set; }

        public override string ToString()
        {
            return $"{Id},{Imie},{Nazwisko},{NrZamowienia},{Cena},{Ilosc},{AdresZamowienia},{email}";
        }
    }
}
