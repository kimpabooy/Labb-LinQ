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

            Console.WriteLine("Produkter i kategorin Electronics:");
            foreach (var product in products)
            {
                Console.WriteLine($"{product.Name} - {product.Price} kr");
            }
        }

        public void ShowSupplierWithLowAmmount(ProductContext context)
        {
            var suppliers = context.Products
                .Where(p => p.StockQuantity < 10)
                .Select(p => p.Supplier)
                .ToList();

            Console.WriteLine("Leverantörer med mindre än 10 producter i lager:");
            foreach (var supplier in suppliers)
            {
                Console.WriteLine($"{supplier.Name}");
            }
        }
    }
}
