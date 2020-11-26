using System;
using Monkiato.BoardGame.Core.Seat;
using Monkiato.BoardGame.Core.Unit;
using NSubstitute;
using NUnit.Framework;

namespace Test.BoardGame.Core.Unit
{
    [TestFixture]
    public class MiniatureTest
    {
        private Miniature _miniature;
        private IPlayer _player;

        [SetUp]
        public void SetUp()
        {
            _player = Substitute.For<IPlayer>();
            _miniature = new Miniature(_player);
        }

        [Test]
        public void ShouldGetAssignedPlayer()
        {
            Assert.AreEqual(_player, _miniature.Player);
        }

        [Test]
        public void ShouldChangeAssignedPlayer()
        {
            var newPlayer = Substitute.For<IPlayer>();
            var success = _miniature.ChangePlayer(newPlayer);
            Assert.IsTrue(success);
            Assert.AreEqual(newPlayer, _miniature.Player);
        }

        [Test]
        public void ShouldReturnFalseIfCantAssignNewPlayer()
        {
            var newPlayer = Substitute.For<IPlayer>();
            _player.When(p => p.ReleaseUnit(_miniature)).Throw<Exception>();
            var success = _miniature.ChangePlayer(newPlayer);
            Assert.IsFalse(success);
            Assert.AreEqual(_player, _miniature.Player);
        }

        [Test]
        public void ShouldRaiseOnChangePlayerEvent()
        {
            var fired = false;
            //TODO: ensure the event contains player information
            _miniature.OnAssignedPlayerChanged += (sender, args) => fired = true;
            var newPlayer = Substitute.For<IPlayer>();
            var success = _miniature.ChangePlayer(newPlayer);
            Assert.IsTrue(fired);
        }

        [Test]
        public void ShouldCallReleasePlayerMethod()
        {
            var newPlayer = Substitute.For<IPlayer>();
            _miniature.ChangePlayer(newPlayer);
            _player.Received().ReleaseUnit(_miniature);
            _player.DidNotReceive().AddUnit(_miniature);
        }

        [Test]
        public void ShouldCallAddPlayerMethod()
        {
            var newPlayer = Substitute.For<IPlayer>();
            _miniature.ChangePlayer(newPlayer);
            newPlayer.Received().AddUnit(_miniature);
            newPlayer.DidNotReceive().ReleaseUnit(_miniature);
        }

        [Test]
        public void ShouldGetMiniatureType()
        {
            Assert.AreEqual("miniature", _miniature.GetUnitType());
        }
    }
}