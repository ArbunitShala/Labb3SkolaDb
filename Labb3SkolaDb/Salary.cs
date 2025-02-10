using Npgsql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Labb3SkolaDb
{
    public class Salary
    {
        public void GetTotalLonPerAvdelning()
        {
            Console.Clear();
            string connectionString = "Host=localhost;Port=5432;Database=almers;Username=postgres;Password=Qorbb98765;";

            using (var connection = new NpgsqlConnection(connectionString))
            {
                connection.Open();

                // sql fråga för att få fram totala lönerna
                string sql = @"
            SELECT av.AvdelningNamn, SUM(p.Lön) AS TotalLön
            FROM Personal p
            JOIN Klasser k ON p.PersonalId = k.MentorID
            JOIN Avdelningar av ON k.AvdelningID = av.AvdelningID
            GROUP BY av.AvdelningNamn;";

                using (var command = new NpgsqlCommand(sql, connection))
                {
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            // värdena hämtas, avdelning och total lön
                            string avdelningNamn = reader["AvdelningNamn"].ToString();
                            decimal totalLon = Convert.ToDecimal(reader["TotalLön"]);

                            Console.WriteLine($"Avdelning: {avdelningNamn} - Totala löner: {totalLon:C}");
                        }
                    }
                }
            }
        }

        public void GetAvgLonPerAvdelning()
        {
            Console.Clear();
            string connectionString = "Host=localhost;Port=5432;Database=almers;Username=postgres;Password=Qorbb98765;";

            using (var connection = new NpgsqlConnection(connectionString))
            {
                connection.Open();

                // sqlfråga, beräknar medellönen för varje avdelning
                string sql = @"
            SELECT av.AvdelningNamn, AVG(p.Lön) AS Medellön
            FROM Personal p
            JOIN Klasser k ON p.PersonalId = k.MentorID
            JOIN Avdelningar av ON k.AvdelningID = av.AvdelningID
            GROUP BY av.AvdelningNamn;";

                using (var command = new NpgsqlCommand(sql, connection))
                {
                    // Utför kommandot och får tillbaka en läsare för resultatet
                    using (var reader = command.ExecuteReader())
                    {
                        // Loopar igenom resultatet
                        while (reader.Read())
                        {
                            // Hämtar värden för avdelningsnamn och medellön
                            string avdelningNamn = reader["AvdelningNamn"].ToString();
                            decimal medellon = Convert.ToDecimal(reader["Medellön"]);
                            Console.WriteLine($"Avdelning: {avdelningNamn} - Medellön: {medellon:C}");
                        }
                    }
                }
            }
        }

    }
}
