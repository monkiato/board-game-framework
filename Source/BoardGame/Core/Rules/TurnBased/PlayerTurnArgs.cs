using System;
using Monkiato.BoardGame.Core.Seat;

namespace Monkiato.BoardGame.Core.Rules.TurnBased
{
    public class PlayerTurnArgs : EventArgs
    {
        public IPlayer Player { get; }
        
        public PlayerTurnArgs(IPlayer player)
        {
            Player = player;
        }
    }
}