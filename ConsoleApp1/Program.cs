using System;
using System.Data.SqlClient;
using BCrypt.Net;

class Program
{
    static void Main(string[] args)
    {
        string connectionString = "Server=LAPTOP-433H02QI\\NGHUY;Database=CaffeShop;Trusted_Connection=True;TrustServerCertificate=True;";

        using (SqlConnection connection = new SqlConnection(connectionString))
        {
            connection.Open();

            // Get all users with plaintext passwords
            string selectQuery = "SELECT Id, Password FROM Users";
            using (SqlCommand selectCommand = new SqlCommand(selectQuery, connection))
            using (SqlDataReader reader = selectCommand.ExecuteReader())
            {
                while (reader.Read())
                {
                    string id = reader.GetString(0);
                    string plainPassword = reader.GetString(1);

                    // Hash the password
                    string hashedPassword = BCrypt.Net.BCrypt.HashPassword(plainPassword);

                    // Update it in the database
                    UpdatePasswordInDatabase(id, hashedPassword, connectionString);

                    Console.WriteLine($"User Id {id}: Password hashed.");
                }
            }
        }

        Console.WriteLine("All passwords have been hashed and updated.");
    }

    static void UpdatePasswordInDatabase(string id, string hashedPassword, string connectionString)
    {
        using (SqlConnection connection = new SqlConnection(connectionString))
        {
            connection.Open();
            string updateQuery = "UPDATE Users SET Password = @HashedPassword WHERE Id = @Id";
            using (SqlCommand updateCommand = new SqlCommand(updateQuery, connection))
            {
                updateCommand.Parameters.AddWithValue("@HashedPassword", hashedPassword);
                updateCommand.Parameters.AddWithValue("@Id", id);
                updateCommand.ExecuteNonQuery();
            }
        }
    }

}
