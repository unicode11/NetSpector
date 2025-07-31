using System;
using System.Collections.Generic;
using NetSpector.Command;

namespace NetSpector.util
{
    public class Menu
    {
        private readonly Option[] Options;
        private int SelectedIndex;

        public Menu(Option[] options)
        {
            Options = options;
            SelectedIndex = 0;
        }

        private void OptionsDisplay()
        {
            var maxLength = 0;

            foreach (var option in Options)
                maxLength = Math.Max(maxLength, option.Name.Length);

            for (var i = 0; i < Options.Length; i++)
            {
                var option = Options[i];
                if (i == SelectedIndex)
                {
                    Console.ForegroundColor = ConsoleColor.Black;
                    Console.BackgroundColor = ConsoleColor.White;
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.BackgroundColor = ConsoleColor.Black;
                }

                Console.WriteLine($"{i} << {option.Name.PadRight(maxLength)} >>");
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

                var keyInfo = Console.ReadKey(true);
                keyPressed = keyInfo.Key;
                var numberKeys = new List<ConsoleKey>();

                for (var i = 0; i < 10; i++) numberKeys.Add(ConsoleKey.D0 + i);

                if ((keyPressed == ConsoleKey.UpArrow) & (SelectedIndex > 0))
                {
                    SelectedIndex--;
                }
                else if ((keyPressed == ConsoleKey.DownArrow) &
                         (SelectedIndex <
                          Options.Length - 1)) // subtracting 1 because it goes beyond limit
                {
                    SelectedIndex++;
                }
                else if (numberKeys.Contains(keyPressed))
                {
                    SelectedIndex = numberKeys.IndexOf(keyPressed);
                    return SelectedIndex;
                    break;
                }
            } while (keyPressed != ConsoleKey.Enter);

            return SelectedIndex;
        }

        public static void ReturnToMain()
        {
            Console.WriteLine("\nPress any key to return to the main menu...");
            Console.ReadKey(true);
            Initialise(MenuList.MainMenu);
        }

        public static void Exit()
        {
            Console.Clear();
            Console.ResetColor();
            Console.Write("Exiting...");
            Environment.Exit(0);
        }

        public static void Initialise(Option[] options = null)
        {
            if (options == null)
                options = new[] { new Option("EXIT", "Exit", () => Exit()) };

            var menu = new Menu(options);
            var selectedIndex = menu.Run();
            options[selectedIndex].Invoker(); // c# простой язык /shrug
        }

        public struct Option
        {
            public string Name;
            public string Description;
            public Action Action;

            public Option(string name, string description, Action custaction)
            {
                Name = name;
                Description = description;
                Action = custaction;
            }

            public void Invoker() // ЭТА ШТО ОТСЫЛКА НА ИНВОКЕРА ИЗ ДОТА 2???????
            {
                Action?.Invoke();
            }
        }
    }

    public class MenuList
    {
        public static Menu.Option[] MainMenu =
        {
            new Menu.Option("EXIT",
                "Leave this mess of a program that was created by the devil himself.", () =>
                    Menu.Exit()),
            new Menu.Option("PORT",
                "Port scanners, fingerprinting etc. Features:\n" +
                "| TCP/UDP ports scanner\n" +
                "| Banner grabbing\n" +
                "| OS Fingerprinting\n" +
                "| Firewall evaider", () =>
                    Menu.Initialise(PortMenu)),
            new Menu.Option("HTTP",
                "Web-exploits, responses, web-structure. Features:\n" +
                "| Security check\n" +
                "| Finding hidden directories\n" +
                "| XSS, HTML injectors\n" +
                "| LFI/RFI\n" +
                "| Cookies & CORS Analysis", () =>
                    Menu.Exit()),
            new Menu.Option("SSL",
                "Certificates, ciphers, configs. Features:\n" +
                "| Info\n" +
                "| Protocol version\n" +
                "| Cipher analysis\n" +
                "| HSTS, Security flags\n" +
                "| Certificate reuse", () =>
                    Menu.Exit()),
            new Menu.Option("SQL",
                "Injections, fuzzing automation. Features: \n" +
                "| SQLi detector\n" +
                "| Error-based SQLi\n" +
                "| Blind SQLi\n" +
                "| Time-based SQLi\n" +
                "| UNION-based SQLi\n" +
                "| WAF/IDS bypass", () =>
                    Menu.Exit()),
            new Menu.Option("BRUTE",
                "Brute-forcing login/auth/etc.. Features: \n" +
                "| Web forms\n" +
                "| HTTP Auth\n" +
                "| SSH Brute\n" +
                "| FTP/SMTP/POP3\n" +
                "| URL\n" +
                "| Username", () =>
                    Menu.Exit())
            // "Cirno-the-Great-Best-Fairy-Nine-out-of-Ten" // todo
        };

        public static Menu.Option[] PortMenu =
        {
            new Menu.Option("EXIT",
                "Leave this mess of a program that was created by the devil himself.", () =>
                    Menu.Exit()),
            new Menu.Option("SCAN",
                "Tool to scan port(-s) for specified address", () =>
                {
                    Port.Scan("2.56.178.91", 22);
                    Menu.ReturnToMain();
                })
        };
    }
}