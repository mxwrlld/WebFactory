using Microsoft.Data.SqlClient;
using System;

namespace _1._2
{
    class Program
    {
        static string connectionString = "Server=./;Database=Aeroflot;Trusted_Connection=True;";
        static void Main()
        {
            string queryText = String.Empty;
            int companyId;

            // ⦁ Добавление 3 авиакомпаний
            queryText = "INSERT INTO Company(ID_comp, Name) values (1, 'UTair'), (2, 'Aeroflot'), (3, 'RusLine');";
            CUDQuery(queryText);


            // ⦁ Добавление 7 перелетов для разных компаний из Москвы и Курумоча
            queryText = "SELECT ID_comp from Company as c where c.name = 'Aeroflot';";
            companyId = (int)RScalarQuery(queryText);
            queryText = string.Format($"INSERT INTO Trip(trip_no, ID_comp, plane, town_from, town_to, time_out, time_in) values (1, {companyId}, 'А380', 'Москвa', 'Курумоч', '2010-01-01 13:44:00','2010-01-03 7:20:00'), (2, {companyId}, 'Dash-8', 'Москвa', 'Курумоч', '2010-01-01 14:50:00','2010-01-03 8:10:00'), (3, {companyId}, 'SAAB', 'Москвa', 'Курумоч', '2010-01-01 15:00:00','2010-01-03 9:00:00');");
            CUDQuery(queryText);

            queryText = "SELECT ID_comp from Company as c where c.name = 'UTair';";
            companyId = (int)RScalarQuery(queryText);
            queryText = string.Format($"INSERT INTO Trip(trip_no, ID_comp, plane, town_from, town_to, time_out, time_in) values (4, {companyId}, 'SAAB', 'Москвa', 'Курумоч', '2010-01-01 13:44:00','2010-01-03 7:20:00'), (5, {companyId}, 'SAAB', 'Москвa', 'Курумоч', '2010-01-01 14:50:00','2010-01-03 8:10:00'), (6, {companyId}, 'SAAB', 'Москвa', 'Курумоч', '2010-01-01 15:00:00','2010-01-03 9:00:00');");
            CUDQuery(queryText);

            queryText = "SELECT ID_comp from Company as c where c.name = 'RusLine';";
            companyId = (int)RScalarQuery(queryText);
            queryText = string.Format($"INSERT INTO Trip(trip_no, ID_comp, plane, town_from, town_to, time_out, time_in) values (7, {companyId}, 'Dash-8', 'Москвa', 'Курумоч', '2010-01-01 13:44:00','2010-01-03 7:20:00');");
            CUDQuery(queryText);

            // ⦁ Изменение перелетов: измените название города «Курумоч» на «Самара».
            queryText = string.Format(
                $"UPDATE Trip SET town_to = 'Самара' where town_to = 'Курумоч';");
            CUDQuery(queryText);

            // ⦁ Удаление: удалите все рейсы одной из созданных компаний.
            queryText = string.Format(
                $"DELETE FROM Trip where ID_comp = " +
                "(Select ID_comp from Company as c where c.name = 'UTair');");
            CUDQuery(queryText);

        }
        private static object RScalarQuery(string queryText)
        {
            object reader;
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                SqlCommand command = new SqlCommand(queryText, connection);
                reader = command.ExecuteScalar();
            }
            return reader;
        }

        private static void CUDQuery(string queryText)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                SqlCommand command = new SqlCommand(queryText, connection);

                int number = command.ExecuteNonQuery();
                string typeOfOperation = DetermineTypeOfOperation(queryText);
                Console.WriteLine("{0} объектов: {1}", typeOfOperation, number);
            }
        }

        private static string DetermineTypeOfOperation(string query)
        {
            string typeOfOperation = query.Trim().Substring(0, query.IndexOf(' ')).ToLower();
            switch (typeOfOperation)
            {
                case "insert":
                    typeOfOperation = "Добавлено";
                    break;
                case "update":
                    typeOfOperation = "Изменено";
                    break;
                case "delete":
                    typeOfOperation = "Удалено";
                    break;
            }
            return typeOfOperation;
        }
    }
}
