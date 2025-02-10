using Npgsql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Labb3SkolaDb
{
    public class StudentScore
    {
        public void GetElevBetyg()
        {
            // frågar om elevens personnummer
            Console.Write("Ange elevens personnummer (12 siffrigt): ");
            string elevPersonnummer = Console.ReadLine();

            string connectionString = "Host=localhost;Port=5432;Database=almers;Username=postgres;Password=Qorbb98765;";

            using (var connection = new NpgsqlConnection(connectionString))
            {
                connection.Open();
                // sql kommando
                string sql = @"
            SELECT 
            CONCAT(e.Förnamn, ' ', e.Efternamn) AS Elev,
            äm.ÄmneNamn,
            CONCAT(p.Förnamn, ' ', p.Efternamn) AS Lärare,
            b.Betyg,
            b.Datum
            FROM Betyg b
            JOIN Elever e ON b.ElevID = e.ElevID
            JOIN Ämne äm ON b.ÄmneID = äm.ÄmneID
            JOIN Personal p ON b.LärareID = p.PersonalID
            WHERE e.Personnummer = @Personnummer  -- Filtrerar efter en specifik elev
            ORDER BY äm.ÄmneNamn, b.Datum;";

                using (var command = new NpgsqlCommand(sql, connection))
                {
                    // Lägger till parameter för @Personnummer
                    var param = new NpgsqlParameter("@Personnummer", NpgsqlTypes.NpgsqlDbType.Varchar);
                    param.Value = elevPersonnummer;
                    command.Parameters.Add(param);

                    using (var reader = command.ExecuteReader())
                    {
                        if (!reader.HasRows)
                        {
                            Console.WriteLine("Personnumret finns inte i databasen.");
                            return;
                        }

                        while (reader.Read())
                        {
                            string elev = reader["Elev"].ToString();
                            string ämne = reader["ÄmneNamn"].ToString();
                            string lärare = reader["Lärare"].ToString();
                            string betyg = reader["Betyg"].ToString();
                            DateTime datum = Convert.ToDateTime(reader["Datum"]);

                            Console.WriteLine($"Elev: {elev}  Ämne: {ämne} | Lärare: {lärare} | Betyg: {betyg} | Datum: {datum.ToShortDateString()}");
                            Console.WriteLine("----");
                        }
                    }
                }
            }
        }


    }
}
