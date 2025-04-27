
using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using System.Xml.Serialization;

[Serializable]
public class Osoba
{
    public string Imie { get; set; }
    public string Nazwisko { get; set; }
    public int Wiek { get; set; }
    public DateTime DataUrodzenia { get; set; }
}

public class Student : Osoba
{
    public string NumerIndeksu { get; set; }
    public string NumerGrupy { get; set; }
}

class Program
{
    static void Main(string[] args)
    {
        List<Osoba> osoby = new List<Osoba>
        {
            new Osoba { Imie = "Jan", Nazwisko = "Kowalski", Wiek = 25, DataUrodzenia = new DateTime(2000,1,15) },
            new Osoba { Imie = "Anna", Nazwisko = "Nowak", Wiek = 30, DataUrodzenia = new DateTime(1994,5,20) },
            new Osoba { Imie = "Pawel", Nazwisko = "Malinowski", Wiek = 28, DataUrodzenia = new DateTime(1996,7,12) },
            new Osoba { Imie = "Katarzyna", Nazwisko = "Kaczmarek", Wiek = 26, DataUrodzenia = new DateTime(1998,9,3) }
        };

        List<Student> studenci = new List<Student>
        {
            new Student { Imie = "Piotr", Nazwisko = "Zielinski", Wiek = 22, DataUrodzenia = new DateTime(2002,2,10), NumerIndeksu = "S123", NumerGrupy = "G1" },
            new Student { Imie = "Maria", Nazwisko = "Wisniewska", Wiek = 23, DataUrodzenia = new DateTime(2001,3,5), NumerIndeksu = "S456", NumerGrupy = "G2" },
            new Student { Imie = "Lukasz", Nazwisko = "Duda", Wiek = 21, DataUrodzenia = new DateTime(2004,4,1), NumerIndeksu = "S789", NumerGrupy = "G3" },
            new Student { Imie = "Agnieszka", Nazwisko = "Piotrowska", Wiek = 24, DataUrodzenia = new DateTime(2001,11,29), NumerIndeksu = "S012", NumerGrupy = "G4" }
        };

        XmlSerializer xmlOsoba = new XmlSerializer(typeof(List<Osoba>));
        using (FileStream fs = new FileStream("osoby.xml", FileMode.Create))
            xmlOsoba.Serialize(fs, osoby);

        XmlSerializer xmlStudent = new XmlSerializer(typeof(List<Student>));
        using (FileStream fs = new FileStream("studenci.xml", FileMode.Create))
            xmlStudent.Serialize(fs, studenci);

        using (FileStream fs = new FileStream("osoby.json", FileMode.Create))
            JsonSerializer.Serialize(fs, osoby, new JsonSerializerOptions { WriteIndented = true });
        
        using (FileStream fs = new FileStream("studenci.json", FileMode.Create))
            JsonSerializer.Serialize(fs, studenci, new JsonSerializerOptions { WriteIndented = true });

        List<Osoba> osobyXml;
        using (FileStream fs = new FileStream("osoby.xml", FileMode.Open))
            osobyXml = (List<Osoba>)xmlOsoba.Deserialize(fs);

        List<Student> studenciXml;
        using (FileStream fs = new FileStream("studenci.xml", FileMode.Open))
            studenciXml = (List<Student>)xmlStudent.Deserialize(fs);

        List<Osoba> osobyJson;
        using (FileStream fs = new FileStream("osoby.json", FileMode.Open))
            osobyJson = JsonSerializer.Deserialize<List<Osoba>>(fs);

        List<Student> studenciJson;
        using (FileStream fs = new FileStream("studenci.json", FileMode.Open))
            studenciJson = JsonSerializer.Deserialize<List<Student>>(fs);

        
        Console.WriteLine("Osoby (XML):");
        foreach (var o in osobyXml)
            Console.WriteLine($"{o.Imie} {o.Nazwisko}, Wiek: {o.Wiek}, Data: {o.DataUrodzenia:d}");
        
        Console.WriteLine("Studenci (XML):");
        foreach (var s in studenciXml)
            Console.WriteLine($"{s.Imie} {s.Nazwisko}, Indeks: {s.NumerIndeksu}, Grupa: {s.NumerGrupy}");

        Console.WriteLine("Osoby (JSON):");
        foreach (var o in osobyJson)
            Console.WriteLine($"{o.Imie} {o.Nazwisko}, Wiek: {o.Wiek}, Data: {o.DataUrodzenia:d}");
        
        Console.WriteLine("Studenci (JSON):");
        foreach (var s in studenciJson)
            Console.WriteLine($"{s.Imie} {s.Nazwisko}, Indeks: {s.NumerIndeksu}, Grupa: {s.NumerGrupy}");
    }
}

/*
1.
Gdy klasa korzysta z zasobów niezarządzanych, które nie są zwalniane automatycznie przez garbage collector.
Metodę Dispose() można przeciążać, implementując wzorzec Dispose, aby w razie potrzeby rozróżnić zwalnianie zasobów zarządzanych i niezarządzanych oraz umożliwić wywołanie zarówno z finalizatora, jak i jawnie.

2.
a) Program z klasą fllikLoggera implementującą IDisposable jest poprawny. Użycie instrukcji using gwarantuje wywołanie Dispose(), a tym samym zamknięcie strumienia.
b) Program z klasą FileLogger również działa, ale ręczne wywoływanie CloseFile() jest bardziej podatne na wycieki zasobów w przypadku wyjątków. Lepszą praktyką jest implementacja IDisposable.
*/