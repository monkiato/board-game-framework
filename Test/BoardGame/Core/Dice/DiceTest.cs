using Monkiato.BoardGame.Core.Dice;
using NUnit.Framework;

namespace Test.BoardGame.Core.Dice
{
    [TestFixture]
    public class DiceTest
    {
        [Test]
        public void ShouldCreateD4()
        {
            Assert.IsInstanceOf<D4Die>(DiceFactory.Create(DiceType.D4));
        }
        
        [Test]
        public void ShouldCreateD6()
        {
            Assert.IsInstanceOf<D6Die>(DiceFactory.Create(DiceType.D6));
        }
        
        [Test]
        public void ShouldCreateD8()
        {
            Assert.IsInstanceOf<D8Die>(DiceFactory.Create(DiceType.D8));
        }
        
        [Test]
        public void ShouldCreateD10()
        {
            Assert.IsInstanceOf<D10Die>(DiceFactory.Create(DiceType.D10));
        }
        
        [Test]
        public void ShouldCreateD12()
        {
            Assert.IsInstanceOf<D12Die>(DiceFactory.Create(DiceType.D12));
        }
        
        [Test]
        public void ShouldCreateD20()
        {
            Assert.IsInstanceOf<D20Die>(DiceFactory.Create(DiceType.D20));
        }
    }
}