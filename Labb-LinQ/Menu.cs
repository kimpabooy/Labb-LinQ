using Labb_LinQ.Data;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace Labb_LinQ
{
    public class Menu
    {
        LINQQueries linqQueries = new LINQQueries();
        
        public void ShowMenu(ProductContext context)
        {
            while (true)
            {
                Console.Clear();

                Console.WriteLine("\nVälj ett alternativ:");
                Console.WriteLine("1. Visa alla produkter i kategorin Elektronik");
                Console.WriteLine("2. Visa leverantörer med lågt lager");
                Console.WriteLine("3. Visa totala ordervärdet för den senaste månaden");
                Console.WriteLine("4. Visa de tre mest sålda produkterna");
                Console.WriteLine("5. Lista alla produkter i en viss kategori");
                Console.WriteLine("6. Visa alla ordrar med information om kund och produkt");
                Console.WriteLine("7. Avsluta programmet\n");
                Console.Write("\nAnge ditt val (1-7): ");

                // Make sure the user input is valid
                string? choice = Console.ReadLine();
                if (!int.TryParse(choice, out int inputInt) || inputInt < 1 || inputInt > 7)
                {
                    Console.WriteLine("Ogiltigt val. Försök igen.");
                    continue;
                }

                switch (choice)
                {
                    case "1":
                        // Show all products in the Electronics category
                        PleaseWait();
                        linqQueries.ShowElectronicsProducts(context);
                        PressAnyKey();
                        break;
                    case "2":
                        // Show suppliers with low stock.
                        PleaseWait();
                        linqQueries.ShowSupplierWithLowAmmount(context);
                        PressAnyKey();
                        break;
                    case "3":
                        // Show total order value for the last month.
                        PleaseWait();
                        linqQueries.ShowTotalOrderValueLastMonth(context);
                        PressAnyKey();
                        break;
                    case "4":
                        // Show top three sold products.
                        PleaseWait();
                        linqQueries.TopThreeProductSold(context);
                        PressAnyKey();
                        break;
                    case "5":
                        // Show list of all products in a specific category.
                        PleaseWait();
                        linqQueries.ListAllProductInCategory(context);
                        PressAnyKey();
                        break;
                    case "6":
                        // Show all orders with customer and product info.
                        PleaseWait();
                        linqQueries.OrdersWithInfo(context);
                        PressAnyKey();
                        break;
                    case "7":
                        // Exit the program.
                        Console.Clear();
                        Console.WriteLine("\nAvslutar programmet...");
                        PressAnyKey();
                        return;
                    default:
                        // Handle invalid input.
                        Console.WriteLine("Ogiltigt val. Försök igen.");
                        break;
                }
            }
        }

        public void PleaseWait()
        {
            Console.Clear();
            Console.WriteLine("Läser in data, var god och vänta...\n");
            Thread.Sleep(1500);
            Console.Clear();
        }

        public void PressAnyKey()
        {
            Console.WriteLine("\nTryck på valfri tangent för att fortsätta...");
            Console.ReadKey();
        }
    }
}
