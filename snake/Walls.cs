using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace snake
{
    class Walls
    {
        List<Figure> wallList;
        private int mapWidth;
        private int mapHeight;

        public Walls(int mapWidth, int mapHeight)
        {
            this.mapWidth = mapWidth;
            this.mapHeight = mapHeight;
            wallList = new List<Figure>();


            // Создаем линии стен
            wallList.Add(new HorizontalLine(1, mapWidth - 2, 0, '═'));          // Верх
            wallList.Add(new HorizontalLine(1, mapWidth - 2, mapHeight - 1, '═')); // Низ
            wallList.Add(new VerticalLine(1, mapHeight - 2, 0, '║'));            // Лево
            wallList.Add(new VerticalLine(1, mapHeight - 2, mapWidth - 1, '║'));  // Право

            // Отрисовываем углы
            DrawCorners(mapWidth, mapHeight);

        }

        private void DrawCorners(int width, int height)
        {
            Console.ForegroundColor = ConsoleColor.Red;

            Console.SetCursorPosition(0, 0);
            Console.Write('╔');

            Console.SetCursorPosition(width - 1, 0);
            Console.Write('╗');

            Console.SetCursorPosition(0, height - 1);
            Console.Write('╚');

            Console.SetCursorPosition(width - 1, height - 1);
            Console.Write('╝');
        }

        public void AddObstacles(int count, int mapWidth, int mapHeight)
        {
            Random rand = new Random();
            char[] obstacleSymbols = { '☠', '⛓', '⚫', '☢' };

            for (int i = 0; i < count; i++)
            {
                int x = rand.Next(2, mapWidth - 2);
                int y = rand.Next(2, mapHeight - 2);
                char symbol = obstacleSymbols[rand.Next(obstacleSymbols.Length)];

                var obstacle = new Point(x, y, symbol);
                var obstacleFigure = new HorizontalLine(x, x, y, symbol);
                wallList.Add(obstacleFigure);

                Console.SetCursorPosition(x, y);
                switch (symbol)
                {
                    case '☠': Console.ForegroundColor = ConsoleColor.Red; break;
                    case '⛓': Console.ForegroundColor = ConsoleColor.DarkYellow; break;
                    case '⚫': Console.ForegroundColor = ConsoleColor.DarkGray; break;
                    case '☢': Console.ForegroundColor = ConsoleColor.Green; break;
                }
                Console.Write(symbol);
            }
        }

        public char CheckCollision(Point point)
        {
            foreach (var wall in wallList)
            {
                foreach (var wallPoint in wall.pList)
                {
                    if (point.IsHit(wallPoint))
                        return wallPoint.sym;
                }
            }
            return '\0';
        }

        public bool IsHit(Point point)
        {
            return point.x <= 0 || point.x >= mapWidth - 1 ||
                   point.y <= 0 || point.y >= mapHeight - 1;
        }

        public void Draw()
        {
            Console.ForegroundColor = ConsoleColor.DarkCyan;
            foreach (var wall in wallList)
            {
                wall.Draw();
            }
        }

        public List<Point> GetAllPoints()
        {
            return wallList.SelectMany(wall => wall.pList).ToList();
        }
    }
}
