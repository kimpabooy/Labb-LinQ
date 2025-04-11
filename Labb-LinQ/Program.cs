using Labb_LinQ.Data;
using System;

namespace Labb_LinQ
{
    public class Program
    {
        static void Main(string[] args)
        {
            LINQQueries querys = new LINQQueries();
            
            using(var context = new ProductContext())
            {
                querys.ShowElectronicsProducts(context);
                querys.ShowSupplierWithLowAmmount(context);
                querys.ShowTotalOrderValueLastMonth(context);
                querys.TopThreeProductSold(context);
                querys.ListAllProductInCategory(context);
                querys.OrdersWithInfo(context);
            }
            Console.ReadKey();
        }
    }
}
