using Monkiato.BoardGame.Core.Exceptions;

namespace Monkiato.BoardGame.Core.Rules.Exceptions
{
    public class PlayerReachedMaxTurnsPerRoundException : BoardGameException
    {
        public PlayerReachedMaxTurnsPerRoundException() :
            base("Mismatching information, current player has reached max turns per round and can't end current turn")
        {
        }
    }
}