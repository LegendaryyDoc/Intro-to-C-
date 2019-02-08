using System;
using System.Collections.Generic;
using System.Text;
using Serialization;

namespace Serialization
{
    class main
    {
        static void Main()
        {
            Player p1;
            p1.name = "Dug";
            p1.curHealth = 10;

            string name = p1.name;
            float curHealth = p1.curHealth;
            float maxHealth = p1.maxHealth;
            float gold = p1.gold;
            bool isRare = p1.isRare;
        }
    }
}
