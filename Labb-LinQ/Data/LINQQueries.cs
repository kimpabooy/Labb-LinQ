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

        public void ShowTotalOrderValueLastMonth(ProductContext context)
        {
            var lastMounth = DateTime.Now.AddMonths(-1);

            var total = context.Orders
                .Where(o => o.OrderDate >= lastMounth)
                .Sum(o => o.TotalAmount);

            Console.WriteLine($"Totalt ordervärde senaste månaden: {total} kr");
        }

        // Hitta de 3 mest sålda produkterna baserat på OrderDetail-data
        public void TopThreeProductSold(ProductContext context)
        {
            var topThree = context.OrderDetails
                .GroupBy(od => od.Product)
                .Select(p => new
                {
                    Product = p.Key,
                    TotalProdSold = p.Sum(p => p.Quantity)
                })
                .OrderByDescending(p => p.TotalProdSold)
                .Take(3)
                .ToList();

            foreach (var product in topThree)
            {
                Console.WriteLine($"Produkt: {product.Product.Name} Totalt antal sålda: {product.TotalProdSold}");
            }
        }
    }
}
