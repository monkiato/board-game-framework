using System.Collections.Generic;
using Monkiato.BoardGame.Core.Seat;

namespace Monkiato.BoardGame.Core.Rules.TurnBased
{
    /*
     * The controller ensures all players have played their turn
     */
    public interface IPlayerTurnController
    {
        /// <summary>
        /// Indicate if players must take turns in order or if they can change the order of their turn (decided by players)
        /// </summary>
        bool ClockwiseTurns { get; set; }

        /// <summary>
        /// the amount of turns per players before the round ends  
        /// </summary>
        int TurnsPerPlayer { get; }
        
        /// <summary>
        /// Current round number, will increase as soon as a round is finished
        /// </summary>
        int CurrentRound { get; }
        
        IPlayer CurrentPlayerTurn { get; }
        
        /// <summary>
        /// Event fired when the round has finished (all player took all their turns)
        /// </summary>
        event OnRoundFinishedEvent OnRoundFinished;

        /// <summary>
        /// Event fired when the turn has changed and a new player is the active player for the current turn
        /// </summary>
        event OnPlayerTurnStartedEvent OnPlayerTurnStartedEvent;
        
        /// <summary>
        /// Event fired when a player has finished their turn (other classes can subscribe to be notified
        /// when EndTurn() is executed)
        /// </summary>
        event OnPlayerTurnFinishedEvent OnPlayerTurnFinishedEvent;
        
        /// <summary>
        /// Add player to the controller, will be the last player to play the round
        /// </summary>
        /// <param name="player">new player being added</param>
        void AddPlayer(IPlayer player);
        
        /// <summary>
        /// Add multiple players to the controller, they will be the last players to play the round, in order as they were passed in the arg
        /// </summary>
        /// <param name="players">new players being added</param>
        void AddPlayers(IList<IPlayer> players);

        
        /// <summary>
        /// Indicate a player has finished their turn
        /// </summary>
        /// <param name="player">player who has finished the turn</param>
        /// <returns>true if controller was able to finish turn for given player, otherwise false (e.g. if passed player was not matching current player turn</returns>
        bool EndTurn(IPlayer player);

        /// <summary>
        /// In some cases is necessary to end the round by an external event, end conditions, etc.
        /// This method is available for those cases
        /// </summary>
        void ForceRoundEnd();

        /// <summary>
        /// Get the current round status for the specified player 
        /// </summary>
        /// <param name="player">the player to evaluate the status</param>
        /// <returns>status for the current round</returns>
        PlayerRoundStatus GetPlayerRoundStatus(IPlayer player);
    }
}