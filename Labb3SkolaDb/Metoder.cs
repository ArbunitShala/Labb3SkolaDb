using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Labb3SkolaDb.Models;

public class Metoder
{
    public static void GetAllStudents()
    {
        Console.Clear();
        using (var context = new AlmersContext())
        {
            // Användaren får välja hur eleverna ska sorteras och vilken ordning
            Console.WriteLine("Vill du sortera eleverna efter förnamn [1] eller efternamn [2]?");
            var sortOption = Console.ReadLine();
            Console.WriteLine("Ska sorteringen vara stigande [1] eller fallande [2]?");
            var orderOption = Console.ReadLine();

            // skapar en fråga för att hämta elever från databasen
            IQueryable<Elever> eleverQuery = context.Elevers;

            if(sortOption == "1")
            {
                eleverQuery = orderOption == "1"
                    ? eleverQuery.OrderBy(e => e.Förnamn) // stigandes efter förnamn
                    : eleverQuery.OrderByDescending(e => e.Förnamn); //fallandes efter förnamn
            }
            else if (sortOption == "2")
            {
                eleverQuery = orderOption == "1"
                    ? eleverQuery.OrderBy(e => e.Efternamn) //stigandes efter efternamn
                    : eleverQuery.OrderByDescending(e => e.Efternamn); // fallandes efter efternamn
            }
            else
            {
                Console.WriteLine("Ogiltigt val, välj 1 eller 2");
            }

            // frågan utförs och listan av elever hämtas
            var elever = eleverQuery.ToList();

            foreach (var elev in elever)
            {
                Console.WriteLine($"{elev.Förnamn} {elev.Efternamn}");
            }
        }
    }
    public static void GetOneClass()
    {
        Console.Clear();
        using (var context = new AlmersContext())
        {
            // Hämta och visa alla klasser
            var klasser = context.Klassers.ToList();
            Console.WriteLine("Klasser:");
            foreach (var klass in klasser)
            {
                Console.WriteLine($"{klass.Klassid}: {klass.Klassnamn}");
            }

            // Användaren väljer en klass
            Console.WriteLine("Ange KlassID för att visa elever i den klassen:");
            var klassId = int.Parse(Console.ReadLine());

            // Hämta och visa alla elever i den valda klassen
            var elever = context.Elevers.Where(e => e.Klassid == klassId).ToList();
            Console.WriteLine($"Elever i klass {klassId}:");
            foreach (var elev in elever)
            {
                Console.WriteLine($"{elev.Förnamn} {elev.Efternamn}");
            }
        }
    }
    public static void AddPersonal()
    {
        try
        {
            using (var context = new AlmersContext())
            {
                // Fråga om information om den nya personalen
                Console.WriteLine("Ange förnamn:");
                var förnamn = Console.ReadLine();

                Console.WriteLine("Ange efternamn:");
                var efternamn = Console.ReadLine();

                Console.WriteLine("Ange personnummer (12 siffror):");
                var personnummer = Console.ReadLine();

                //kontrollerar om personnummer är 12 siffrigt
                if (personnummer.Length != 12)
                {
                    Console.WriteLine("Personnummer måste vara 12 siffror långt.");
                }

                Console.WriteLine("Roller: " +
                    "[1] Lärare " +
                    "[2] Administratör " +
                    "[3] Rektor " +
                    "[4] Vaktmästare");
                Console.WriteLine("Ange rollID (1-4):");
                var rollID = int.Parse(Console.ReadLine());

                // Kontrollera om rollID 1-4 har valts
                if (rollID < 1 || rollID > 4)
                {
                    Console.WriteLine("Ogiltigt rollID. Välj 1-4");
                }

                // Skapa ett nytt Personal-objekt
                var nyPersonal = new Personal
                {
                    Förnamn = förnamn,
                    Efternamn = efternamn,
                    Personnummer = personnummer,
                    Rollid = rollID
                };

                // Lägg till den nya personalen i databasen
                context.Personals.Add(nyPersonal);
                context.SaveChanges();

                Console.WriteLine("Ny personal har lagts till i databasen.");
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Ett fel uppstod: {ex.Message}");
        }
    }

    public static void GetTeachersPerDepartment()
    {
        Console.Clear();
        using (var context = new AlmersContext())
        {
            var query = context.Klassers
                .Include(k => k.Mentor)
                .Include(k => k.Avdelning)
                .GroupBy(k => k.Avdelning.AvdelningNamn)
                .Select(g => new
                {
                    AvdelningNamn = g.Key,
                    AntalLarare = g.Select(k => k.Mentor).Distinct().Count()
                })
                .ToList();

            foreach (var result in query)
            {
                Console.WriteLine($"{result.AvdelningNamn}: {result.AntalLarare} lärare");
            }
        }
    }

    public static void GetAllStudentsInfo()
    {
        Console.Clear();
        using (var context = new AlmersContext())
        {
            var students = context.Elevers
                .Include(e => e.Klass)
                .Select(e => new
                {
                    e.Förnamn,
                    e.Efternamn,
                    e.Personnummer,
                    KlassNamn = e.Klass.Klassnamn
                })
                .ToList();

            foreach (var student in students)
            {
                Console.WriteLine($"Namn: {student.Förnamn} {student.Efternamn}, Personnummer: {student.Personnummer}, Klass: {student.KlassNamn}");
                Console.WriteLine("-----");
            }
        }
    }

    public static void GetActiveCourses()
    {
        Console.Clear();
        using (var context = new AlmersContext())
        {
            var activeCourses = context.Ämnes
                .Where(a => a.Aktiv == true)
                .Select(a => new
                {
                    a.Ämnenamn
                })
                .ToList();

            foreach (var course in activeCourses)
            {
                Console.WriteLine($"Kurs: {course.Ämnenamn}");
            }
        }
    }

}

