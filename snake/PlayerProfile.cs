using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace snake
{
    internal class PlayerProfile
    {
        public string Name { get; set; }
        public int TotalGames { get; set; }
        public int BestScore { get; set; }
        public DateTime JoinDate { get; set; }

        public PlayerProfile(string name)
        {
            Name = name;
            TotalGames = 0;
            BestScore = 0;
            JoinDate = DateTime.Now;
        }

        public void UpdateAfterGame(int score)
        {
            TotalGames++;
            if (score > BestScore)
                BestScore = score;
        }

        public override string ToString()
        {
            return $"{Name} | Games: {TotalGames}, Best: {BestScore}, Joined: {JoinDate.ToShortDateString()}";
        }
    }
}
