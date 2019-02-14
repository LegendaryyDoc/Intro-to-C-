using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization.Formatters.Soap;
using System.IO;
using Serialize;

namespace Serialize
{
    class Program
    {
        static void Main(string[] args)
        {

            /*   WRITING TO FILE    */
            player p1 = new player();

            Stream streamS = File.Open("test.xml", FileMode.Create);
            SoapFormatter formatterS = new SoapFormatter();

            formatterS.Serialize(streamS, p1);
            streamS.Close();

            /*************************************************/
            /*************************************************/

            /*    READING FROM FILE    */
            Stream streamD = File.Open("test.xml", FileMode.Open);
            SoapFormatter formatterD = new SoapFormatter();

            player p2 = null;

            p2 = (player)formatterD.Deserialize(streamD);
            streamD.Close();

            Console.WriteLine(p2.name);
            Console.WriteLine(p2.curHealth);
            Console.WriteLine(p2.maxHealth);
            Console.WriteLine(p2.gold);
            Console.WriteLine(p2.isRare);

            /*************************************************/
            /*************************************************/
        }
    }
}
