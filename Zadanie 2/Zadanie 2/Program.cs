using System;
using System.Collections;
using System.Collections.Generic;
public class Ciasto
{
    public string Nazwa { get; set; }
    public string Rodzaj { get; set; }
    public List<string> Skladniki { get; set; }
}

public interface IFabrykaCiasta
{
    Ciasto StworzCiasto();
}

public class FabrykaCiastaCzekoladowego :
    IFabrykaCiasta
{
    public Ciasto StworzCiasto() => new Ciasto
    {
        Nazwa = "Ciasto czekoladowe",
        Rodzaj = "Kruche",
        Skladniki = new List<string> { "Czekolada", "Mąka", "Jajka", "Masło" }
    };
}

    

public class FabrykaCiastaJablkowego : IFabrykaCiasta
{
    public Ciasto StworzCiasto() => new Ciasto
    {
        Nazwa = "Jabłkowe",
        Rodzaj = "Drożdżowe",
        Skladniki = new List<string> { "Jabłka", "Cynamon", "Mąka", "Cukier" }
    };
}

class PlanPieczenia : IEnumerable<Ciasto>
{
    private List<Ciasto> listaCiast = new();

    public void DodajCiasto(IFabrykaCiasta fabryka) => listaCiast.Add(fabryka.StworzCiasto());

    public void WyswietlPlan()
    {
        foreach (var ciasto in listaCiast)
            Console.WriteLine($"{ciasto.Nazwa} ({ciasto.Rodzaj}): {string.Join(", ", ciasto.Skladniki)}");
    }

    public IEnumerator<Ciasto> GetEnumerator() => listaCiast.GetEnumerator();
    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
}

class PoteznaFabryka
{
    static void Main()
    {
        var plan = new PlanPieczenia();
        plan.DodajCiasto(new FabrykaCiastaCzekoladowego());
        plan.DodajCiasto(new FabrykaCiastaJablkowego());
        
        plan.WyswietlPlan();
    }
}