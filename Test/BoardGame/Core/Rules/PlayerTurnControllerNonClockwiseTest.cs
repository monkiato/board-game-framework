using Monkiato.BoardGame.Core.Rules.TurnBased;
using Monkiato.BoardGame.Core.Seat;
using NSubstitute;
using NUnit.Framework;

namespace Test.BoardGame.Core.Rules
{
    [TestFixture]
    public class PlayerTurnControllerNonClockwiseTest
    {
        private PlayerTurnController _playerTurnController;
        private IPlayer _player1;
        private IPlayer _player2;
        private IPlayer _player3;

        [SetUp]
        public void SetUp()
        {
            _playerTurnController = new PlayerTurnController();
            _playerTurnController.ClockwiseTurns = false;
            AddBasePlayers();
        }

        private void AddBasePlayers()
        {
            _player1 = Substitute.For<IPlayer>();
            _player1.Name.Returns("Player 1");
            _player2 = Substitute.For<IPlayer>();
            _player2.Name.Returns("Player 2");
            _player3 = Substitute.For<IPlayer>();
            _player3.Name.Returns("Player 3");
            _playerTurnController.AddPlayer(_player1);
            _playerTurnController.AddPlayer(_player2);
            _playerTurnController.AddPlayer(_player3);
        }

        [Test]
        public void ShouldNotSetCurrentPlayerTurn()
        {
            // expected round order: p1, p2, p3
            Assert.IsNull(_playerTurnController.CurrentPlayerTurn);
            Assert.IsTrue(_playerTurnController.EndTurn(_player1));
            Assert.IsNull(_playerTurnController.CurrentPlayerTurn);
            Assert.IsTrue(_playerTurnController.EndTurn(_player2));
            Assert.IsNull(_playerTurnController.CurrentPlayerTurn);
        }

        [Test]
        public void ShouldAllowTurnsInMixedOrder()
        {
            // player 2 -> 3 -> 1
            Assert.IsTrue(_playerTurnController.EndTurn(_player2));
            Assert.IsTrue(_playerTurnController.EndTurn(_player3));
            Assert.IsTrue(_playerTurnController.EndTurn(_player1));
            
            SetUp();
            // player 3 -> 1 -> 2
            Assert.IsTrue(_playerTurnController.EndTurn(_player3));
            Assert.IsTrue(_playerTurnController.EndTurn(_player1));
            Assert.IsTrue(_playerTurnController.EndTurn(_player2));
        }

        [Test]
        public void ShouldGiveMultipleTurnsPerPlayer()
        {
            _playerTurnController = new PlayerTurnController(2) {ClockwiseTurns = false};
            AddBasePlayers();
            var endRoundCalled = false;
            _playerTurnController.OnRoundFinished += (sender, args) => endRoundCalled = true;
            
            _playerTurnController.EndTurn(_player3);
            _playerTurnController.EndTurn(_player1);
            _playerTurnController.EndTurn(_player2);
            Assert.IsFalse(endRoundCalled);
            _playerTurnController.EndTurn(_player3);
            _playerTurnController.EndTurn(_player2);
            _playerTurnController.EndTurn(_player1);
            Assert.IsTrue(endRoundCalled);
        }
    }
}