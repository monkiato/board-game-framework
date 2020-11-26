using System;
using Monkiato.BoardGame.Core.Table.MapTile;
using Monkiato.BoardGame.Core.Unit;
using Monkiato.Core;
using NSubstitute;
using NUnit.Framework;

namespace Test.BoardGame.Core.Table
{
    [TestFixture]
    public class BoardTest
    {
        private MapTileMapTileBoard _mapTileMapTileBoard;

        [SetUp]
        public void SetUp()
        {
            // width: 5, height: 6
            var size = Point.New(5, 6);
            _mapTileMapTileBoard = new MapTileMapTileBoard(size);
        }
        
        [Test]
        public void ShouldGetBoardSize()
        {
            var size = _mapTileMapTileBoard.Size;
            Assert.AreEqual(5, size.X);
            Assert.AreEqual(6, size.Y);
        }
        
        [Test]
        public void ShouldGetTile()
        {
            var tile = _mapTileMapTileBoard.GetTile(2, 3);
            Assert.AreEqual(2,  tile.X);
            Assert.AreEqual(3,  tile.Y);
            // unassigned tile type
            Assert.AreEqual(null, tile.TileType);
        }
        
        [Test]
        public void ShouldGetLastTile()
        {
            //this test ensures it was initialized with x=4 and y=5 and were not inverted
            var tile = _mapTileMapTileBoard.GetTile(4, 5); // size of board is 5x6, we get tile using index zero
            Assert.AreEqual(4,  tile.X);
            Assert.AreEqual(5,  tile.Y);
            // unassigned tile type
            Assert.AreEqual(null, tile.TileType);
        }
        
        [Test]
        public void ShouldReturnNullIfGetTileOutOfBounds()
        {
            var tile = _mapTileMapTileBoard.GetTile(10, 10);
            Assert.IsNull(tile);
        }
        
        [Test]
        public void ShouldConstructBoardWithPredefinedTileTypeMapping()
        {
            Enum[,] mapTileType = {
                {TestingTileType.Wall, TestingTileType.Wall, TestingTileType.Wall, TestingTileType.Wall},
                {TestingTileType.Wall, TestingTileType.Water, TestingTileType.Water, TestingTileType.Wall},
                {TestingTileType.Wall, TestingTileType.Water, TestingTileType.Water, TestingTileType.Wall},
                {TestingTileType.Wall, TestingTileType.Wall, TestingTileType.Wall, TestingTileType.Wall}
            };
            
            _mapTileMapTileBoard = new MapTileMapTileBoard(mapTileType);
            
            Assert.AreEqual(4, _mapTileMapTileBoard.Size.X);
            Assert.AreEqual(4, _mapTileMapTileBoard.Size.Y);
            Assert.AreEqual(TestingTileType.Wall, _mapTileMapTileBoard.GetTile(0, 0).TileType);
            Assert.AreEqual(TestingTileType.Water, _mapTileMapTileBoard.GetTile(1, 1).TileType);
            Assert.AreEqual(TestingTileType.Wall, _mapTileMapTileBoard.GetTile(3, 3).TileType);
        }
        
        [Test]
        public void ShouldGetZeroMiniatures()
        {
            var miniatures = _mapTileMapTileBoard.GetItems();
            Assert.AreEqual(0, miniatures.Count);
        }
        
        [Test]
        public void ShouldAddMiniatures()
        {
            var miniature = Substitute.For<IMiniature>();
            _mapTileMapTileBoard.AddItem(miniature, 2, 3);
            var miniatures = _mapTileMapTileBoard.GetItems();
            Assert.AreEqual(1, miniatures.Count);
            Assert.AreEqual(miniature, miniatures[0]);
        }
        
        [Test]
        public void ShouldGetMiniatureAtPosition()
        {
            var miniature = Substitute.For<IMiniature>();
            _mapTileMapTileBoard.AddItem(miniature, 2, 3);
            var miniatures = _mapTileMapTileBoard.GetItems(2, 3);
            Assert.AreEqual(1, miniatures.Count);
            Assert.AreEqual(miniature, miniatures[0]);
            //ensure miniature is not being returned in a different position
            miniatures = _mapTileMapTileBoard.GetItems(0, 0);
            Assert.AreEqual(0, miniatures.Count);
        }

        [Test]
        public void ShouldGetMiniaturePosition()
        {
            var miniature = Substitute.For<IMiniature>();
            _mapTileMapTileBoard.AddItem(miniature, 2, 3);
            var pos = _mapTileMapTileBoard.GetItemPosition(miniature);
            Assert.AreEqual(2, pos.X);
            Assert.AreEqual(3, pos.Y);
        }

        [Test]
        public void ShouldMoveMiniatureToANewTile()
        {
            var miniature = Substitute.For<IMiniature>();
            _mapTileMapTileBoard.AddItem(miniature, 0, 0);
            var success =_mapTileMapTileBoard.MoveItem(miniature, 2, 3);
            Assert.IsTrue(success);
            var pos = _mapTileMapTileBoard.GetItemPosition(miniature);
            Assert.AreEqual(2, pos.X);
            Assert.AreEqual(3, pos.Y);
        }

        [Test]
        public void ShouldNotMoveMiniatureToAnOutOfBoundPosition()
        {
            var miniature = Substitute.For<IMiniature>();
            _mapTileMapTileBoard.AddItem(miniature, 0, 0);
            var success =_mapTileMapTileBoard.MoveItem(miniature, 10, 10);
            Assert.IsFalse(success);
            var pos = _mapTileMapTileBoard.GetItemPosition(miniature);
            Assert.AreEqual(0, pos.X);
            Assert.AreEqual(0, pos.Y);
        }
    }
}