using System.Collections.Generic;
using Monkiato.BoardGame.Core.Cards;
using NSubstitute;
using NUnit.Framework;

namespace Test.BoardGame.Core.Cards
{
    [TestFixture]
    public class DeckTest
    {
        private Deck _deck;

        private enum TestCardType
        {
            GenericType1,
            GenericType2,
            GenericType3,
            GenericType4,
            GenericType5
        }

        [SetUp]
        public void SetUp()
        {
            var cards = new List<ICard>
            {
                new GenericCard(new GenericCardInfo {Name = "test1", Type = TestCardType.GenericType1}),
                new GenericCard(new GenericCardInfo {Name = "test2", Type = TestCardType.GenericType2}),
                new GenericCard(new GenericCardInfo {Name = "test3", Type = TestCardType.GenericType3}),
                new GenericCard(new GenericCardInfo {Name = "test4", Type = TestCardType.GenericType4})
            };
            _deck = new Deck(cards);
        }

        [Test]
        public void ShouldContainInitialCards()
        {
            Assert.AreEqual(4, _deck.Count());
        }

        [Test]
        public void ShouldInitializeEmptyDeck()
        {
            _deck = new Deck(20);
            Assert.AreEqual(0, _deck.Count());
        }

        [Test]
        public void ShouldDrawTopCard()
        {
            var card = _deck.DrawTop();
            Assert.AreEqual(3, _deck.Count());
            Assert.AreEqual("test4", card.Name);
        }

        [Test]
        public void ShouldNotDrawTopCardIfEmpty()
        {
            _deck = new Deck(20);
            var card = _deck.DrawTop();
            Assert.AreEqual(0, _deck.Count());
            Assert.IsNull(card);
        }

        [Test]
        public void ShouldDrawTopCards()
        {
            var cards = _deck.DrawTop(2);
            Assert.AreEqual(2, _deck.Count());
            Assert.AreEqual("test3", cards[0].Name);
            Assert.AreEqual("test4", cards[1].Name);
        }

        [Test]
        public void ShouldNotDrawCardsIfAboveAvailableAmount()
        {
            var cards = _deck.DrawTop(5);
            Assert.AreEqual(4, _deck.Count());
            Assert.IsNull(cards);
        }

        [Test]
        public void ShouldDrawBottomCard()
        {
            var card = _deck.DrawBottom();
            Assert.AreEqual(3, _deck.Count());
            Assert.AreEqual("test1", card.Name);
        }

        [Test]
        public void ShouldNotDrawBottomCardIfEmpty()
        {
            _deck = new Deck(20);
            var card = _deck.DrawBottom();
            Assert.AreEqual(0, _deck.Count());
            Assert.IsNull(card);
        }

        [Test]
        public void ShouldDrawBottomCards()
        {
            var cards = _deck.DrawBottom(2);
            Assert.AreEqual(2, _deck.Count());
            Assert.AreEqual("test1", cards[0].Name);
            Assert.AreEqual("test2", cards[1].Name);
        }

        [Test]
        public void ShouldNotDrawBottomCardsIfAboveAvailableAmount()
        {
            var cards = _deck.DrawBottom(5);
            Assert.AreEqual(4, _deck.Count());
            Assert.IsNull(cards);
        }

        [Test]
        public void ShouldAddCardOnTop()
        {
            var card = Substitute.For<ICard>();
            _deck.AddTop(card);
            Assert.AreEqual(5, _deck.Count());
            Assert.AreEqual(card, _deck.DrawTop());
        }

        [Test]
        public void ShouldNotAddCardOnTopIfNull()
        {
            _deck.AddTop(null);
            Assert.AreEqual(4, _deck.Count());
        }

        [Test]
        public void ShouldAddCardOnBottom()
        {
            var card = Substitute.For<ICard>();
            _deck.AddBottom(card);
            Assert.AreEqual(5, _deck.Count());
            Assert.AreEqual(card, _deck.DrawBottom());
        }

