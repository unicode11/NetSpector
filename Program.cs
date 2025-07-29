using System;
using NetSpector.util;

namespace NetSpector
{
    internal class Program
    {
        public static void Main(string[] args)
        {
            Menu.Display();
            Commands.Port.Scan("2.56.178.91", 22);
        }
    }
}