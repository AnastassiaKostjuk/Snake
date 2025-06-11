using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace snake
{
    class Figure
    {
        public List<Point> pList;

        public void Draw()
        {
            Console.ForegroundColor = ConsoleColor.DarkCyan; // устанавливаем нужный цвет
            foreach (Point p in pList)
            {
                p.Draw();
            }
        }
        public bool IsHit(Figure figure)
        {
            foreach (var p in pList)
            {
                if (figure.IsHit(p))
                    return true;
            }
            return false;
        }

        public bool IsHit(Point point)
        {
            foreach (var p in pList)
            {
                if (p.IsHit(point))
                    return true;
            }
            return false;
        }
    }
}
