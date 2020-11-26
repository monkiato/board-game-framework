using System.Collections.Generic;
using System.Linq;
using Monkiato.BoardGame.Core.Rules.Exceptions;
using Monkiato.BoardGame.Core.Seat;

namespace Monkiato.BoardGame.Core.Rules.TurnBased
{
    public class PlayerTurnController : IPlayerTurnController
    {
        private readonly Queue<IPlayer> _players = new Queue<IPlayer>();
        private readonly Dictionary<IPlayer, int> _playedInRound = new Dictionary<IPlayer, int>();
        private bool _clockwiseTurns = true;

        public bool ClockwiseTurns
        {
            get => _clockwiseTurns;
            set
            {
                _clockwiseTurns = value;
                if (value) UpdateCurrentPlayerTurn();
                else CurrentPlayerTurn = null;
            }
        }

        public int TurnsPerPlayer { get; }
        public int CurrentRound { get; private set; } = 1;
        public IPlayer CurrentPlayerTurn { get; private set; }
        
        public event OnRoundFinishedEvent OnRoundFinished;
        
        public event OnPlayerTurnStartedEvent OnPlayerTurnStartedEvent;

        public event OnPlayerTurnFinishedEvent OnPlayerTurnFinishedEvent;
        
        public PlayerTurnController(int turnsPerPlayer = 1)
        {
            if (turnsPerPlayer == 0)
            {
                throw new InvalidTurnsPerPlayerException();
            }
            TurnsPerPlayer = turnsPerPlayer;
        }
        
        public void AddPlayer(IPlayer player)
        {
            // call the other method to keep new player logic in a single place
            AddPlayers(new List<IPlayer>{player});
        }

        public void AddPlayers(IList<IPlayer> players)
        {
            foreach (var player in players)
            {
                if (_players.Contains(player))
                {
                    throw new PlayerAlreadyAddedException();
                }
                _players.Enqueue(player);
                _playedInRound[player] = 0;
            }
            UpdateCurrentPlayerTurn();
        }

        public bool EndTurn(IPlayer player)
        {
            if (player == null) return false;
            if (ClockwiseTurns && CurrentPlayerTurn != player) return false;

            // then check if player already exceed turns per player
            if (_playedInRound[player] >= TurnsPerPlayer)
            {
                throw new PlayerReachedMaxTurnsPerRoundException();
            }
            
            // finally increase played turns
            _playedInRound[player]++;
            
            // notify about end of turn event
            OnPlayerTurnFinishedEvent?.Invoke(this, new PlayerTurnArgs(player));
            
            // send player to the back of the queue
            _players.Enqueue(_players.Dequeue());
            CheckRoundEnd();
            UpdateCurrentPlayerTurn();
            return true;
        }

        private void CheckRoundEnd()
        {
            if (_players.Any(player => _playedInRound[player] < TurnsPerPlayer))
            {
                return;
            }

            EndRound();
        }

        private void EndRound()
        {
            OnRoundFinished?.Invoke(this, new RoundArgs());
            foreach (var player in _players)
            {
                _playedInRound[player] = 0;
            }

            CurrentRound++;
        }

        public void ForceRoundEnd()
        {
            var round = CurrentRound;
            // finish player turn first
            EndTurn(CurrentPlayerTurn);
            
            // if round end was not triggered, we force it
            if (CurrentRound == round)
            {
                EndRound();
            }
        }

        public PlayerRoundStatus GetPlayerRoundStatus(IPlayer player)
        {
            if (!_players.Contains(player))
            {
                return PlayerRoundStatus.NotPlaying;
            } 
            
            return _playedInRound[player] < TurnsPerPlayer ? PlayerRoundStatus.PendingToPlay : PlayerRoundStatus.AlreadyPlayed;
        }

        private void UpdateCurrentPlayerTurn()
        {
            if (!ClockwiseTurns) return;
            CurrentPlayerTurn = _players.Peek();
            OnPlayerTurnStartedEvent?.Invoke(this, new PlayerTurnArgs(CurrentPlayerTurn));
        }
    }
}