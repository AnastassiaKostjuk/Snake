using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace snake
{
    internal class Level
    {
        public int LevelNumber { get; private set; }
        public int Speed { get; private set; }
        public int Obstacles { get; private set; }

        public Level(int score)
        {
            LevelNumber = Math.Max(1, score / 50 + 1); // Новый уровень каждые 50 очков
            Speed = Math.Max(50, 100 - (LevelNumber - 1) * 10); // Не ниже 50 мс
            Obstacles = (LevelNumber - 1) * 3;
        }

        public void ApplySettingsToGame(ref int gameSpeed)
        {
            gameSpeed = Speed;
        }
    }
}
