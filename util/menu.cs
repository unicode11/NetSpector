using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;

namespace NetSpector.util
{
    public class Menu
    {
        public static Dictionary<int, string> Options = new Dictionary<int, string>()
        {
            {1, "PORT"},
            {2, "HTTP"},
            {3, "SSL-CERTIFICATE"},
            {4, "SQL"},
            {5, "BRUTE"},
            {9, "Cirno-the-Great-Best-Fairy-Nine-out-of-Ten"}, // TODO
        };

        public static void ClearDisplay()
        {
            Console.Clear();
        }
        
        
        static void OptionsDisplay()
        {
            foreach (var option in Options)
            {
                if (option.Key != 9)
                    Console.WriteLine($"{option.Key}. {option.Value}");
                else
                    Console.WriteLine($"\n{option.Key}. {option.Value}");
            }
        }

        static void ControlDisplay()
        {
            Console.WriteLine(
                "\n" +
                "use Arrow keys to navigate\n" +
                "use ENTER to execute selected option\n" +
                "use ESC to quit" 
            );
        }

        static void MiscDisplay()
        {
            Console.WriteLine(
                "\n" +
                "version: CLI-0.0.0\n" +
                "(c) unicode, 2025" // placeholder потом заменю на нормальные асции логушки TODO
                ); 
        }
        public static void Display()
        {
            OptionsDisplay();
            ControlDisplay();
            MiscDisplay();
        }
    }
}