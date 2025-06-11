using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace snake
{
    internal class PlayerManager
    {
        public static void ShowPlayers()
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.SetCursorPosition(5, 2);
            Console.WriteLine("PLAYER RECORDS:");

            if (!File.Exists(GameData.ScoreFile))
            {
                Console.WriteLine("No records yet.");
            }
            else
            {
                var lines = File.ReadAllLines(GameData.ScoreFile);
                var sorted = lines
                    .Select(line => {
                        var parts = line.Split(':');
                        return new { Name = parts[0], Score = int.Parse(parts[1]) };
                    })
                    .OrderByDescending(x => x.Score)
                    .Take(10);

                int y = 4;
                foreach (var item in sorted)
                {
                    Console.SetCursorPosition(5, y++);
                    Console.WriteLine($"{item.Name.PadRight(15)} {item.Score}");
                }
            }

            Console.SetCursorPosition(5, 16);
            Console.WriteLine("Press any key to return...");
            Console.ReadKey();
        }
    }
}
