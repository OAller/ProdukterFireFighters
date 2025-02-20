using System;
using System.ComponentModel.DataAnnotations;
using BCrypt.Net;

public class User
{
    [Key]
    public int Id { get; set; }  // Primær nøgle

    public string Email { get; set; }  // Unik email
    public string PasswordHash { get; set; }  // Hashet adgangskode

    // Konstruktør der sikrer, at email og password ikke er tomme
    public User(string email, string password)
    {
      //  if (string.IsNullOrWhiteSpace(email))
        {
           // throw new ArgumentException("Email må ikke være tom.");
        }

       // if (string.IsNullOrWhiteSpace(password))
        {
           // throw new ArgumentException("Password må ikke være tomt.");
        }

        Email = email;
        PasswordHash = HashPassword(password);
    }

    // Metode til at hashe adgangskode
    public static string HashPassword(string password)
    {
        return BCrypt.Net.BCrypt.HashPassword(password);
    }

    // Metode til at validere adgangskode
    public bool VerifyPassword(string password)
    {
        return BCrypt.Net.BCrypt.Verify(password, this.PasswordHash);
    }
}
