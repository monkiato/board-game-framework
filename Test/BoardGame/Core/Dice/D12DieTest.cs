using Monkiato.BoardGame.Core.Dice;
using NUnit.Framework;

namespace Test.BoardGame.Core.Dice
{
    [TestFixture]
    public class D12DieTest
    {
        private IDie _die;

        [SetUp]
        public void SetUp()
        {
            _die = new D12Die();
        }

        [Test]
        public void ShouldReturnOneAsDefaultResult()
        {
            Assert.AreEqual(1, _die.Result);
        }

        [Test]
        public void ShouldReturnValidType()
        {
            Assert.AreEqual(DiceType.D12, _die.Type);
        }

        [Test]
        public void ShouldRollAndGetResultBetweenRange()
        {
            // there's a random factor inside the implementation but it check the random result is between the range
            for (int i = 0; i < 10; i++)
            {
                // not a deterministic test, but this will show some stability or unintentional updates across different tests
                _die.Roll();
                Assert.IsTrue(_die.Result >= 1 && _die.Result <= 12);
            }
        }
    }
}