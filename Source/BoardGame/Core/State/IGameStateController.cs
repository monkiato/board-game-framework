using System;

namespace Monkiato.BoardGame.Core.State
{
    public interface IGameStateController<T> where T : Enum
    {
        IGameState<T> GameState { get; }
    }
}