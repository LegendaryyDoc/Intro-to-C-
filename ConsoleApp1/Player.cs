using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlayerClass
{
    class PlayerStats
    {
        private String playername = "Dug";
        public String playerName
        {
            get
            {
                return playername;
            }
            set
            {
                if(!string.IsNullOrEmpty(value))
                {
                    playername = value;
                }
            }
        }

        int fragCount = 40;
        int deathCount = 41;
        float totalDamage = 1124;

        public float score
        {
            get
            {
                return deathCount - fragCount;
            }
        }
    }
}
