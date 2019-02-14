using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LINQ
{
    class hsEntry
    {
        private string name;
        private int rank;
        private int score;

        public string Name { get => name; set => name = value; }
        public int Rank { get => rank; set => rank = value; }
        public int Score { get => score; set => score = value; }

        public hsEntry(string v1, int v2, int v3)
        {
            Name = v1;
            Rank = v2;
            Score = v3;
        }
    }
}
