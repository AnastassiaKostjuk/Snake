using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace snake
{
    internal class Menu
    {
        public static void ShowMainMenu()
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Red;
            
            Console.SetCursorPosition(20, 4);
            Console.WriteLine("             SNAKE GAME        ");
          
            // Кнопки
            Console.SetCursorPosition(20, 7);
            Console.WriteLine("             Start the Magic(s)             ");
            Console.SetCursorPosition(20, 8);
            Console.WriteLine("             Exit to Muggle World(e)        ");

            Console.SetCursorPosition(33, 15);
            Console.Write("Choose your spell (s or e): ");
            Console.SetCursorPosition(33, 14);
            Console.WriteLine("Show Players Records (p)");

            Console.SetCursorPosition(33, 17);
            Console.Write("Choose: ");

            var key = Console.ReadKey();
            switch (char.ToLower(key.KeyChar))
            {
                case 's':
                    Console.Clear();
                    Console.SetCursorPosition(30, 10);
                    Console.Write("Enter your name (max 20 chars): ");
                    PlayerSession.PlayerName = Console.ReadLine();
                    if (PlayerSession.PlayerName.Length > 20)
                        PlayerSession.PlayerName = PlayerSession.PlayerName.Substring(0, 20);
                    GameManager.StartGame();
                    break;

                case 'e':
                    Environment.Exit(0);
                    break;

                case 'p':
                    PlayerManager.ShowPlayers();
                    ShowMainMenu(); // Возврат в меню
                    break;

                default:
                    ShowMainMenu(); // Повторный вызов
                    break;
            }
        }
    }
}
