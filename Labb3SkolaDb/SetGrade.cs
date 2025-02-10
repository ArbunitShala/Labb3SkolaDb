using Npgsql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Labb3SkolaDb
{
    public class SetGrade
    {
        public void SetStudentGrade()
        {
            try
            {
                // Fråga användaren om nödvändig  information
                Console.Write("Ange elevens ID: ");
                if (!int.TryParse(Console.ReadLine(), out int elevID))
                {
                    Console.WriteLine("Ogiltigt ID. Ange ett giltigt heltal.");
                    return;
                }

                Console.Write("Ange ämnets ID: ");
                if (!int.TryParse(Console.ReadLine(), out int ämneID))
                {
                    Console.WriteLine("Ogiltigt ID. Ange ett giltigt heltal.");
                    return;
                }

                Console.Write("Ange lärarens ID: ");
                if (!int.TryParse(Console.ReadLine(), out int lärareID))
                {
                    Console.WriteLine("Ogiltigt ID. Ange ett giltigt heltal.");
                    return;
                }

                Console.Write("Ange betyg (A-F): ");
                char betyg = Console.ReadLine().ToUpper()[0];
                if (!"ABCDEF".Contains(betyg))
                {
                    Console.WriteLine("Ogiltigt betyg. Ange en bokstav från A till F.");
                    return;
                }

                Console.Write("Ange datum (åååå-mm-dd): ");
                if (!DateTime.TryParse(Console.ReadLine(), out DateTime datum))
                {
                    Console.WriteLine("Ogiltigt datum. Ange ett giltigt datum i formatet åååå-mm-dd.");
                    return;
                }

                string connectionString = "Host=localhost;Port=5432;Database=almers;Username=postgres;Password=Qorbb98765;";

                using (var connection = new NpgsqlConnection(connectionString))
                {
                    connection.Open();

                    // starta en transaktion
                    using (var transaction = connection.BeginTransaction())
                    {
                        try
                        {
                            // sql kommando för att sätta betyg
                            string sql = @"
                    INSERT INTO Betyg (ElevID, ÄmneID, LärareID, Betyg, Datum)
                    VALUES (@ElevID, @ÄmneID, @LärareID, @Betyg, @Datum);";

                            using (var command = new NpgsqlCommand(sql, connection))
                            {
                                // Lägg till parametrar
                                command.Parameters.AddWithValue("@ElevID", elevID);
                                command.Parameters.AddWithValue("@ÄmneID", ämneID);
                                command.Parameters.AddWithValue("@LärareID", lärareID);
                                command.Parameters.AddWithValue("@Betyg", betyg);
                                command.Parameters.AddWithValue("@Datum", datum);

                                command.ExecuteNonQuery();
                            }

                            // om allt lyckas  bekräftas transaktionen
                            transaction.Commit();
                            Console.WriteLine("Betyget sattes framgångsrikt.");
                        }
                        catch (Exception ex)
                        {
                            // om något går fel, återställ transaktionen
                            transaction.Rollback();
                            Console.WriteLine($"Ett fel inträffade: {ex.Message}. Transaktionen har återställts.");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ett fel inträffade: {ex.Message}");
            }
        }

    }
}
