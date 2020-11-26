using System;
using System.Collections.Generic;
using System.Linq;
using Monkiato.BoardGame.Core.Unit;
using Monkiato.Core;

namespace Monkiato.BoardGame.Core.Table.MapTile
{
    public class MapTileMapTileBoard : IMapTileBoard
    {
        private readonly Dictionary<IBoardUnit, Point> _itemPositions = new Dictionary<IBoardUnit, Point>();
        private readonly List<IBoardUnit> _items = new List<IBoardUnit>();
        private readonly Tile[,] _tiles;

        public MapTileMapTileBoard(Point size)
        {
            Size = size;
            _tiles = new Tile[Size.X, Size.Y];
            InitializeDefaultTiles();
        }

        public MapTileMapTileBoard(Enum[,] mapTileType)
        {
            Size = Point.New(mapTileType.GetLength(0), mapTileType.GetLength(1));
            _tiles = new Tile[Size.X, Size.Y];
            InitializeTiles(mapTileType);
        }

        event Events.TileChangedEventHandler IMapTileBoard.OnMiniaturePositionChanged
        {
            add => throw new NotImplementedException();
            remove => throw new NotImplementedException();
        }

        public Point Size { get; }

        public ITile GetTile(int x, int y)
        {
            try
            {
                return _tiles[x, y];
            }
            catch (IndexOutOfRangeException)
            {
                return null;
            }
        }

        public List<IBoardUnit> GetItems()
        {
            return _items;
        }

        public List<IBoardUnit> GetItems(int x, int y)
        {
            return (from keyValuePair in _itemPositions 
                where keyValuePair.Value.X == x && keyValuePair.Value.Y == y
                select keyValuePair.Key).ToList();
        }

        public void AddItem(IBoardUnit unit, int x, int y)
        {
            _items.Add(unit);
            MoveItem(unit, x, y);
        }

        public Point GetItemPosition(IBoardUnit unit)
        {
            return !_itemPositions.ContainsKey(unit) ? null : _itemPositions[unit];
        }

        public bool MoveItem(IBoardUnit unit, int x, int y)
        {
            if (!IsPositionValid(x, y)) return false;

            _itemPositions[unit] = Point.New(x, y);
            return true;
        }

        private void InitializeDefaultTiles()
        {
            for (var x = 0; x < _tiles.GetLength(0); x++)
            for (var y = 0; y < _tiles.GetLength(1); y++)
                _tiles[x, y] = new Tile(x, y);
        }

        private void InitializeTiles(Enum[,] mapTileType)
        {
            // Initialize tiles with types passed by param
            for (var x = 0; x < _tiles.GetLength(0); x++)
            for (var y = 0; y < _tiles.GetLength(1); y++)
                _tiles[x, y] = new Tile(x, y, mapTileType[x, y]);
        }

        private bool IsPositionValid(int x, int y)
        {
            return GetTile(x, y) != null;
        }
    }
}