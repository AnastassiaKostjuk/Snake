using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace snake
{
    internal class GameData
    {
        public static readonly string ScoreFile = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "scores.txt");
    }
}
