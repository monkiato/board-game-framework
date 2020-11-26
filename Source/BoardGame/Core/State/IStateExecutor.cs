using System;
using System.Collections.Generic;

namespace Monkiato.BoardGame.Core.State
{
    public interface IStateExecutor<T> where T : Enum
    {
        void Execute(IGameState<T> gameState, object sender, IDictionary<string, object> data);
    }
}