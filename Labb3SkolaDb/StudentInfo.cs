using Npgsql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Labb3SkolaDb
{
    public class StudentInfo
    {
        public void GetElevInfo()
        {
            Console.Clear();
            try
            {
                Console.Write("Ange elevens ID: ");
                Console.WriteLine();
                if (!int.TryParse(Console.ReadLine(), out int elevID))
                {
                    Console.WriteLine("Ogiltigt ID. Vänligen ange ett giltigt heltal.");
                    return;
                }

                string connectionString = "Host=localhost;Port=5432;Database=almers;Username=postgres;Password=Qorbb98765;";

                using (var connection = new NpgsqlConnection(connectionString))
                {
                    connection.Open();

                    // Kontrollera om elev-ID finns
                    string checkSql = "SELECT COUNT(1) FROM Elever WHERE ElevID = @ElevID;";
                    using (var checkCommand = new NpgsqlCommand(checkSql, connection))
                    {
                        checkCommand.Parameters.AddWithValue("@ElevID", elevID);
                        int count = Convert.ToInt32(checkCommand.ExecuteScalar());
                        if (count == 0)
                        {
                            Console.WriteLine("Ingen elev hittades med angivet ID.");
                            return;
                        }
                    }

                    // anropar sql kommandot
                    string sql = "SELECT * FROM GetElevInfo(@ElevID);";
                    using (var command = new NpgsqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@ElevID", elevID);

                        using (var reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                string elevNamn = reader["ElevNamn"].ToString();
                                string personnummer = reader["Personnummer"].ToString();
                                string klassNamn = reader["KlassNamn"].ToString();
                                string mentor = reader["Mentor"].ToString();
                                string ämne = reader["Ämne"].ToString();
                                string betyg = reader["Betyg"].ToString();
                                string datum = reader["Datum"].ToString();

                                Console.WriteLine($"Elev: {elevNamn}");
                                Console.WriteLine($"Personnummer: {personnummer}");
                                Console.WriteLine($"Klass: {klassNamn}");
                                Console.WriteLine($"Mentor: {mentor}");
                                Console.WriteLine($"Ämne: {ämne}");
                                Console.WriteLine($"Betyg: {betyg} (Datum: {datum})");
                                Console.WriteLine("-------------------------------------");
                            }
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
