using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace snake
{
    internal class Lives
    {
        public int CurrentLives { get; private set; } = 3;

        public bool IsAlive => CurrentLives > 0;

        public void LoseLife()
        {
            if (CurrentLives > 0)
                CurrentLives--;
        }

        public void ResetLives()
        {
            CurrentLives = 3;
        }

        public void ShowLives()
        {
            Console.SetCursorPosition(7, 2);
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.Write($"Lives: {new string('❤', CurrentLives)}   ");
        }

        public void AddLife()
        {
            if (CurrentLives < 3) 
                CurrentLives++;
        }

    }
}
