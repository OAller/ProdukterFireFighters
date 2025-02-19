using BCrypt.Net;
using Microsoft.Data.SqlClient;
using System;
using System.Diagnostics;
using static System.Net.Mime.MediaTypeNames;
using static System.Runtime.InteropServices.JavaScript.JSType;

public class ProdukterRepo
{
    private string connectionString;
    public ProdukterRepo()
    {

        // connectionString = "Data Source=mssql16.unoeuro.com;User ID=mathiasabel_dk;Password=Hnmxry4ftGBFzeadDwgp;Connect Timeout=30;Encrypt=False;Trust Server Certificate=False;Application Intent=ReadWrite;Multi Subnet Failover=False";
        connectionString = "Data Source=mssql16.unoeuro.com,1433;Initial Catalog=mathiasabel_dk_db_abel;User ID=mathiasabel_dk;Password=Hnmxry4ftGBFzeadDwgp;Connect Timeout=30;Encrypt=True;Trust Server Certificate=False;Application Intent=ReadWrite;Multi Subnet Failover=False";
    }  //; Connect Timeout = 30; Encrypt = True; Trust Server Certificate = False; Application Intent = ReadWrite; Multi Subnet Failover = False

    // Metode til at oprette en ny bruger
    public void CreateUser(string email, string password)
        {
        try
        {
            Debug.WriteLine("Before");

            string passwordHash = User.HashPassword(password);
            Debug.WriteLine("After hash");


            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                Debug.WriteLine("Before open");

                conn.Open();
                string query = "INSERT INTO Users (Email, Password) VALUES (@Email, @PasswordHash)";

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
        catch (Exception ex)
        {
            Debug.WriteLine(ex.Message);
            Debug.WriteLine(ex.StackTrace);
        }
    }

    // Metode til at validere brugerlogin
    public bool ValidateUser(string email, string password)
    {
        try
        {
            Debug.WriteLine("Starter login-validering for: " + email);

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                Debug.WriteLine("Forbindelse til databasen oprettet.");

                string query = "SELECT Password FROM Users WHERE Email = @Email";

                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@Email", email);
                    object result = cmd.ExecuteScalar();

                    if (result != null)
                    {
                        string storedHash = result.ToString();
                        Debug.WriteLine("Hentet password-hash fra databasen: " + storedHash);

                        bool isValid = BCrypt.Net.BCrypt.Verify(password, storedHash);
                        Debug.WriteLine("BCrypt.Verify resultat: " + isValid);

                        return isValid;
                    }
                    else
                    {
                        Debug.WriteLine("Ingen bruger fundet med email: " + email);
                        return false;
                    }
                }
            }
        }
        catch (SqlException sqlEx)
        {
            Debug.WriteLine("SQL-fejl i ValidateUser: " + sqlEx.Message);
            Debug.WriteLine(sqlEx.StackTrace);
            return false;
        }
        catch (Exception ex)
        {
            Debug.WriteLine("Generel fejl i ValidateUser: " + ex.Message);
            Debug.WriteLine(ex.StackTrace);
            return false;
        }
    }

    public bool ChangeUserPassword(string email, string oldPassword, string newPassword)
    {
        try
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                string query = "SELECT Password FROM Users WHERE Email = @Email";

                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@Email", email);
                    object result = cmd.ExecuteScalar();

                    if (result != null)
                    {
                        string storedHash = result.ToString();

                        if (BCrypt.Net.BCrypt.Verify(oldPassword, storedHash))
                        {
                            string newHashedPassword = BCrypt.Net.BCrypt.HashPassword(newPassword);

                            string updateQuery = "UPDATE Users SET Password = @NewPassword WHERE Email = @Email";
                            using (SqlCommand updateCmd = new SqlCommand(updateQuery, conn))
                            {
                                updateCmd.Parameters.AddWithValue("@Email", email);
                                updateCmd.Parameters.AddWithValue("@NewPassword", newHashedPassword);
                                updateCmd.ExecuteNonQuery();

                                return true;
                            }
                        }
                    }
                }
            }
            return false;
        }
        catch (SqlException sqlEx)
        {
            Debug.WriteLine("SQL-fejl i ChangeUserPassword: " + sqlEx.Message);
            return false;
        }
        catch (Exception ex)
        {
            Debug.WriteLine("Generel fejl i ChangeUserPassword: " + ex.Message);
            return false;
        }
    }


}
