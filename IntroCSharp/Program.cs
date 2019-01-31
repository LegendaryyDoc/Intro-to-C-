using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntroCSharp
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("This is the start of this random chat.");
            Console.WriteLine("Enter some text");

            String userInput = Console.ReadLine();

            /*var replacements = new[] // weird stuff but works
            {
                new {Find = "Hello", Replace = "*****"},
                new {Find = "name", Replace = "****"},
                new {Find = "Yes", Replace = "***"},
            };

            foreach (var set in replacements)
            {
                userInput = userInput.Replace(set.Find, set.Replace);
            }*/

            args = userInput.Split(' ');

            for(int i = 0; i < args.Length; i++)
            {
                bool helloWord = string.Equals(args[i], "hello", StringComparison.CurrentCultureIgnoreCase);
                bool noWord = string.Equals(args[i], "no", StringComparison.CurrentCultureIgnoreCase);
                bool yesWord = string.Equals(args[i], "yes", StringComparison.CurrentCultureIgnoreCase);
                bool cantWord = string.Equals(args[i], "can't", StringComparison.CurrentCultureIgnoreCase);

                if (helloWord == true)
                {
                    args[i] = "*****";
                }
                if (noWord == true)
                {
                    args[i] = "**";
                }
                if (yesWord == true)
                {
                    args[i] = "*****";
                }
                if (cantWord)
                {
                    args[i] = "****";
                }
            }

            userInput = string.Join(" ", args);

            Console.WriteLine(userInput);
        }
    }
}
