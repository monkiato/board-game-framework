using Monkiato.BoardGame.Core.Unit;

namespace Monkiato.BoardGame.Core.Table.MapTile
{
    public class TileChangedArgs
    {
        public TileChangedArgs(IBoardUnit boardUnit, Tile fromTile, Tile toTile)
        {
            BoardUnit = boardUnit;
            FromTile = fromTile;
            ToTile = toTile;
        }

        public IBoardUnit BoardUnit { get; }
        public Tile FromTile { get; }
        public Tile ToTile { get; }
    }
}