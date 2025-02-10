using System;
using System.Collections.Generic;
using Microsoft.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Npgsql;

namespace Labb3SkolaDb.Models
{
    public class PersonalInfo
    {
        public void GetAllPersonalOverview()
        {
            Console.Clear();
            string connectionString = "Host=localhost;Port=5432;Database=almers;Username=postgres;Password=Qorbb98765;";

            // skapar anslutning till databasen
            using (var connection = new NpgsqlConnection(connectionString))
            {
                connection.Open();

                // Sql kommando som ska hämta personalinfon
                string sql = @"
            SELECT p.Förnamn, p.Efternamn, r.RollNamn, DATE_PART('year', AGE(p.Anstallningsdatum)) AS AntalAr
            FROM Personal p
            JOIN Roller r ON p.RollID = r.RollID;";

                using (var command = new NpgsqlCommand(sql, connection))
                {
                    // Kommando utförs, får läsare tillbaka
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            // hämtar värden för varje kolumn i raden
                            string förnamn = reader["Förnamn"].ToString();
                            string efternamn = reader["Efternamn"].ToString();
                            string rollNamn = reader["RollNamn"].ToString();
                            int antalAr = Convert.ToInt32(reader["AntalAr"]);

                            Console.WriteLine($"{förnamn} {efternamn}, Befattning: {rollNamn}, Antal år på skolan: {antalAr}");
                            Console.WriteLine("--------------");
                        }
                    }
                }
            }
        }

        public void AddNewPersonal(string förnamn, string efternamn, string personnummer, int rollID, DateTime anstallningsdatum)
        {
            Console.Clear();
            // Kontrollerar om personnummer är 12 siffror
            if (personnummer.Length != 12 || !personnummer.All(char.IsDigit))
            {
                Console.WriteLine("Personnumret måste vara exakt 12 siffror.");
                return; // Avbryts om personnummer är ogiltigt
            }

            // Kontrollerar om rollID är inom  1-4
            if (rollID < 1 || rollID > 4)
            {
                Console.WriteLine("RollID måste vara mellan 1 och 4.");
                return; // Avbryts om rollID är ogiltigt
            }
            string connectionString = "Host=localhost;Port=5432;Database=almers;Username=postgres;Password=Qorbb98765;";

            using (var connection = new NpgsqlConnection(connectionString))
            {
                connection.Open();

                // sql kommandot som ska köras
                string sql = @"
            INSERT INTO Personal (Förnamn, Efternamn, Personnummer, RollID, Anstallningsdatum)
            VALUES (@Förnamn, @Efternamn, @Personnummer, @RollID, @Anstallningsdatum);";

                // skapar kommando
                using (var command = new NpgsqlCommand(sql, connection))
                {
                    // Lägger till parametrar
                    command.Parameters.AddWithValue("@Förnamn", förnamn);
                    command.Parameters.AddWithValue("@Efternamn", efternamn);
                    command.Parameters.AddWithValue("@Personnummer", personnummer);
                    command.Parameters.AddWithValue("@RollID", rollID);
                    command.Parameters.AddWithValue("@Anstallningsdatum", anstallningsdatum);

                    int rowsAffected = command.ExecuteNonQuery();
                    Console.WriteLine("Personal lades till!");
                    Console.WriteLine($"Antal rader som påverkades: {rowsAffected}");
                }
            }
        }

    }
}
