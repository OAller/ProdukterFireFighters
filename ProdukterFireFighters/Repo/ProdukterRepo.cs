using ProdukterLib.Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;


namespace ProdukterLib
{
    internal class ProdukterRepo
    {
        // Instance fields
        private readonly List<User> _users = new List<User>();
        private int _nextId = 1;
        // Constructor, that adds mock data
        public ProdukterRepo()
        {
            Add(new User { Id = GetNextId, FirstName = "John", LastName = "Doe", Email = " }    
        
        private ProdukterRepo()
        {
            Add(new User { Id = 1, FirstName = "John", LastName = "Doe", Email = "test@test.dk", Phone = "12345678", Password = "Mellon", Image = "Url", IsAdmin = false, IsEmployee = false });       
        }

        // GetNextId: Returns the next available ID
        private int GetNextId()
        {
            return _users.Count == 0 ? 1 : _users.Max(resultat => resultat.Id) + 1;
        }

        // GetAll: Returns all products
        public List<Product> GetAll()
        {
            return new List<Product>(_users); // Returns a new list
        }

        // GetById: Returns a product based on ID
        public Product GetById(int id)
        {
            Product? product = _users.Find(p => p.Id == id);

            if (product == null)
            {
                throw new KeyNotFoundException($"Product with ID {id} was not found.");
            }
            return product;
        }

        // Add: Adds a new product and returns it
        public UserStringHandle Add(User product)
        {
            _products.Add(_users);
            return _users;
        }

        // Update: Updates an existing product and returns the updated version
        public User Update(int id, User updatedUser)
        {
            User existingUser = GetById(id); // Throws KeyNotFoundException if ID is not found

            // Update values
            existingUser.Name = updatedUser.Name;
            existingUser.Price = updatedUser.Price;
            existingUser.Stock = updatedUser.Stock;
            existingUser.ImageUrl = updatedUser.ImageUrl;

            return existingUser;
        }

        // Delete: Removes a product based on ID and returns the removed product
        public User Delete(int id)
        {
            User user = GetById(id); // Throws KeyNotFoundException if ID is not found
            _users.Remove(user);
            return user;
        }
    }


}
