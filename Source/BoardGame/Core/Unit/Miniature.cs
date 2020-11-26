using System;
using Monkiato.BoardGame.Core.Seat;

namespace Monkiato.BoardGame.Core.Unit
{
    public class Miniature : IMiniature
    {
        public event EventHandler OnAssignedPlayerChanged;

        public IPlayer Player { get; private set; }
        
        public Miniature(IPlayer player)
        {
            Player = player;
        }

        public bool ChangePlayer(IPlayer player)
        {
            try
            {
                Player?.ReleaseUnit(this);
                Player = player;
                Player.AddUnit(this);
                // TODO: add players information in the event
                OnAssignedPlayerChanged?.Invoke(this, EventArgs.Empty);
            }
            catch (Exception) //TODO: add specific exception thrown by Player class
            {
                //TODO: logging
                return false;
            }

            return true;
        }

        public string GetUnitType()
        {
            return Units.Miniature;
        }
    }
}