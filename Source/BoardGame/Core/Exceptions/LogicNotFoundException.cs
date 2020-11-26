using System;

namespace Monkiato.BoardGame.Core.Exceptions
{
    public class LogicNotFoundException : Exception
    {
        public LogicNotFoundException(Type componentType, Type logicType) :
            base($"logic '{componentType}' not found for component '{logicType}'")
        {
            
        }
    }
}