using System.Collections.Generic;
using Monkiato.BoardGame.Core.Unit;
using Monkiato.Core;

namespace Monkiato.BoardGame.Core.Table.MapTile
{
    public interface IMapTileBoard
    {
        Point Size { get; }
        event Events.TileChangedEventHandler OnMiniaturePositionChanged;
        ITile GetTile(int x, int y);

        List<IBoardUnit> GetItems();

        List<IBoardUnit> GetItems(int x, int y);

        void AddItem(IBoardUnit unit, int x, int y);

        Point GetItemPosition(IBoardUnit unit);

        bool MoveItem(IBoardUnit unit, int x, int y);
    }
}