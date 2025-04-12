using Microsoft.EntityFrameworkCore;

namespace Labb_LinQ.Data
{
    public class LINQQueries
    {
        // List all products in category "Electronics" and order by price descending.
        public void ShowElectronicsProducts(ProductContext context)
        {
            string categoryName = "Electronics"; // Category's: Electronics, Home & Kitchen, Clothing, Sports, Books

            var products = context.Products
                .Where(p => p.Category.Name == categoryName)
                .OrderByDescending(p => p.Price)
                .ToList();

            Console.WriteLine($"\n << Produkter I Kategorin {categoryName} >> \n");
            LineBreak();
            foreach (var product in products)
            {
                Console.WriteLine($"\nArtikel: {product.Name}\nPris: {product.Price} kr");
                Console.WriteLine($"\nBeskrivning: {product.Description}\n");
                LineBreak();
            }
        }

        // List all suppliers that have products with a stock quantity below 10.
        public void ShowSupplierWithLowAmmount(ProductContext context)
        {
            var suppliers = context.Products
                .Where(p => p.StockQuantity < 10)
                .Select(p => p.Supplier)
                .Distinct()
                .ToList();

            Console.WriteLine("\n << Leverantörer Med Mindre Än 10 Produkter I Lager >> \n");
            foreach (var supplier in suppliers)
            {
                Console.WriteLine($"\nLeverantör: {supplier.Name}");
                Console.WriteLine($"Kontaktperson: {supplier.ContactPerson}");
                Console.WriteLine($"Email: {supplier.Email}");
                Console.WriteLine($"Tel: {supplier.Phone}");
                LineBreak();
            }
        }

        // List total order value for the last month.
        public void ShowTotalOrderValueLastMonth(ProductContext context)
        {
            var lastMounth = DateTime.Now.AddMonths(-1);

            var total = context.Orders
                .Where(o => o.OrderDate >= lastMounth)
                .Sum(o => o.TotalAmount);

            Console.WriteLine($"\n << Totalt Ordervärde Senaste Månaden >>\n");
            Console.WriteLine($"Total Ordervärde: {total} kr");
        }

        // List top three sold products based on OrderDetail data.
        public void TopThreeProductSold(ProductContext context)
        {
            int count = 1;
            var topThree = context.OrderDetails
                .GroupBy(od => od.Product)
                .Select(p => new
                {
                    Product = p.Key,
                    TotalProdSold = p.Sum(p => p.Quantity),
                    TotalPrice = p.Sum(p => p.Quantity * p.Product.Price)
                })
                .OrderByDescending(p => p.TotalProdSold)
                .Take(3)
                .ToList();

            Console.WriteLine("\n << Top 3 Sålda Produkterna >>");
            foreach (var product in topThree)
            {
                Console.WriteLine($"\n#{count}\nProdukt: {product.Product.Name}\nTotalt Antal Sålda: {product.TotalProdSold}st\nTotalt Pris: {Math.Round(product.TotalPrice,2)} kr");
                Console.WriteLine("......................................");
                count++;
            }
        }

        // List all categories and the number of products in each category.
        public void ListAllProductInCategory(ProductContext context)
        {
            var listAllProducts = context.Categories
                .Select(c => new
                {
                    c.Name,
                    ProductCount = c.Products.Count
                })
                .ToList();

            Console.WriteLine($"\n << Antal Produkter I Varje Kategori >>\n");
            foreach (var product in listAllProducts)
            {
                Console.WriteLine($"Kategori: {product.Name}\nAntal Produkter: {product.ProductCount} st.");
                LineBreak();
            }
        }

        // List all orders with customer and product info where the total amount exceeds 1000 kr.
        public void OrdersWithInfo(ProductContext context)
        {
            var orders = context.Orders
                .Where(o => o.TotalAmount > 1000)
                .Include(o => o.Customer)
                .Include(o => o.OrderDetails)
                .ThenInclude(o => o.Product)
                .ToList();

            Console.WriteLine($"\n << Order Information >>");
            foreach (var order in orders)
            {
                Console.WriteLine($"\nKund Id: #{order.Customer.Id}\n");
                Console.WriteLine($"Namn: {order.Customer.Name}");
                Console.WriteLine($"Adress: {order.Customer.Address}");
                Console.WriteLine($"Email: {order.Customer.Email}");
                Console.WriteLine($"Tel: {order.Customer.Phone}\n");
                Console.WriteLine($"Order Id: #{order.Id}\nOrder Datum: {order.OrderDate.ToShortDateString()}");

                decimal totalOrderAmount = 0;

                foreach (var item in order.OrderDetails)
                {
                    decimal itemTotal = item.UnitPrice * item.Quantity;
                    totalOrderAmount += itemTotal;

                    Console.WriteLine($"\n{item.Quantity} x {item.Product.Name} ( à pris {item.UnitPrice}:- ) ");
                }
                Console.WriteLine($"\nTotal Orderkostnad: {totalOrderAmount}:-\n");
                Console.WriteLine($"Status: >> {order.Status} <<");
                Console.WriteLine("..........................................");
            }
        }

        private void LineBreak()
        {
            Console.WriteLine("........................");
        }
    }
}
