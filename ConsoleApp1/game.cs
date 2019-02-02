using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using myVector3;
using GameStatesChange;

namespace ConsoleApp1
{
    class game
    {
        static void Main()
        {
            Vector3 vector3 = new Vector3();

            vector3.x = 10.0f;
            /*    Player    */

            PlayerClass.PlayerStats newPlayer = new PlayerClass.PlayerStats();

            Console.WriteLine(newPlayer.playerName);

            newPlayer.playerName = "Pablo";

            Console.WriteLine(newPlayer.playerName);
            Console.WriteLine(newPlayer.score);

            GameState g = new GameState();

            bool on = true;
            g.CurrentState = GameState.states.AfterMatch;
            g.CurrentState = GameState.states.GameInProgres;
            g.CurrentState = GameState.states.Lobby;
            g.CurrentState = GameState.states.StartScreen;
        }
    }
}
