using ProdukterLib.Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;

namespace ProdukterLib
{
    public class ProdukterRepo
    {

        private List<User> _users { get; set; } = new List<User>(); // Initialiserer en tom liste
                                                                    //private int _nextId = 1;
                                                                    // Constructor, der tilføjer mock data
        public ProdukterRepo()
        {
            _users.Add(new User { Id = GetNextId(), FirstName = "John", LastName = "Doe", Email = "test@test.dk", Phone = "12345678", Password = "Mellon" });
            _users.Add(new User { Id = GetNextId(), FirstName = "Jane", LastName = "Smith", Email = "jane.smith@test.dk", Phone = "23456789", Password = "JanesPass" });
            _users.Add(new User { Id = GetNextId(), FirstName = "Bob", LastName = "Johnson", Email = "bob.johnson@test.dk", Phone = "34567890", Password = "BobsPass" });
            _users.Add(new User { Id = GetNextId(), FirstName = "Alice", LastName = "Brown", Email = "alice.brown@test.dk", Phone = "45678901", Password = "AlicesPass" });
            _users.Add(new User { Id = GetNextId(), FirstName = "Michael", LastName = "Hansen", Email = "michael.hansen@test.dk", Phone = "56789012", Password = "MichaelsPass" });
            _users.Add(new User { Id = GetNextId(), FirstName = "Camilla", LastName = "Mortensen", Email = "camilla.mortensen@test.dk", Phone = "67890123", Password = "CamillasPass" });
            _users.Add(new User { Id = GetNextId(), FirstName = "David", LastName = "Eriksen", Email = "david.eriksen@test.dk", Phone = "78901234", Password = "DavidsPass" });
            _users.Add(new User { Id = GetNextId(), FirstName = "Emma", LastName = "Andersen", Email = "emma.andersen@test.dk", Phone = "89012345", Password = "EmmasPass" });
            _users.Add(new User { Id = GetNextId(), FirstName = "Frederik", LastName = "Nielsen", Email = "frederik.nielsen@test.dk", Phone = "90123456", Password = "FrederiksPass" });
            _users.Add(new User { Id = GetNextId(), FirstName = "Sofie", LastName = "Petersen", Email = "sofie.petersen@test.dk", Phone = "01234567", Password = "SofiesPass" });
        }

        // GetAll: Returnerer alle brugere
        public List<User> GetAll()
        {
            return new List<User>(_users); // Returnerer en ny liste for at undgå ekstern modifikation
        }

        private int GetNextId()
        {
            return _users.Count == 0 ? 1 : _users.Max(resultat => resultat.Id) + 1; //hvis der ikke er nogen participants så er id = 1 ellers er id = max id + 1
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
            user.Id = GetNextId();
            _users.Add(user);
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
