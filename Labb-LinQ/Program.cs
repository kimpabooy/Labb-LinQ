﻿using Labb_LinQ.Data;
using System;

namespace Labb_LinQ
{
    public class Program
    {
        static void Main(string[] args)
        {
            using var context = new ProductContext();
            new Menu().ShowMenu(context);
        }
    }
}
