using System;
using System.Collections.Generic;

namespace Monkiato.BoardGame.Core.State
{
    public interface IGameState<T> where T : Enum
    {
        /// <summary>
        /// Gets current state of the game, this is an enum representation
        /// </summary>
        /// <returns>current state</returns>
        T GetState();

        /// <summary>
        /// Modifies current state of the game, it will be replaced by the new state passed by
        /// argument
        /// </summary>
        /// <param name="newState">new state of the game</param>
        /// <param name="extraData">any extra info related to the state update, or null if not required</param>
        void UpdateState(T newState, IDictionary<string, object> extraData = null);
    }
}