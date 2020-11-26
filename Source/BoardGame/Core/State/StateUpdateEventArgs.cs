using System;
using System.Collections.Generic;

namespace Monkiato.BoardGame.Core.State
{
    public class StateUpdateEventArgs<T> : EventArgs where T : Enum
    {
        public StateUpdateEventArgs(T newState, IDictionary<string, object> data)
        {
            NewState = newState;
            Data = data;
        }

        public IDictionary<string, object> Data { get; }

        public T NewState { get; } 
    }
}