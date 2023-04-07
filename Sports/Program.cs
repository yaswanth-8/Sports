
using Microsoft.Data.SqlClient;
using System.Data;

namespace MyProject;
class Program
{
    public static void displaySports()
    {
        string connectionString = "Data Source=DESKTOP-A1NJHOG;Initial Catalog=Sports;Integrated Security=True;Encrypt=False;";
        using (SqlConnection connection = new SqlConnection(connectionString))
        {
            connection.Open();
            string query = "SELECT * FROM Sports";
            using (SqlCommand command = new SqlCommand(query, connection))
            {
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Console.WriteLine(reader[0] + "   " + reader[1]);
                    }
                }
            }
        }
    }
    static void Main(string[] args)
    {
        displaySports();
    }
}