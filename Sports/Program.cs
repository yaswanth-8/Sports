
using Microsoft.Data.SqlClient;
using System.Data;
using System.Xml.Linq;

namespace MyProject;
class Program
{
    public static int S_Id = 0;
    public static int T_Id = 0;
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

    public static void getInitialDetails()
    {
        string connectionString = "Data Source=DESKTOP-A1NJHOG;Initial Catalog=Sports;Integrated Security=True;Encrypt=False;";
        using (SqlConnection connection = new SqlConnection(connectionString))
        {
            connection.Open();
            string query = $"SELECT COUNT(*) FROM Sports;";
            using (SqlCommand command = new SqlCommand(query, connection))
            {
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        S_Id = reader.GetInt32(0);
                    }
                }
            }
            string query1 = $"SELECT COUNT(*) FROM Tournament;";
            using (SqlCommand command = new SqlCommand(query1, connection))
            {
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        T_Id = reader.GetInt32(0);
                    }
                }
            }
        }
    }

    static void Main(string[] args)
    {
        getInitialDetails();
        //displaySports();
        //AddSport();
        //AddTournament();
        //removeSport();
        //removeTournament();
        //enterScoreboard();
        //getInitialDetails();
        //displayResults();
        removePlayer();


    }
    public static void removePlayer()
    {
        Console.Write("Team Name to be removed from the system : ");
        string Team = Console.ReadLine();
        string connectionString = "Data Source=DESKTOP-A1NJHOG;Initial Catalog=Sports;Integrated Security=True;Encrypt=False;";
        using (SqlConnection connection = new SqlConnection(connectionString))
        {
            connection.Open();
            string query1 = $"DELETE FROM scoreboard WHERE A_Name='{Team}' OR B_Name='{Team}'";
            using (SqlCommand command1 = new SqlCommand(query1, connection))
            {
                command1.ExecuteNonQuery();
            }
        }
    }
    public static void displayResults()
    {
        Console.Write("Enter tournament id: ");
        int TournamentID = int.Parse(Console.ReadLine());
        Console.Write("Enter Sport id: ");
        int sportID = int.Parse(Console.ReadLine());
        string connectionString = "Data Source=DESKTOP-A1NJHOG;Initial Catalog=Sports;Integrated Security=True;Encrypt=False;";
        using (SqlConnection connection = new SqlConnection(connectionString))
        {
            connection.Open();
            string query = $"SELECT M_Id,Team_A,Team_B,A_Name,B_Name FROM scoreboard WHERE T_Id={TournamentID} AND S_Id={sportID}";
            using (SqlCommand command = new SqlCommand(query, connection))
            {
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Console.WriteLine(reader[0] + "             " + reader[3]+" - " + reader[1] +"          " + reader[4]+" - " + reader[2]);
                    }
                }
            }
        }
    }



    public static void enterScoreboard()
    {
        Console.Write("Enter tournament ID:");
        int tournament_id = int.Parse(Console.ReadLine());
        Console.Write("Enter Sport ID:");
        int sport_id = int.Parse(Console.ReadLine());
        Console.WriteLine("Enter Team A name and Team B name :");
        string A_Name= Console.ReadLine();
        string B_Name = Console.ReadLine();
        Console.Write("Which team won : ");
        string wonTeam= Console.ReadLine();
        string TeamA = "";
        string TeamB = "";
        if(wonTeam == A_Name)
        {
            TeamA = "won";
            TeamB = "lose";
        }
        else
        {
            TeamA = "lose";
            TeamB = "won";
        }
        string connectionString = "Data Source=DESKTOP-A1NJHOG;Initial Catalog=Sports;Integrated Security=True;Encrypt=False;";
        using (SqlConnection connection = new SqlConnection(connectionString))
        {
            connection.Open();
            string query = $"INSERT INTO scoreboard VALUES ({tournament_id},{sport_id},'{TeamA}','{TeamB}','{A_Name}','{B_Name}')";
            using (SqlCommand command = new SqlCommand(query, connection))
            {
                command.ExecuteNonQuery();
            }
        }
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
            string query3 = $"DELETE FROM scoreboard WHERE T_Id={TournamentID}";
            using (SqlCommand command1 = new SqlCommand(query3, connection))
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
            string query3 = $"DELETE FROM scoreboard WHERE S_Id={sportId}";
            using (SqlCommand command1 = new SqlCommand(query3, connection))
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
            string query = $"INSERT INTO Tournament (T_Id, T_Name) VALUES ({T_Id+1}, '{T_Name}');";
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
                string query = $"INSERT INTO League (T_Id, S_Id) VALUES ({T_Id+1}, {sportId});";
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
            string query = $"INSERT INTO Sports (S_Id, S_Name) VALUES ({S_Id+1}, '{S_Name}');";
            using (SqlCommand command = new SqlCommand(query, connection))
            {
                command.ExecuteNonQuery();
            }
        }
    }
}