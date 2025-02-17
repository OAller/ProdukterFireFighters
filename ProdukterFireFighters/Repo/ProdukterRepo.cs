using Microsoft.Data.SqlClient;
using System;

using ProdukterLib;

public class ProdukterRepo
{
    private string connectionString = Environment.GetEnvironmentVariable("MyDatabaseConnection");

    // Metode til at oprette en ny bruger
    public void CreateUser(string email, string password)
    {
        string passwordHash = User.HashPassword(password);

        using (SqlConnection conn = new SqlConnection(connectionString))
        {
            conn.Open();
            string query = "INSERT INTO Users (Email, PasswordHash) VALUES (@Email, @PasswordHash)";

            using (SqlCommand cmd = new SqlCommand(query, conn))
            {
                cmd.Parameters.AddWithValue("@Email", email);
                cmd.Parameters.AddWithValue("@PasswordHash", passwordHash);
                cmd.ExecuteNonQuery();
            }
        }
    }

    // Metode til at validere brugerlogin
    public bool ValidateUser(string email, string password)
    {
        using (SqlConnection conn = new SqlConnection(connectionString))
        {
            conn.Open();
            string query = "SELECT PasswordHash FROM Users WHERE Email = @Email";

            using (SqlCommand cmd = new SqlCommand(query, conn))
            {
                cmd.Parameters.AddWithValue("@Email", email);
                object result = cmd.ExecuteScalar();

                if (result != null)
                {
                    string storedHash = result.ToString();
                    return BCrypt.Net.BCrypt.Verify(password, storedHash);
                }
            }
        }
        return false;
    }
}
