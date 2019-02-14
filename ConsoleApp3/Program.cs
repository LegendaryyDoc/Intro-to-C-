using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.IO;
using Serialization;
using System.Runtime.Serialization.Formatters.Soap;

namespace Serialization
{
    class main
    {
        static void Main()
        {
            Player p1 = null;
            p1.name = "Dug";
            p1.curHealth = 10;
            p1.maxHealth = 100;
            p1.gold = 12390;
            p1.isRare = false;

            Stream str = File.Open("test.xml", FileMode.Create);
            SoapFormatter formatter;
        }
    }
}
