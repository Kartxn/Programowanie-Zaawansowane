using System;
using System.IO;

namespace MediatorStrategiaExample
{
    interface IMediator
    {
        void RealizujOperacje(IOperacjaFinansowa operacja);
        void ZapiszDoPliku(string operacja);
    }

    interface IOperacjaFinansowa
    {
        IMediator Mediator { get; set; }
        void Realizuj();
    }

    interface IWplacalne { }
    interface IWyplacalne { }

    class Bank : IMediator
    {
        private const string FileName = "operacje.txt";

        public void RealizujOperacje(IOperacjaFinansowa operacja)
        {
            operacja.Realizuj();
            ZapiszDoPliku($"Wykonano operację: {operacja.GetType().Name}");
        }

        public void ZapiszDoPliku(string operacja)
        {
            File.AppendAllText(FileName, operacja + Environment.NewLine);
        }
    }

    class Wplata : IOperacjaFinansowa, IWplacalne
    {
        public IMediator Mediator { get; set; }
        private decimal Kwota;

        public Wplata(IMediator mediator, decimal kwota)
        {
            Mediator = mediator;
            Kwota = kwota;
        }

        public void Realizuj()
        {
            Console.WriteLine($"Wykonano operację wpłaty. Kwota: {Kwota}");
        }
    }

    class Wyplata : IOperacjaFinansowa, IWyplacalne
    {
        public IMediator Mediator { get; set; }
        private decimal Kwota;

        public Wyplata(IMediator mediator, decimal kwota)
        {
            Mediator = mediator;
            Kwota = kwota;
        }

        public void Realizuj()
        {
            Console.WriteLine($"Wykonano operację wypłaty. Kwota: {Kwota}");
        }
    }

    interface IPodatekStrategia
    {
        decimal ObliczPodatek(decimal kwota);
    }

    class PodatekPolska : IPodatekStrategia
    {
        public decimal ObliczPodatek(decimal kwota)
        {
            return kwota * 0.23m;
        }
    }

    class PodatekNiemcy : IPodatekStrategia
    {
        public decimal ObliczPodatek(decimal kwota)
        {
            return kwota * 0.19m;
        }
    }

    class KalkulatorPodatku
    {
        private IPodatekStrategia _strategia;

        public KalkulatorPodatku(IPodatekStrategia strategia)
        {
            _strategia = strategia;
        }

        public decimal Oblicz(decimal kwota)
        {
            return _strategia.ObliczPodatek(kwota);
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            IMediator bank = new Bank();

            IOperacjaFinansowa wplata = new Wplata(bank, 1000m);
            IOperacjaFinansowa wyplata = new Wyplata(bank, 500m);

            bank.RealizujOperacje(wplata);
            bank.RealizujOperacje(wyplata);

            Console.WriteLine("\nObliczanie podatków:");

            IPodatekStrategia polskiPodatek = new PodatekPolska();
            KalkulatorPodatku kalkulatorPolska = new KalkulatorPodatku(polskiPodatek);
            Console.WriteLine($"Podatek (PL) od 1000 wynosi: {kalkulatorPolska.Oblicz(1000m)}");

            IPodatekStrategia niemieckiPodatek = new PodatekNiemcy();
            KalkulatorPodatku kalkulatorNiemcy = new KalkulatorPodatku(niemieckiPodatek);
            Console.WriteLine($"Podatek (DE) od 1000 wynosi: {kalkulatorNiemcy.Oblicz(1000m)}");
        }
    }
}