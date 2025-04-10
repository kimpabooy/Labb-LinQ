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
            }
            
            Console.ReadKey();
        }
    }
}
