using System.Collections.Generic;
using Monkiato.BoardGame.Core.Unit;

namespace Monkiato.BoardGame.Core.Seat
{
    public interface IPlayer
    {
        /// <summary>
        /// Player name
        /// </summary>
        string Name { get; }
        
        /// <summary>
        /// List of units owned by the the player (miniatures, or figures)
        /// </summary>
        List<IBoardUnit> Units { get; }

        /// <summary>
        /// Remove unit from the list
        /// </summary>
        /// <param name="unit">unit to remove</param>
        void ReleaseUnit(IBoardUnit unit);
        
        /// <summary>
        /// Add new unit to the list
        /// </summary>
        /// <param name="unit">unit to add</param>
        void AddUnit(IBoardUnit unit);
    }
}