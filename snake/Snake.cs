using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace snake
{
    class Snake : Figure
    {
        Direction direction;
        Direction nextDirection;

        public Snake(Point tail, int length, Direction _direction)
        {
            direction = _direction;
            nextDirection = _direction;
            pList = new List<Point>();

            for (int i = length - 1; i >= 0; i--)
            {
                Point p = new Point(tail);
                p.Move(i, direction); // теперь голова — первый элемент
                pList.Add(p);
            }
        }


        public void Move(bool grow = false)
        {
            direction = nextDirection;
            Point head = GetNextPoint();

            pList.Insert(0, head);  // Добавляем новую голову
            head.Draw();

            if (!grow)
            {
                Point tail = pList.Last();
                tail.Clear();
                pList.RemoveAt(pList.Count - 1);  // Удаляем хвост только если не поели
            }
        }



        public Point GetNextPoint()
        {
            Point head = pList.First(); // Голова первый элемент
            Point nextPoint = new Point(head);
            nextPoint.Move(1, direction);
            return nextPoint;
        }

        public void HandleKey(ConsoleKey key)
        {
            // Запрещаем разворот на 180 градусов
            switch (key)
            {
                case ConsoleKey.LeftArrow when direction != Direction.RIGHT:
                    nextDirection = Direction.LEFT;
                    break;
                case ConsoleKey.RightArrow when direction != Direction.LEFT:
                    nextDirection = Direction.RIGHT;
                    break;
                case ConsoleKey.DownArrow when direction != Direction.UP:
                    nextDirection = Direction.DOWN;
                    break;
                case ConsoleKey.UpArrow when direction != Direction.DOWN:
                    nextDirection = Direction.UP;
                    break;
            }
        }

        public bool Eat(Point food)
        {
            Point head = GetNextPoint();
            if (head.IsHit(food))
            {
                // Добавляем новую точку в конец (хвост)
                pList.Add(new Point(food));
                return true;
            }
            return false;
        }

        public Point GetHead()
        {
            return pList.First(); // Возвращает первую точку — голову змеи
        }

        public bool CheckSelfCollision()
        {
            Point nextHead = GetNextPoint();
            return pList.Skip(1).Any(p => p.IsHit(nextHead));
        }

    }
}
