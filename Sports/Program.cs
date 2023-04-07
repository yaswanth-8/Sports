
using Microsoft.Data.SqlClient;
using System.Data;
using System.Xml.Linq;

namespace MyProject;
class Program
{
    public static int S_Id = 2;
    public static int T_Id = 1;
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
        //displaySports();
        //AddSport();
        //AddTournament();
        //removeSport();
        removeTournament();




    }

    public static void removeTournament()
    {
        Console.Write("Tournament ID to be removed from the system : ");
        int TournamentID = int.Parse(Console.ReadLine());
        string connectionString = "Data Source=DESKTOP-A1NJHOG;Initial Catalog=Sports;Integrated Security=True;Encrypt=False;";
        using (SqlConnection connection = new SqlConnection(connectionString))
        {
            connection.Open();
            string query1 = $"DELETE FROM League WHERE T_Id={TournamentID}";
            using (SqlCommand command1 = new SqlCommand(query1, connection))
            {
                command1.ExecuteNonQuery();
            }
            string query2 = $"DELETE FROM Tournament WHERE T_Id={TournamentID}";
            using (SqlCommand command2 = new SqlCommand(query2, connection))
            {
                command2.ExecuteNonQuery();
            }
        }
    }


    public static void removeSport()
    {
        displaySports();
        Console.Write("Choose the sport ID to be removed from the system : ");
        int sportId = int.Parse(Console.ReadLine());
        string connectionString = "Data Source=DESKTOP-A1NJHOG;Initial Catalog=Sports;Integrated Security=True;Encrypt=False;";
        using (SqlConnection connection = new SqlConnection(connectionString))
        {
            connection.Open();
            string query1 = $"DELETE FROM League WHERE S_Id={sportId}";
            using (SqlCommand command1 = new SqlCommand(query1, connection))
            {
                command1.ExecuteNonQuery();
            }
            string query2 = $"DELETE FROM Sports WHERE S_Id={sportId}";
            using (SqlCommand command2 = new SqlCommand(query2, connection))
            {
                command2.ExecuteNonQuery();
            }
        }
    }

    public static void AddTournament()
    {
        Console.Write("Enter the Tournament Name to be added : ");
        String T_Name = Console.ReadLine();
        AddSportToTournament();
        string connectionString = "Data Source=DESKTOP-A1NJHOG;Initial Catalog=Sports;Integrated Security=True;Encrypt=False;";
        using (SqlConnection connection = new SqlConnection(connectionString))
        {
            connection.Open();
            string query = $"INSERT INTO Tournament (T_Id, T_Name) VALUES ({T_Id++}, '{T_Name}');";
            using (SqlCommand command = new SqlCommand(query, connection))
            {
                command.ExecuteNonQuery();
            }
        }
    }

    public static void AddSportToTournament()
    {
        bool itr = true;
        while (itr)
        {
            displaySports();
            Console.Write("Choose the sport ID to be added to the tournament : ");
            int sportId = int.Parse(Console.ReadLine());
            string connectionString = "Data Source=DESKTOP-A1NJHOG;Initial Catalog=Sports;Integrated Security=True;Encrypt=False;";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                string query = $"INSERT INTO League (T_Id, S_Id) VALUES ({T_Id}, {sportId});";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.ExecuteNonQuery();
                }
            }
            Console.WriteLine("Do you want to add more sport to tournament :");
            string answer = Console.ReadLine();
            if (answer == "yes") { itr = true; }else itr = false;
            }

        }

    public static void AddSport()
    {
        Console.Write("Enter the Sport Name to be added : ");
        String S_Name=Console.ReadLine();
        string connectionString = "Data Source=DESKTOP-A1NJHOG;Initial Catalog=Sports;Integrated Security=True;Encrypt=False;";
        using (SqlConnection connection = new SqlConnection(connectionString))
        {
            connection.Open();
            string query = $"INSERT INTO Sports (S_Id, S_Name) VALUES ({S_Id++}, '{S_Name}');";
            using (SqlCommand command = new SqlCommand(query, connection))
            {
                command.ExecuteNonQuery();
            }
        }
    }
}