using Monkiato.BoardGame.Core.Cards;
using NUnit.Framework;

namespace Test.BoardGame.Core.Cards
{
    [TestFixture]
    public class GenericCardTest
    {
        private GenericCard _card;

        private enum GenericCardTestType
        {
            Water,
            Sun,
            Fire
        }
        
        [SetUp]
        public void SetUp()
        {
            var info = new GenericCardInfo
            {
                Name = "testing card",
                LoreText = "a short description",
                AbilityText = "get a free card",
                Size = CardSize.American,
                Type = GenericCardTestType.Fire,
                MainImage = "image.png",
                BackImage = "back.png",
                FrontBackgroundImage = "front_background.png"
            };
            _card = new GenericCard(info);
        }

        [Test]
        public void ShouldReturnName()
        {
            Assert.AreEqual("testing card", _card.Name);
        }

        [Test]
        public void ShouldReturnLoreText()
        {
            Assert.AreEqual("a short description", _card.LoreText);
        }

        [Test]
        public void ShouldReturnAbilityText()
        {
            Assert.AreEqual("get a free card", _card.AbilityText);
        }

        [Test]
        public void ShouldReturnSize()
        {
            Assert.AreEqual(CardSize.American, _card.Size);
        }

        [Test]
        public void ShouldReturnType()
        {
            Assert.AreEqual(GenericCardTestType.Fire, _card.Type);
        }

        [Test]
        public void ShouldReturnMainImage()
        {
            Assert.AreEqual("image.png", _card.MainImage);
        }

        [Test]
        public void ShouldReturnBackImage()
        {
            Assert.AreEqual("back.png", _card.BackImage);
        }

        [Test]
        public void ShouldReturnFrontBackgroundImage()
        {
            Assert.AreEqual("front_background.png", _card.FrontBackgroundImage);
        }
    }
}