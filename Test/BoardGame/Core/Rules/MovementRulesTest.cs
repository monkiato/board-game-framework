using Monkiato.BoardGame.Core.Rules;
using Monkiato.BoardGame.Core.Table.MapTile;
using Monkiato.BoardGame.Core.Unit;
using NSubstitute;
using NUnit.Framework;

namespace Test.BoardGame.Core.Rules
{
    [TestFixture]
    public class MovementRulesTest
    {
        private MovementRules rules;

        [SetUp]
        public void SetUp()
        {
            rules = new MovementRules(true, true);
        }

        [Test]
        public void ShouldEvaluateValidMoves()
        {
            var board = Substitute.For<IMapTileBoard>();
            var miniature = Substitute.For<IMiniature>();
            var moves = rules.GetValidMoves(board, miniature, 1);
            Assert.AreEqual(8, moves.Count);
        }
    }
}