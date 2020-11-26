using System;
using Monkiato.BoardGame.Core.State;

namespace Monkiato.BoardGame.Core.Actions
{
    /// <summary>
    /// Represent any action performed during the game, either triggered by the user or internal
    /// state changes that were caused by automated action (e.g. remote user actions, AI, or actions
    /// triggered by game conditions)
    /// </summary>
    public interface IAction<T> where T : Enum
    {
        /// <summary>
        /// Indicates if the action can be performed based on the current game state
        /// </summary>
        /// /// <param name="gameState">current game state</param>
        /// <returns>true if the action can be applied to the current game state, otherwise returns false</returns>
        bool IsLegal(IGameState<T> gameState);

        /// <summary>
        /// Perform current action, current game state is expected to change is most cases after the action is performed 
        /// </summary>
        /// <param name="gameState">current game state</param>
        void Apply(IGameState<T> gameState);
    }
}