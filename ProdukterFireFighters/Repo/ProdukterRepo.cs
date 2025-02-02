using ProdukterLib.Classes;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ProdukterLib
{
    public class ProdukterRepo
    {
        // Instance fields
        private readonly List<User> _users = new List<User>();
        private int _nextId = 1;

        // Constructor, der tilføjer mock data
        public ProdukterRepo()
        {
            Add(new User { FirstName = "John", LastName = "Doe", Email = "test@test.dk", Phone = "12345678", Password = "Mellon" });
            Add(new User { FirstName = "Jane", LastName = "Smith", Email = "jane.smith@test.dk", Phone = "23456789", Password = "JanesPass" });
            Add(new User { FirstName = "Bob", LastName = "Johnson", Email = "bob.johnson@test.dk", Phone = "34567890", Password = "BobsPass" });
            Add(new User { FirstName = "Alice", LastName = "Brown", Email = "alice.brown@test.dk", Phone = "45678901", Password = "AlicesPass" });
            Add(new User { FirstName = "Michael", LastName = "Hansen", Email = "michael.hansen@test.dk", Phone = "56789012", Password = "MichaelsPass" });
            Add(new User { FirstName = "Camilla", LastName = "Mortensen", Email = "camilla.mortensen@test.dk", Phone = "67890123", Password = "CamillasPass" });
            Add(new User { FirstName = "David", LastName = "Eriksen", Email = "david.eriksen@test.dk", Phone = "78901234", Password = "DavidsPass" });
            Add(new User { FirstName = "Emma", LastName = "Andersen", Email = "emma.andersen@test.dk", Phone = "89012345", Password = "EmmasPass" });
            Add(new User { FirstName = "Frederik", LastName = "Nielsen", Email = "frederik.nielsen@test.dk", Phone = "90123456", Password = "FrederiksPass" });
            Add(new User { FirstName = "Sofie", LastName = "Petersen", Email = "sofie.petersen@test.dk", Phone = "01234567", Password = "SofiesPass" });
        }

        // GetAll: Returnerer alle brugere
        public List<User> GetAll()
        {
            return new List<User>(_users); // Returnerer en ny liste for at undgå ekstern modifikation
        }

        // GetById: Finder en bruger baseret på ID
        public User GetById(int id)
        {
            User? user = _users.Find(u => u.Id == id);

            if (user == null)
            {
                throw new KeyNotFoundException($"User with ID {id} was not found.");
            }
            return user;
        }

        // Add: Tilføjer en ny bruger og returnerer den
        public User Add(User user)
        {
            if (user == null)
            {
                throw new ArgumentNullException(nameof(user));
            }
            user.Id = _nextId++;
            _users.Add( user );
            return user;
           
        }

        // Update: Opdaterer en eksisterende bruger og returnerer den opdaterede version
        public User Update(int id, User updatedUser)
        {
            User existingUser = GetById(id); // Finder eksisterende bruger

            // Opdaterer værdier
            existingUser.FirstName = updatedUser.FirstName;
            existingUser.LastName = updatedUser.LastName;
            existingUser.Email = updatedUser.Email;
            existingUser.Phone = updatedUser.Phone;
            existingUser.Password = updatedUser.Password;
            existingUser.Image = updatedUser.Image;
            existingUser.IsAdmin = updatedUser.IsAdmin;
            existingUser.IsEmployee = updatedUser.IsEmployee;

            return existingUser;
        }

        // Delete: Fjerner en bruger baseret på ID og returnerer den slettede bruger
        public User Delete(int id)
        {
            User user = GetById(id); // Finder eksisterende bruger
            _users.Remove(user);
            return user;
        }
    }
}
