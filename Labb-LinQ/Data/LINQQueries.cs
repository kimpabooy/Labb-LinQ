using Labb_LinQ.Models;
using System;

namespace Labb_LinQ.Data
{
    public class LINQQueries
    {
        public void ShowElectronicsProducts(ProductContext context)
        {
            var products = context.Products
                .Where(p => p.Category.Name == "Electronics")
                .OrderByDescending(p => p.Price)
                .ToList();

            Console.WriteLine("Produkter i Electronics:");
            foreach (var product in products)
            {
                Console.WriteLine($"{product.Name} - {product.Price} kr");
            }
        }
    }
}
