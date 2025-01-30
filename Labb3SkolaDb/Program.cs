namespace Labb3SkolaDb
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var run = true;
            while (run)
            {
                Console.WriteLine("[1] Hämta alla elever");
                Console.WriteLine("[2] Hämta alla elever i en klass");
                Console.WriteLine("[3] Lägg till ny personal");
                Console.WriteLine("[4] Avsluta programmet");
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
                        Metoder.AddPersonal();
                        break;
                    case "4":
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
