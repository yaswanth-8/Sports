
using Microsoft.Data.SqlClient;
using System.Data;
using System.Xml.Linq;

namespace MyProject;
class Program
{
    public static int S_Id = 0;
    public static int T_Id = 0;

    //Display Sports Present
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

    //Initial Preparation 
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

    //Main Method
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
        //displayResultistory();
        displayIndividualResults();
        //removePlayer();
        //registerIndividual();

    }

    //Display Stuednts and their scores
    public static void displayIndividualResults()
    {
        string connectionString = "Data Source=DESKTOP-A1NJHOG;Initial Catalog=Sports;Integrated Security=True;Encrypt=False;";
        using (SqlConnection connection = new SqlConnection(connectionString))
        {
            connection.Open();
            string query = $"SELECT StudentName,Score FROM Students";
            using (SqlCommand command = new SqlCommand(query, connection))
            {
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Console.WriteLine(reader[0] + "             " + reader[1] );
                    }
                }
            }
        }
    }


    //Registeration of Individual
    public static void registerIndividual()
    {
        Console.Write("Student Name : ");
        string Name = Console.ReadLine();
        Console.Write("Tounament Id : ");
        string Tid = Console.ReadLine();
        Console.Write("Sport Id : ");
        string Sid = Console.ReadLine();
        string connectionString = "Data Source=DESKTOP-A1NJHOG;Initial Catalog=Sports;Integrated Security=True;Encrypt=False;";
        using (SqlConnection connection = new SqlConnection(connectionString))
        {
            connection.Open();
            string query1 = $"INSERT INTO STUDENTS(StudentName,T_Id,S_Id) VALUES ('{Name}',{Tid},{Sid})";
            using (SqlCommand command1 = new SqlCommand(query1, connection))
            {
                command1.ExecuteNonQuery();
            }
        }
    }

    //Remove Player
    public static void removePlayer()
    {
        Console.Write("Team Name to be removed from the system : ");
        string name = Console.ReadLine();
        string connectionString = "Data Source=DESKTOP-A1NJHOG;Initial Catalog=Sports;Integrated Security=True;Encrypt=False;";
        using (SqlConnection connection = new SqlConnection(connectionString))
        {
            connection.Open();
            string query = $"DELETE FROM Students WHERE StudentName='{name}'";
            using (SqlCommand command1 = new SqlCommand(query, connection))
            {
                command1.ExecuteNonQuery();
            }
            string query1 = $"DELETE FROM scoreboard WHERE A_Name='{name}' OR B_Name='{name}'";
            using (SqlCommand command1 = new SqlCommand(query1, connection))
            {
                command1.ExecuteNonQuery();
            }
        }
    }

    //Display Match History
    public static void displayResultistory()
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


    //Entering Scorecard details
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
        string connectionString = "Data Source=DESKTOP-A1NJHOG;Initial Catalog=Sports;Integrated Security=True;Encrypt=False;";
        if (wonTeam == A_Name)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                string query1 = $"UPDATE Students SET Score = Score + 1 WHERE StudentName='{A_Name}'";
                using (SqlCommand command = new SqlCommand(query1, connection))
                {
                    command.ExecuteNonQuery();
                }
            }
            TeamA = "won";
            TeamB = "lose";
        }
        else
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                string query1 = $"UPDATE Students SET Score = Score + 1 WHERE StudentName='{B_Name}'";
                using (SqlCommand command = new SqlCommand(query1, connection))
                {
                    command.ExecuteNonQuery();
                }
            }
            TeamA = "lose";
            TeamB = "won";
        }
        
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

    //Remove Tournament From the system
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

    //Remove Sport from the system
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

    //Add Tournament to the system
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

    //Adding sport to the Tournament while tournament initialization
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

    //Adding sport to the student
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