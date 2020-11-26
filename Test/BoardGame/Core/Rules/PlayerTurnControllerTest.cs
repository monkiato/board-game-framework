using System.Collections.Generic;
using Monkiato.BoardGame.Core.Rules.Exceptions;
using Monkiato.BoardGame.Core.Rules.TurnBased;
using Monkiato.BoardGame.Core.Seat;
using NSubstitute;
using NUnit.Framework;

namespace Test.BoardGame.Core.Rules
{
    [TestFixture]
    public class PlayerTurnControllerTest
    {
        private PlayerTurnController _playerTurnController;
        private IPlayer _player1;
        private IPlayer _player2;
        private IPlayer _player3;

        [SetUp]
        public void SetUp()
        {
            _playerTurnController = new PlayerTurnController();
            AddBasePlayers();
        }

        [Test]
        public void ShouldFailIfTurnsPerPlayerIsZero()
        {
            Assert.Throws<InvalidTurnsPerPlayerException>(() => _playerTurnController = new PlayerTurnController(0));
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
        public void ShouldPassTurnClockwise()
        {
            // expected round order: p1, p2, p3
            Assert.AreEqual(_player1, _playerTurnController.CurrentPlayerTurn);
            Assert.IsTrue(_playerTurnController.EndTurn(_player1));
            Assert.AreEqual(_player2, _playerTurnController.CurrentPlayerTurn);
            Assert.IsTrue(_playerTurnController.EndTurn(_player2));
            Assert.AreEqual(_player3, _playerTurnController.CurrentPlayerTurn);
        }

        [Test]
        public void ShouldNotAllowPassTurnForWrongPlayer()
        {
            Assert.IsFalse(_playerTurnController.EndTurn(_player2));
        }

        [Test]
        public void ShouldGiveMultipleTurnsPerPlayer()
        {
            _playerTurnController = new PlayerTurnController(2);
            AddBasePlayers();
            var endRoundCalled = false;
            _playerTurnController.OnRoundFinished += (sender, args) => endRoundCalled = true;
            _playerTurnController.EndTurn(_player1);
            _playerTurnController.EndTurn(_player2);
            _playerTurnController.EndTurn(_player3);
            Assert.IsFalse(endRoundCalled);
            _playerTurnController.EndTurn(_player1);
            _playerTurnController.EndTurn(_player2);
            _playerTurnController.EndTurn(_player3);
            Assert.IsTrue(endRoundCalled);
        }

        [Test]
        public void ShouldFireRoundFinishedEvent()
        {
            var endRoundCalled = false;
            _playerTurnController.OnRoundFinished += (sender, args) => endRoundCalled = true;
            _playerTurnController.EndTurn(_player1);
            _playerTurnController.EndTurn(_player2);
            
            Assert.IsFalse(endRoundCalled);
            _playerTurnController.EndTurn(_player3);
            Assert.IsTrue(endRoundCalled);
        }

        [Test]
        public void ShouldFirePlayerTurnFinishedEvent()
        {
            var playerTurnFinished = false;
            _playerTurnController.OnPlayerTurnFinishedEvent += (sender, args) =>
            {
                playerTurnFinished = true;
                Assert.AreEqual(_player1, args.Player);
            };
            _playerTurnController.EndTurn(_player1);
            Assert.IsTrue(playerTurnFinished);
        }

        [Test]
        public void ShouldChangePlayerRoundStatusAfterPlaying()
        {
            Assert.AreEqual(PlayerRoundStatus.PendingToPlay, _playerTurnController.GetPlayerRoundStatus(_player1));
            _playerTurnController.EndTurn(_player1);
            Assert.AreEqual(PlayerRoundStatus.AlreadyPlayed, _playerTurnController.GetPlayerRoundStatus(_player1));
        }

        [Test]
        public void ShouldFirePlayerTurnStartedEvent()
        {
            var playerTurnStarted = false;
            _playerTurnController.OnPlayerTurnStartedEvent += (sender, args) =>
            {
                playerTurnStarted = true;
                Assert.AreEqual(_player2, args.Player);
            };
            _playerTurnController.EndTurn(_player1);
            Assert.IsTrue(playerTurnStarted);
        }

        [Test]
        public void ShouldAddPlayer()
        {
            var newPlayer = Substitute.For<IPlayer>();
            Assert.AreEqual(PlayerRoundStatus.NotPlaying, _playerTurnController.GetPlayerRoundStatus(newPlayer));
            _playerTurnController.AddPlayer(newPlayer);
            Assert.AreEqual(PlayerRoundStatus.PendingToPlay, _playerTurnController.GetPlayerRoundStatus(newPlayer));
        }

        [Test]
        public void ShouldAddPlayers()
        {
            var newPlayer1 = Substitute.For<IPlayer>();
            var newPlayer2 = Substitute.For<IPlayer>();
            Assert.AreEqual(PlayerRoundStatus.NotPlaying, _playerTurnController.GetPlayerRoundStatus(newPlayer1));
            Assert.AreEqual(PlayerRoundStatus.NotPlaying, _playerTurnController.GetPlayerRoundStatus(newPlayer2));
            _playerTurnController.AddPlayers(new List<IPlayer>{newPlayer1, newPlayer2});
            Assert.AreEqual(PlayerRoundStatus.PendingToPlay, _playerTurnController.GetPlayerRoundStatus(newPlayer1));
            Assert.AreEqual(PlayerRoundStatus.PendingToPlay, _playerTurnController.GetPlayerRoundStatus(newPlayer2));
        }

        [Test]
        public void ShouldNotAddExistingPlayer()
        {
            var newPlayer = Substitute.For<IPlayer>();
            _playerTurnController.AddPlayer(newPlayer);
            Assert.Throws<PlayerAlreadyAddedException>(() => _playerTurnController.AddPlayer(newPlayer));
        }

        [Test]
        public void ShouldNotAddExistingPlayers()
        {
            var newPlayer1 = Substitute.For<IPlayer>();
            var newPlayer2 = Substitute.For<IPlayer>();
            _playerTurnController.AddPlayers(new List<IPlayer>{newPlayer1, newPlayer2});
            Assert.Throws<PlayerAlreadyAddedException>(() => _playerTurnController.AddPlayers(new List<IPlayer>{newPlayer1, newPlayer2}));
        }

        [Test]
        public void ShouldEndTurn()
        {
            Assert.AreEqual(_player1, _playerTurnController.CurrentPlayerTurn);
            Assert.AreEqual(PlayerRoundStatus.PendingToPlay, _playerTurnController.GetPlayerRoundStatus(_player1));
            _playerTurnController.EndTurn(_player1);
            Assert.AreEqual(_player2, _playerTurnController.CurrentPlayerTurn);
            Assert.AreEqual(PlayerRoundStatus.AlreadyPlayed, _playerTurnController.GetPlayerRoundStatus(_player1));
        }

        [Test]
        public void ShouldForceRoundEnd()
        {
            _playerTurnController.EndTurn(_player1);
            Assert.AreEqual(PlayerRoundStatus.AlreadyPlayed, _playerTurnController.GetPlayerRoundStatus(_player1));
            Assert.AreEqual(1, _playerTurnController.CurrentRound);
            _playerTurnController.ForceRoundEnd();
            // ensure player status has been refreshed
            Assert.AreEqual(PlayerRoundStatus.PendingToPlay, _playerTurnController.GetPlayerRoundStatus(_player1));
            // ensure there's a new round 
            Assert.AreEqual(2, _playerTurnController.CurrentRound);
        }

        [Test]
        public void ShouldFireRoundFinishedEventOnForcedRoundEnd()
        {
            var endRoundCalled = false;
            _playerTurnController.OnRoundFinished += (sender, args) => endRoundCalled = true;
            _playerTurnController.EndTurn(_player1);
            
            Assert.IsFalse(endRoundCalled);
            _playerTurnController.ForceRoundEnd();
            Assert.IsTrue(endRoundCalled);
        }

        [Test]
        public void ShouldFireRoundFinishedOnceIfNormalRoundEndIsTriggeredByForcedRoundEnd()
        {
            var endRoundCalled = 0;
            _playerTurnController.OnRoundFinished += (sender, args) => endRoundCalled++;
            _playerTurnController.EndTurn(_player1);
            _playerTurnController.EndTurn(_player2);
            
            Assert.AreEqual(0, endRoundCalled);
            Assert.AreEqual(1, _playerTurnController.CurrentRound);
            _playerTurnController.ForceRoundEnd();
            Assert.AreEqual(1, endRoundCalled);
            Assert.AreEqual(2, _playerTurnController.CurrentRound);
        }
    }
}