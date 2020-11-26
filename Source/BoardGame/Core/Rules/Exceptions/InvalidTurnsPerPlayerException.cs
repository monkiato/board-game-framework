using Monkiato.BoardGame.Core.Exceptions;

namespace Monkiato.BoardGame.Core.Rules.Exceptions
{
    public class InvalidTurnsPerPlayerException : BoardGameException
    {
        public InvalidTurnsPerPlayerException() :
            base("Player is not allowed to play more turns")
        {
        }
    }
}