using Labb3SkolaDb.Models;

namespace Labb3SkolaDb
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var run = true;
            var personalInfo = new PersonalInfo();
            var studentScore = new StudentScore();
            var salary = new Salary();
            var studentInfo = new StudentInfo();
            var setGrade = new SetGrade();

            while (run)
            {
                Console.WriteLine("---------------------");
                Console.WriteLine("[1] Hämta alla elever");
                Console.WriteLine("[2] Hämta alla elever i en klass");
                Console.WriteLine("[3] Lägg till ny personal");
                Console.WriteLine("[4] Se antal lärare per avdelning");
                Console.WriteLine();
                Console.WriteLine("[5] Se information om  alla elever");
                Console.WriteLine("[6] Hämta alla aktiva kurser");
                Console.WriteLine("[7] Hämta översikt över all personal");
                Console.WriteLine("[8] Hämta en elevs betyg");
                Console.WriteLine();
                Console.WriteLine("[9] Hämta avdelningarnas totala löner");
                Console.WriteLine("[10] Hämta avdelningarnas medellöner");
                Console.WriteLine("[11] Hämta info om en elev");
                Console.WriteLine("[12] Sätt betyg på en elev");
                Console.WriteLine("[13] Avsluta programmet");
                Console.WriteLine("---------------------");
                var userChoice = Console.ReadLine();

                switch (userChoice)
                {
                    case "1":
                        Metoder.GetAllStudents();
                        break;
                    case "2":
                        Metoder.GetOneClass();
                        break;
                    case "3":
                        Console.Write("Förnamn: ");
                        string förnamn = Console.ReadLine();

                        Console.Write("Efternamn: ");
                        string efternamn = Console.ReadLine();

                        Console.Write("Personnummer (12 siffror): ");
                        string personnummer = Console.ReadLine();

                        Console.Write("RollID: [1] Lärare [2] Administratör [3] Rektor [4] Vaktmästare ");
                        int rollID = int.Parse(Console.ReadLine());

                        Console.Write("Anställningsdatum (yyyy-MM-dd): ");
                        DateTime anstallningsdatum = DateTime.Parse(Console.ReadLine());

                        try
                        {
                            personalInfo.AddNewPersonal(förnamn, efternamn, personnummer, rollID, anstallningsdatum);
                        }
                        catch (ArgumentException ex)
                        {
                            Console.WriteLine($"Fel: {ex.Message}");
                        }
                        break;
                    case "4":
                        Metoder.GetTeachersPerDepartment();
                        break;
                    case "5":
                        Metoder.GetAllStudentsInfo();
                        break;
                    case "6":
                        Metoder.GetActiveCourses();
                        break;
                    case "7":
                        personalInfo.GetAllPersonalOverview();
                        break;
                    case "8":
                        studentScore.GetElevBetyg();
                        break;
                    case "9":
                        salary.GetTotalLonPerAvdelning();
                        break;
                    case "10":
                        salary.GetAvgLonPerAvdelning();
                        break;
                    case "11":
                        studentInfo.GetElevInfo();
                        break;
                    case "12":
                        setGrade.SetStudentGrade();
                        break;
                    case "13":
                        run = false;
                        break;
                    default:
                        Console.WriteLine("Ogiltigt val, välj mellan 1-4");
                        break;
                }
            }
        }
    }
}
