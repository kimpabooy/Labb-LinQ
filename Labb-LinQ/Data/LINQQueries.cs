using Labb_LinQ.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Channels;

namespace Labb_LinQ.Data
{
    public class LINQQueries
    {
        // Hämta alla produkter i kategorin "Electronics" och sortera dem efter pris (högst först).
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

        // Lista alla leverantörer som har produkter med ett lagersaldo under 10 enheter.
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

        // Beräkna det totala ordervärdet för alla ordrar gjorda under den senaste månaden.
        public void ShowTotalOrderValueLastMonth(ProductContext context)
        {
            var lastMounth = DateTime.Now.AddMonths(-1);

            var total = context.Orders
                .Where(o => o.OrderDate >= lastMounth)
                .Sum(o => o.TotalAmount);

            Console.WriteLine($"Totalt ordervärde senaste månaden: {total} kr");
        }

        // Hittar de 3 mest sålda produkterna baserat på OrderDetail-data.
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

        // Listar alla kategorier och antalet produkter i varje kategori.
        public void ListAllProductInCategory(ProductContext context)
        {
            var listAllProducts = context.Categories
                .Select(c => new
                {
                    c.Name,
                    ProductCount = c.Products.Count
                })
                .ToList();
            

            foreach (var product in listAllProducts)
            {
                Console.WriteLine($"Kategori: {product.Name}\nAntal: {product.ProductCount} st.\n");
            }
        }

        // Hämtar alla ordrar med tillhörande kunduppgifter och orderdetaljer där totalbeloppet överstiger 1000 kr.
        public void OrdersWithInfo(ProductContext context)
        {
            var orders = context.Orders
                .Where(o => o.TotalAmount > 1000)
                .Include(o => o.Customer)
                .Include(o => o.OrderDetails)
                .ThenInclude(o => o.Product)
                .ToList();

            foreach (var order in orders)
            {
                Console.WriteLine($"\nNamn: {order.Customer.Name}\nOrderDatum: {order.OrderDate.ToShortDateString()}\n");
                foreach (var item in order.OrderDetails)
                {
                    Console.Write($"Produkt:{item.Product.Name} | Kostnad: {order.TotalAmount}:-\n");
                }
                Console.WriteLine("..........................................");
            }
        }
    }
}