        [Test]
        public void ShouldNotAddCardOnBottomIfNull()
        {
            _deck.AddBottom(null);
            Assert.AreEqual(4, _deck.Count());
        }

        [Test]
        public void ShouldFindByName()
        {
            var card = _deck.FindCard("test1");
            Assert.AreEqual(4, _deck.Count());
            Assert.IsNotNull(card);
            Assert.AreEqual("test1", card.Name);
        }

        [Test]
        public void ShouldNotFindByName()
        {
            var card = _deck.FindCard("test1fake");
            Assert.AreEqual(4, _deck.Count());
            Assert.IsNull(card);
        }

        [Test]
        public void ShouldFindByType()
        {
            var card = _deck.FindCard(TestCardType.GenericType2);
            Assert.AreEqual(4, _deck.Count());
            Assert.IsNotNull(card);
            Assert.AreEqual("test2", card.Name);
        }

        [Test]
        public void ShouldNotFindByType()
        {
            var card = _deck.FindCard(TestCardType.GenericType5);
            Assert.AreEqual(4, _deck.Count());
            Assert.IsNull(card);
        }

        [Test]
        public void ShouldCastCardCorrectlyOnFindByName()
        {
            var card = _deck.FindCard<GenericCard>("test1");
            Assert.AreEqual(4, _deck.Count());
            Assert.IsNotNull(card);
            Assert.AreEqual("test1", card.Name);
        }

        [Test]
        public void ShouldCastCardCorrectlyOnFindByType()
        {
            var card = _deck.FindCard<GenericCard>(TestCardType.GenericType2);
            Assert.AreEqual(4, _deck.Count());
            Assert.IsNotNull(card);
            Assert.AreEqual("test2", card.Name);
        }

        [Test]
        public void ShouldNotLoseCardsOnShuffle()
        {
            _deck.Shuffle();
            Assert.AreEqual(4, _deck.Count());
            Assert.IsNotNull(_deck.FindCard("test1"));
            Assert.IsNotNull(_deck.FindCard("test2"));
            Assert.IsNotNull(_deck.FindCard("test3"));
            Assert.IsNotNull(_deck.FindCard("test4"));
        }

        [Test]
        public void ShouldAddCardsFromAnotherDeck()
        {
            var extraCards = new List<ICard>
            {
                new GenericCard(new GenericCardInfo {Name = "test5", Type = TestCardType.GenericType1}),
                new GenericCard(new GenericCardInfo {Name = "test6", Type = TestCardType.GenericType2})
            };
            var otherDeck = new Deck(extraCards);
            _deck.AddCards(otherDeck);
            Assert.AreEqual(6, _deck.Count());
            Assert.AreEqual(2, otherDeck.Count());
            Assert.IsNotNull(_deck.FindCard("test1"));
            Assert.IsNotNull(_deck.FindCard("test2"));
            Assert.IsNotNull(_deck.FindCard("test3"));
            Assert.IsNotNull(_deck.FindCard("test4"));
            Assert.IsNotNull(_deck.FindCard("test5"));
            Assert.IsNotNull(_deck.FindCard("test6"));
        }

        [Test]
        public void ShouldAddCardsFromAnotherDeckAndCleanFromPreviousDeck()
        {
            var extraCards = new List<ICard>
            {
                new GenericCard(new GenericCardInfo {Name = "test5", Type = TestCardType.GenericType1}),
                new GenericCard(new GenericCardInfo {Name = "test6", Type = TestCardType.GenericType2})
            };
            var otherDeck = new Deck(extraCards);
            _deck.AddCards(otherDeck, true);
            Assert.AreEqual(6, _deck.Count());
            Assert.AreEqual(0, otherDeck.Count());
            Assert.IsNotNull(_deck.FindCard("test1"));
            Assert.IsNotNull(_deck.FindCard("test2"));
            Assert.IsNotNull(_deck.FindCard("test3"));
            Assert.IsNotNull(_deck.FindCard("test4"));
            Assert.IsNotNull(_deck.FindCard("test5"));
            Assert.IsNotNull(_deck.FindCard("test6"));
        }
        
    }
}