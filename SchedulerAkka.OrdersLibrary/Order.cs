using System;

namespace SchedulerAkka.OrdersLibrary
{
    public class Order
    {
        public int Id { get; set; }
        public string Imie { get; set; }
        public string Nazwisko { get; set; }
        public int NrZamowienia { get; set; }
        public decimal Cena { get; set; }
        public string NumerPaczki { get; set; }
        public decimal Ilosc { get; set; }
        public string AdresZamowienia { get; set; }
        public string Email { get; set; }

        public override string ToString()
        {
            return $"{Id},{Imie},{Nazwisko},{NrZamowienia},{Cena},{Ilosc},{AdresZamowienia},{Email}";
        }
    }
}