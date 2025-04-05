using System;

public interface IPaczka
{
    void Przygotuj();
}

public class MałaPaczka : IPaczka
{
    public void Przygotuj()
    {
        Console.WriteLine("Przygotowano małą paczkę.");
    }
}

public class ŚredniaPaczka : IPaczka
{
    public void Przygotuj()
    {
        Console.WriteLine("Przygotowano średnią paczkę.");
    }
}

public class DużaPaczka : IPaczka
{
    public void Przygotuj()
    {
        Console.WriteLine("Przygotowano dużą paczkę.");
    }
}

public class ZarządzaniePaczkami
{
    private IFabrykaPaczek _fabryka;
    private static ZarządzaniePaczkami _instancja;

    private ZarządzaniePaczkami()
    {
    }

    public static ZarządzaniePaczkami Instancja
    {
        get
        {
            if (_instancja == null)
                _instancja = new ZarządzaniePaczkami();
            return _instancja;
        }
    }

    public void UstawFabrykę(IFabrykaPaczek fabryka)
    {
        _fabryka = fabryka;
    }

    public interface IFabrykaPaczek
    {
        IPaczka UtwórzPaczkę();
    }

    public class FabrykaMałychPaczek : IFabrykaPaczek
    {
        public IPaczka UtwórzPaczkę()
        {
            return new MałaPaczka();
        }
    }

    public class FabrykaŚrednichPaczek : IFabrykaPaczek
    {
        public IPaczka UtwórzPaczkę()
        {
            return new ŚredniaPaczka();
        }
    }

    public class FabrykaDużychPaczek : IFabrykaPaczek
    {
        public IPaczka UtwórzPaczkę()
        {
            return new DużaPaczka();
        }
    }


    class Program
    {
        static void Main(string[] args)
        {
            var zarządzanie = ZarządzaniePaczkami.Instancja;

            zarządzanie.UstawFabrykę(new FabrykaMałychPaczek());
            IPaczka paczka1 = new FabrykaMałychPaczek().UtwórzPaczkę();
            paczka1.Przygotuj();

            zarządzanie.UstawFabrykę(new FabrykaŚrednichPaczek());
            IPaczka paczka2 = new FabrykaŚrednichPaczek().UtwórzPaczkę();
            paczka2.Przygotuj();

            zarządzanie.UstawFabrykę(new FabrykaDużychPaczek());
            IPaczka paczka3 = new FabrykaDużychPaczek().UtwórzPaczkę();
            paczka3.Przygotuj();
        }
    }
}