using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntroCSharp
{
    class Chat
    {     
        static void Main(string[] args)
        {
            bool endChat = false;

            Console.WriteLine("This is the start of this random chat.");

            while (endChat == false)
            {
                string[] fileWordFilter = System.IO.File.ReadAllLines("TextFiles/Filter.txt"); // reading from file for filtered words
                String FilteredWords = string.Join(" ", fileWordFilter); // used to store old words so can add onto the end of a file in blacklist
                string[] fileJokes = System.IO.File.ReadAllLines("TextFiles/Jokes.txt"); // reading from file for filtered words
                String oldJokes = string.Join(", ", fileJokes); // used to store old jokes
                
                Console.WriteLine("Enter some text");

                String userInput = Console.ReadLine();
                Console.WriteLine("");
                args = userInput.Split(' ');

                for (int i = 0; i < args.Length; i++)
                {
                    /*    Checking for command    */

                    if (args[0] == "!") // if ! is the first thing put its a command
                    {
                        Console.ForegroundColor = ConsoleColor.DarkCyan; // changes chat color to DarkCyan for commands
                        if (args[i] == "commands") // displays a all available commands
                        {
                            args[i] = "Commands:\n" +
                                      "- ! fly\n" +
                                      "- ! rules\n" +
                                      "- ! addblacklistedwords\n" +
                                      "- ! endchat\n" +
                                      "- ! joke\n" +
                                      "- ! addnewjoke\n";
                        }

                        else if (args[i] == "fly")
                        {
                            args[i] = "You cannot fly sorry to say...\n";
                        }

                        else if (args[i] == "rules")
                        {
                            args[i] = "Rules:\n" +
                                      "- Be Respectful\n" +
                                      "- Don't be a bot\n" +
                                      "- NO SPAM\n";
                        }

                        else if (args[i] == "joke")
                        {
                            if (fileJokes.Length > 0)
                            {
                                Random rand = new Random();
                                int randJoke = rand.Next(fileJokes.Length);

                                Console.WriteLine(fileJokes[randJoke]);
                            }
                            else
                            {
                                Console.WriteLine("No Jokes have been added try command addnewjoke to make your own!");
                            }
                        }

                        else if (args[i] == "addnewjoke")
                        {
                            Console.WriteLine("Add a new joke to the list of jokes!");
                            string jokeText = Console.ReadLine();

                            if (oldJokes != null)
                            {
                                string oldAndNewJokes = oldJokes + jokeText;

                                string[] newJokeList = oldAndNewJokes.Split(',');

                                System.IO.File.WriteAllLines("TextFiles/Jokes.txt", newJokeList);
                            }
                            else
                            {
                                string[] newJokeList = jokeText.Split(',');

                                System.IO.File.WriteAllLines("TextFiles/Jokes.txt", newJokeList);
                            }
                        }

                        else if (args[i] == "addblacklistedwords") // adds another word to the filters
                        {
                            Console.WriteLine("Add to the list of black listed words for chat, put a space inbetween each of them");
                            string text = Console.ReadLine(); // storing the users input into a string

                            string oldAndNewWords = FilteredWords + " " + text; // adding the old and new words to same string so doesnt delete the old ones and instead adds onto the end of it

                            string[] blackListedWords = oldAndNewWords.Split(' ');

                            System.IO.File.WriteAllLines("TextFiles/Filter.txt", blackListedWords);

                            Console.WriteLine("Words added to the list of not to be used");
                        }

                        else if (args[i] == "endchat")
                        {
                            endChat = true;
                            Console.WriteLine("Leaving chat\n");
                        }

                        else if (args[i] != "!")
                        {
                            args[i] = "Sorry that is not possible use the ! command to see what commands you can use.\n";
                        }
                    }

                    /*    Filtering out words    */
                    else
                    {
                        for (int j = 0; j < fileWordFilter.Length; j++)
                        {
                            Console.ForegroundColor = ConsoleColor.Red;
                            bool word = string.Equals(args[i], fileWordFilter[j], StringComparison.CurrentCultureIgnoreCase);

                            if (word == true)
                            {
                                args[i] = "****";
                            }
                        }
                    }
                }

                userInput = string.Join(" ", args);

                Console.WriteLine(userInput + "\n");
                Console.ResetColor();
            }
        }
    
    }
}
