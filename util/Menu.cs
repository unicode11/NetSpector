using System;
using System.Collections.Generic;
using System.Linq;

namespace NetSpector.util
{
    public class Menu
    {
        private int SelectedIndex;
        private string[] Options;
        
        public Menu(string[] options)
        {
            Options = options;
            SelectedIndex = 0;
        }
        
        private void OptionsDisplay()
        {
            int maxLength = 0;
            
            foreach (var option in Options)
            {
                if (maxLength < option.Length & !option.StartsWith("Cirno")) maxLength = option.Length;
            }
            
            for (int i = 0; i < Options.Length; i++)
            {
                string option = Options[i];
                char prefix;

                if (i == SelectedIndex)
                {
                    prefix = '*';
                    Console.ForegroundColor = ConsoleColor.Black;
                    Console.BackgroundColor = ConsoleColor.White;
                }
                else
                {
                    prefix = '.';
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.BackgroundColor = ConsoleColor.Black;
                }
                
                if (!option.StartsWith("Cirno"))
                {
                    Console.WriteLine($"{prefix} <<  {option.PadRight(maxLength)} >>");
                }
                else
                {
                    Console.WriteLine($"\n{prefix} {option}\n\n");
                }
                Console.ResetColor();
            }
        }

        private void AboutDisplay(int option)
        {
            if (option == 0)
                Console.WriteLine(
                    "Tools to check for exploits on specific port.\n"
                    );
            else if (option == 1)
                Console.WriteLine("Tools to test HTTP-exploitable.\n");
            else if (option == 2)
                Console.WriteLine("Tools to test SSL certificates exploits.\n");
            else
                Console.WriteLine("All of them were doomed from the start. Last bullet ended this parade of egoism and frauds.");
        }

        public int Run()
        {
            ConsoleKey keyPressed;
            do
            {
                Console.Clear();
                OptionsDisplay();
                AboutDisplay(SelectedIndex);
                
                ConsoleKeyInfo keyInfo = Console.ReadKey(true);
                keyPressed = keyInfo.Key;

                if (keyPressed == ConsoleKey.UpArrow & SelectedIndex > 0)
                    SelectedIndex--;
                else if (keyPressed == ConsoleKey.DownArrow & SelectedIndex < Options.Length - 1) // subtracting 1 because for some reason it goes beyond max
                    SelectedIndex++;

            } while(keyPressed != ConsoleKey.Enter);
            
            return SelectedIndex;
        }

        public static void ReturnToMain()
        {
            Console.WriteLine("\nPress any key to return to the main menu...");
            Console.ReadKey(true);
            Initialise();
        }

        public static void Initialise()
        {
            string[] Options = {
            "PORT", "HTTP", "SSL", "SQL", "BRUTE", "EXIT", 
            "Cirno-the-Great-Best-Fairy-Nine-out-of-Ten" // todo
            };
            
            Menu mainMenu = new Menu(Options);
            int selectedIndex = mainMenu.Run();
            switch (selectedIndex)
            {
                case 0:
                    Console.WriteLine("there's naher we can do");
                    ReturnToMain();
                    break;
                case 5:
                    Environment.Exit(0);
                    break;
            }                
        }
    }
}