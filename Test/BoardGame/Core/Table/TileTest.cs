using Monkiato.BoardGame.Core.Table.MapTile;
using NUnit.Framework;

namespace Test.BoardGame.Core.Table
{
    [TestFixture]
    public class TileTest
    {
        private Tile _tile;

        [SetUp]
        public void SetUp()
        {
            _tile = new Tile(4, 6);
        }

        [Test]
        public void ShouldGetXPosition()
        {
            Assert.AreEqual(4, _tile.X);
        }

        [Test]
        public void ShouldGetYPosition()
        {
            Assert.AreEqual(6, _tile.Y);
        }
    }
}