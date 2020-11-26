using System;

namespace Monkiato.BoardGame.Core.Dice
{
    public abstract class AbstractDie : IDie
    {
        public DiceType Type { get; }

        public int Result { get; private set; } = 1;

        private readonly Random _random;
        private readonly int _maxValue;

        protected AbstractDie(DiceType type, int maxValue)
        {
            Type = type;
            _maxValue = maxValue;
            _random = new Random();
        }
        
        public void Roll()
        {
            Result = _random.Next() % _maxValue + 1; // index 1 result
        }
    }
}