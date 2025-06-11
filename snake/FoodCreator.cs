using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace snake
{
    class FoodCreator
    {
        public class Food
        {
            public Point Position { get; }
            public FoodCreator.FoodType Type { get; }
            public char Symbol { get; }
            public ConsoleColor Color { get; }

            public Food(Point position, FoodCreator.FoodType type, char symbol, ConsoleColor color)
            {
                Position = position;
                Type = type;
                Symbol = symbol;
                Color = color;
            }

            public void Draw()
            {
                Console.ForegroundColor = Color;
                Console.SetCursorPosition(Position.x, Position.y);
                Console.Write(Symbol);
                Console.ResetColor();
            }
        }



        public enum FoodType
        {
            Regular, // +10 очков
            Bonus, // +25 очков
            Money, // + 50 очков
            Health, // + 1 жизнь / 5 очков
            Sun,      // ускорение + 100 очков
            Flower,   // + 10 очков
            Magic     // +10 очков
        }

        Dictionary<char, (FoodType Type, ConsoleColor Color)> foodData = new()
    {
        { '$', (FoodType.Money, ConsoleColor.Green) },
        { '✶', (FoodType.Regular, ConsoleColor.Cyan) },
        { '♦', (FoodType.Bonus, ConsoleColor.Magenta) },
        { '♥', (FoodType.Health, ConsoleColor.Red) },
        { '☀', (FoodType.Sun, ConsoleColor.Yellow) },
        { '✿', (FoodType.Flower, ConsoleColor.Blue) },
        { '⚗', (FoodType.Magic, ConsoleColor.DarkMagenta) }
        };

        int mapWidth;
        int mapHeight;
        Random random = new Random();

        public FoodCreator(int _mapWidth, int _mapHeight)
        {
            mapWidth = _mapWidth;
            mapHeight = _mapHeight;
        }

        public Food CreateFood(List<Point> snakeBody, List<Point> wallPoints)
        {
            Food food;
            do
            {
                int x = random.Next(2, mapWidth - 2);
                int y = random.Next(2, mapHeight - 2);
                var symbol = foodData.Keys.ElementAt(random.Next(foodData.Count));
                var (type, color) = foodData[symbol];
                Point position = new Point(x, y, symbol) { FoodColor = color };
                food = new Food(position, type, symbol, color);
            }
            while (snakeBody.Any(p => p.IsHit(food.Position)) || wallPoints.Any(p => p.IsHit(food.Position)));

            return food;
        }
    }
}
