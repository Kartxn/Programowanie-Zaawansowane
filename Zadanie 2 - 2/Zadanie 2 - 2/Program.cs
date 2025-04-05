using System;
public interface IPaczka
{
    void Spakuj();
}

public interface IKurier
{
    void Dostarcz();
}


public class MałaPaczka : IPaczka
{
    public void Spakuj()
    {
        Console.WriteLine("Spakowano małą paczkę.");
    }
}

public class DużaPaczka : IPaczka
{
    public void Spakuj()
    {
        Console.WriteLine("Spakowano dużą paczkę.");
    }
}

public class DHLKurier : IKurier
{
    public void Dostarcz()
    {
        Console.WriteLine("Dostarczono przez kuriera DHL.");
    }
}

public class UPSKurier : IKurier
{
    public void Dostarcz()
    {
        Console.WriteLine("Dostarczono przez kuriera UPS.");
    }
}


public interface IFabrykaLogistyki
{
    IPaczka UtwórzPaczkę();
    IKurier UtwórzKuriera();
}


public class FabrykaLogistykiPolska : IFabrykaLogistyki
{
    public IPaczka UtwórzPaczkę() => new MałaPaczka();
    public IKurier UtwórzKuriera() => new DHLKurier();
}

public class FabrykaLogistykiUSA : IFabrykaLogistyki
{
    public IPaczka UtwórzPaczkę() => new DużaPaczka();
    public IKurier UtwórzKuriera() => new UPSKurier();
}


public class ZarządzaniePrzesyłkami
{
    private IFabrykaLogistyki _fabryka;
    private static ZarządzaniePrzesyłkami _instancja;

    private ZarządzaniePrzesyłkami() { }

    public static ZarządzaniePrzesyłkami Instancja
    {
        get
        {
            if (_instancja == null)
                _instancja = new ZarządzaniePrzesyłkami();
            return _instancja;
        }
    }

    public void PrzyjmijZamówienie(string lokalizacja)
    {
        switch (lokalizacja.ToLower())
        {
            case "polska":
                _fabryka = new FabrykaLogistykiPolska();
                break;
            case "usa":
                _fabryka = new FabrykaLogistykiUSA();
                break;
            default:
                throw new ArgumentException("Nieobsługiwana lokalizacja.");
        }

        var paczka = _fabryka.UtwórzPaczkę();
        var kurier = _fabryka.UtwórzKuriera();
        paczka.Spakuj();
        kurier.Dostarcz();
    }
}

class Program
{
    static void Main() 
    {
        var zarządzanie = ZarządzaniePrzesyłkami.Instancja;

        Console.WriteLine("[ Zamówienie z Polski ]");
        zarządzanie.PrzyjmijZamówienie("Polska");

        Console.WriteLine("\n[ Zamówienie z USA ]");
        zarządzanie.PrzyjmijZamówienie("USA");
    }
}