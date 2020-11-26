using Monkiato.BoardGame.Core.Seat;
using Monkiato.BoardGame.Core.Unit;
using NSubstitute;
using NUnit.Framework;

namespace Test.BoardGame.Core.Seat
{
    [TestFixture]
    public class PlayerTest
    {
        private Player _player;

        [SetUp]
        public void SetUp()
        {
            _player = new Player("testingPlayer");
        }

        [Test]
        public void ShouldGetPlayerName()
        {
            Assert.AreEqual("testingPlayer", _player.Name);
        }

        [Test]
        public void ShouldAddItem()
        {
            var item = Substitute.For<IBoardUnit>();
            Assert.AreEqual(0, _player.Units.Count);
            _player.AddUnit(item);
            Assert.AreEqual(1, _player.Units.Count);
            Assert.AreEqual(item, _player.Units[0]);
        }

        [Test]
        public void ShouldReleaseItem()
        {
            var item = Substitute.For<IBoardUnit>();
            _player.AddUnit(item);
            Assert.AreEqual(1, _player.Units.Count);
            _player.ReleaseUnit(item);
            Assert.AreEqual(0, _player.Units.Count);
        }
    }
}