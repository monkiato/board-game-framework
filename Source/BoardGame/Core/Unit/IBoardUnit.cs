using System;
using Monkiato.BoardGame.Core.Seat;

namespace Monkiato.BoardGame.Core.Unit
{
    public interface IBoardUnit
    {
        /// <summary>
        /// Player associated with the unit, null if the unit is not associated with any particular player
        /// (e.g. color specific to a player, or IA that is not controlled by a player).
        /// Having an associated player doesn't mean the player owns the unit, the unit can be attached to a
        /// different player that is not the owner of the unit (e.g. a miniature captured by another player)
        /// </summary>
        IPlayer Player { get; }
        
        event EventHandler OnAssignedPlayerChanged;

        bool ChangePlayer(IPlayer player);

        string GetUnitType();
    }
}