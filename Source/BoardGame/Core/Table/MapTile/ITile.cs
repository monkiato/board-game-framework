using System;

namespace Monkiato.BoardGame.Core.Table.MapTile
{
    public interface ITile
    {
        ITileProperties Properties { get; }
        Enum TileType { get; }
        double X { get; }
        double Y { get; }
    }
}