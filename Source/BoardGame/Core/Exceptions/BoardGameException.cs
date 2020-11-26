using System;

namespace Monkiato.BoardGame.Core.Exceptions
{
    public class BoardGameException : Exception
    {
        protected BoardGameException(string message) : base(message)
        {
            
        }
    }
}