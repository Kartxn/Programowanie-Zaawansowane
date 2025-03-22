
public interface INPC
{
    void Przedstawsie();
}

public interface IFabrykaNPC
{
    INPC CreateNPC();
}

public class Wojownik : INPC
{
    public void Przedstawsie()
    {
        Console.WriteLine("Jestem Wojownikiem, walczę mieczem");
    }
}

public class Mag : INPC
{
    public void Przedstawsie()
    {
        Console.WriteLine("Jestem Magiem, władającym magią żywiołów");
    }
}

public class Złodziej : INPC
{
    public void Przedstawsie()
    {
        Console.WriteLine("Jestem Złodziejem, nie mam atrybutów");
    }
}

public class FabrykaWojownika : IFabrykaNPC
{
    public INPC CreateNPC() => new Wojownik();
}

public class FabrykaMaga : IFabrykaNPC
{
    public INPC CreateNPC() => new Mag();
}

public class FabrykaZlodzieja : IFabrykaNPC
{
    public INPC CreateNPC() => new Złodziej();
}

public class Gra
{
    static void Main()
    {
        List<IFabrykaNPC> fabryki = new List<IFabrykaNPC>
        {
            new FabrykaWojownika(),
            new FabrykaMaga(),
            new FabrykaZlodzieja()
        };

        Random losuj = new Random();
        int wybranaFabryka = losuj.Next(fabryki.Count);
        
        INPC npc = fabryki[wybranaFabryka].CreateNPC();
        npc.Przedstawsie();
    }
}