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
                    Console.WriteLine($"\n{prefix} {option}");
                }
                Console.ResetColor();
            }
        }

        private string AboutDisplay(int option)
        {
            if (option == 0)
                return "Tools to check for exploits on specific port.\n";
            if (option == 1)
                return "Tools to test HTTP-exploitable.\n";

            return "All of them were doomed from the start. Last bullet ended this parade of egoism and frauds.";
        }

        public int Run()
        {
            ConsoleKey keyPressed;
            do
            {
                Console.Clear();
                OptionsDisplay();
                Console.WriteLine(AboutDisplay(SelectedIndex));
                
                ConsoleKeyInfo keyInfo = Console.ReadKey(true);
                keyPressed = keyInfo.Key;

                if (keyPressed == ConsoleKey.UpArrow & SelectedIndex > 0)
                    SelectedIndex--;
                else if (keyPressed == ConsoleKey.DownArrow & SelectedIndex < Options.Length - 1) // subtracting 1 because for some reason it goes beyond max
                    SelectedIndex++;

            } while(keyPressed != ConsoleKey.Escape);
            
            return SelectedIndex;
        }

        public static void Initialise()
        {
            string[] Options = {
            "PORT", "HTTP", "SSL", "SQL", "BRUTE", 
            "Cirno-the-Great-Best-Fairy-Nine-out-of-Ten" // todo
            };
            
            Menu mainMenu = new Menu(Options);
            mainMenu.Run();
        }
    }
}