using System;
using System.Collections.Generic;
using System.Linq;

namespace ProdukterLib
{
    internal class ProdukterRepo
    {
        // Instance fields
        private readonly List<Product> _products;

        // Constructor, that adds mock data
        public ProdukterRepo(bool mockData = false)
        {
            _products = new List<Product>();

            if (mockData)
            {
                PopulateProducts();
            }
        }

        // Mock data for initialization
        private void PopulateProducts()
        {
            Add(new Product { Id = 1, Name = "Product 1", Price = 10.99m, Stock = 100, ImageUrl = "www.example1.com" });
            Add(new Product { Id = 2, Name = "Product 2", Price = 20.99m, Stock = 200, ImageUrl = "www.example2.com" });
            Add(new Product { Id = 3, Name = "Product 3", Price = 30.99m, Stock = 300, ImageUrl = "www.example3.com" });
            Add(new Product { Id = 4, Name = "Product 4", Price = 40.99m, Stock = 400, ImageUrl = "www.example4.com" });
        }

        // GetAll: Returns all products
        public List<Product> GetAll()
        {
            return new List<Product>(_products); // Returns a new list
        }

        // GetById: Returns a product based on ID
        public Product GetById(int id)
        {
            Product? product = _products.Find(p => p.Id == id);

            if (product == null)
            {
                throw new KeyNotFoundException($"Product with ID {id} was not found.");
            }
            return product;
        }

        // Add: Adds a new product and returns it
        public Product Add(Product product)
        {
            _products.Add(product);
            return product;
        }

        // Update: Updates an existing product and returns the updated version
        public Product Update(int id, Product updatedProduct)
        {
            Product existingProduct = GetById(id); // Throws KeyNotFoundException if ID is not found

            // Update values
            existingProduct.Name = updatedProduct.Name;
            existingProduct.Price = updatedProduct.Price;
            existingProduct.Stock = updatedProduct.Stock;
            existingProduct.ImageUrl = updatedProduct.ImageUrl;

            return existingProduct;
        }

        // Delete: Removes a product based on ID and returns the removed product
        public Product Delete(int id)
        {
            Product product = GetById(id); // Throws KeyNotFoundException if ID is not found
            _products.Remove(product);
            return product;
        }
    }

    // Assuming Product class is defined somewhere in your project
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public int Stock { get; set; }
        public string ImageUrl { get; set; }
    }
}
