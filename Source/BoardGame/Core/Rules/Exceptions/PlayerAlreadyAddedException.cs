using Monkiato.BoardGame.Core.Exceptions;

namespace Monkiato.BoardGame.Core.Rules.Exceptions
{
    public class PlayerAlreadyAddedException : BoardGameException
    {
        public PlayerAlreadyAddedException() :
            base("Player already added to the turn controller")
        {
        }
    }
}