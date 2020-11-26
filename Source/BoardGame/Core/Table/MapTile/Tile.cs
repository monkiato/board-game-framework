using System;

namespace Monkiato.BoardGame.Core.Table.MapTile
{
    public class Tile : ITile
    {
        public Tile(int x, int y, Enum tileType = default)
        {
            TileType = tileType;
            X = x;
            Y = y;
        }

        public ITileProperties Properties { get; }

        public Enum TileType { get; }
        public double X { get; }
        public double Y { get; }
    }
}