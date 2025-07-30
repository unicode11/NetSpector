using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.AccessControl;
using NetSpector.util.menu;

namespace NetSpector.util
{
    public class Menu
    {
        private int SelectedIndex;
        private Option[] Options;

        public struct Option
        {
            public string Name;
            public string Description;
            public Action action;
            public Option(string name, string description, Action custaction)
            {
                Name = name;
                Description = description;
                action = custaction;
            }

            public void Invoker() // ЭТА ШТО ОТСЫЛКА НА ИНВОКЕРА ИЗ ДОТА 2???????
            {
                action?.Invoke();
            }
        };

        
        public Menu(Option[] options)
        {
            Options = options;
            SelectedIndex = 0;
        }
        
        private void OptionsDisplay()
        {
            int maxLength = 0;
            
            foreach (var option in Options)
                maxLength = Math.Max(maxLength, option.Name.Length);
            
            for (int i = 0; i < Options.Length; i++)
            {
                Option option = Options[i];
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
                
                Console.WriteLine($"{prefix} << {option.Name.PadRight(maxLength)} >>");
                Console.ResetColor();
            }
        }

        public int Run()
        {
            ConsoleKey keyPressed;
            do
            {
                Console.Clear();
                OptionsDisplay();
                Console.WriteLine(Options[SelectedIndex].Description);
                
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
            Initialise(MAIN_MENU.Options);
        }

        public static void Initialise(Option[] options = null)
        {
            if (options == null)
                options = new Option[] { new Option("EXIT", "Exit", ReturnToMain)};
            
            Menu menu = new Menu(options);
            int selectedIndex = menu.Run();
            switch (selectedIndex)
            {
                case 0:
                    Console.WriteLine("there's naher we can do");
                    ReturnToMain();
                    break;
                case 5:
                    Console.WriteLine("bye");
                    Environment.Exit(0);
                    break;
            }                
        }
    }
}