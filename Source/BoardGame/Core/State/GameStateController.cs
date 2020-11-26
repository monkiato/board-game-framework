using System;
using System.Collections.Generic;
using Monkiato.BoardGame.Core.Rules.TurnBased;
using Monkiato.BoardGame.Core.Seat;
using Monkiato.Core.Collections;

namespace Monkiato.BoardGame.Core.State
{
    public abstract class GameStateController<T> : IGameStateController<T> where T : Enum
    {
        public PlayerTurnController PlayerTurnController { get; }

        public IGameState<T> GameState { get; }

        private Dictionary<Enum, IStateExecutor<T>> _stateExecutors;

        public GameStateController(GameState<T> gameState, IList<IPlayer> players)
        {
            //shuffle before assigning round order
            players.Shuffle();

            PlayerTurnController = new PlayerTurnController();
            PlayerTurnController.AddPlayers(players);

            gameState.OnStateUpdated += OnStateUpdated;
            GameState = gameState;
        }

        protected void InitStateExecutors(Dictionary<Enum, IStateExecutor<T>> stateExecutors)
        {
            _stateExecutors = stateExecutors;
        }

        protected virtual void OnStateUpdated(object sender, StateUpdateEventArgs<T> args)
        {
            if (_stateExecutors == null)
            {
                throw new Exception("state executors were not initialized");
            }

            if (!_stateExecutors.ContainsKey(args.NewState))
            {
                throw new Exception("state executor not found for state " + args.NewState);
            }

            _stateExecutors[args.NewState].Execute(GameState, sender, args.Data);
        }
    }
}