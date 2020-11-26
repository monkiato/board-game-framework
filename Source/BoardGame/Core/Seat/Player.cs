using System.Collections.Generic;
using Monkiato.BoardGame.Core.Unit;

namespace Monkiato.BoardGame.Core.Seat
{
    public class Player : IPlayer
    {
        public string Name { get; }
        
        public Player(string name)
        {
            Name = name;
        }

        public List<IBoardUnit> Units { get; } = new List<IBoardUnit>();

        public void ReleaseUnit(IBoardUnit unit)
        {
            Units.Remove(unit);
        }

        public void AddUnit(IBoardUnit unit)
        {
            Units.Add(unit);
        }
    }
}