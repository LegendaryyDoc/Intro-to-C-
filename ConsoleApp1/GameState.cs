using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameStatesChange
{
    class GameState
    {
        public enum states
        {
            StartScreen,
            Lobby,
            GameInProgres,
            AfterMatch
        };

        public states currentState = GameState.states.StartScreen;
        public states CurrentState
        {
            get
            {
                return currentState;
            }
            set
            {
                states oldState = currentState;
                currentState = value;
                OnStateChanged(oldState);
            }
        }

        void OnStateChanged(states oldState)
        {
            switch (oldState)
            {
                case states.Lobby:
                    Console.WriteLine("--MATCH START--");
                    break;
                case states.GameInProgres:
                    Console.WriteLine("--MATCH ENDED--");
                    break;
                default:
                    break;
            }
            
        }
    }
}
