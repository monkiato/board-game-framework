using System.Collections.Generic;
using Monkiato.BoardGame.Core.Component;
using NUnit.Framework;

namespace Test.BoardGame.Core.Components
{
    [TestFixture]
    public class GenericComponentTest
    {
        private GenericComponent _component;
        
        private class ScoreSheetLogic : IComponentLogic
        {
        }

        private class TestComponent : GenericComponent
        {
            public TestComponent(IReadOnlyDictionary<string, IComponentLogic> logicMap)
            {
                Init(logicMap);
            }
        }
        
        [SetUp]
        public void SetUp()
        {
            var logicMap = new Dictionary<string, IComponentLogic>
            {
                {"score_sheet", new ScoreSheetLogic()}
            };
            _component = new TestComponent(logicMap);
            
        }

        [Test]
        public void ShouldGetLogic()
        {
            var scoreSheet = _component.GetLogic("score_sheet");
            Assert.IsNotNull(scoreSheet);
            Assert.AreEqual(typeof(ScoreSheetLogic), scoreSheet.GetType());
        }

        [Test]
        public void ShouldGetLogicByTypeOnly()
        {
            var scoreSheet = _component.GetLogic<ScoreSheetLogic>();
            Assert.IsNotNull(scoreSheet);
            Assert.AreEqual(typeof(ScoreSheetLogic), scoreSheet.GetType());
        }
    }
}