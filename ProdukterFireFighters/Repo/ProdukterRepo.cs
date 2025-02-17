using Microsoft.Data.SqlClient;
using System;
using System.Diagnostics;

public class ProdukterRepo
{
    private string connectionString;
    public ProdukterRepo()
    {

        connectionString = "Data Source=mssql16.unoeuro.com;User ID=mathiasabel_dk;Password=Hnmxry4ftGBFzeadDwgp;Connect Timeout=30;Encrypt=False;Trust Server Certificate=False;Application Intent=ReadWrite;Multi Subnet Failover=False";
    }

    // Metode til at oprette en ny bruger
    public void CreateUser(string email, string password)
    {
        Debug.WriteLine("Before");
        
        string passwordHash = User.HashPassword(password);
        Debug.WriteLine("After hash");


        using (SqlConnection conn = new SqlConnection(connectionString))
        {
            Debug.WriteLine("Before open");

            conn.Open();
            string query = "INSERT INTO Users (Email, PasswordHash) VALUES (@Email, @PasswordHash)";

            try
            {
                Debug.WriteLine("Bafterv open");
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@Email", email);
                    cmd.Parameters.AddWithValue("@PasswordHash", passwordHash);
                    cmd.ExecuteNonQuery();

                    Debug.WriteLine("exe open");
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
                Debug.WriteLine(ex.StackTrace);
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
