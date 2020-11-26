using System;
using System.Collections.Generic;

namespace Monkiato.BoardGame.Core.State
{
    public abstract class GameState<T> : IGameState<T> where T : Enum
    {
        public delegate void OnStateUpdatedEvent(object sender, StateUpdateEventArgs<T> args);

        private T _state;

        public T GetState()
        {
            return _state;
        }

        public void UpdateState(T newState, IDictionary<string, object> extraData = null)
        {
            _state = newState;
            NotifyStateUpdated(extraData);
        }

        public event OnStateUpdatedEvent OnStateUpdated;

        private void NotifyStateUpdated(IDictionary<string, object> extraData)
        {
            OnStateUpdated?.Invoke(this, new StateUpdateEventArgs<T>(_state, extraData));
        }
    }
}