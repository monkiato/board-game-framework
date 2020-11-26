using System;

namespace Monkiato.BoardGame.Core.Dice
{
    public static class DiceFactory
    {
        public static IDie Create(DiceType type)
        {
            IDie die;
            switch (type)
            {
                case DiceType.D4:
                    die = new D4Die();
                    break;
                case DiceType.D6:
                    die = new D6Die();
                    break;
                case DiceType.D8:
                    die = new D8Die();
                    break;
                case DiceType.D10:
                    die = new D10Die();
                    break;
                case DiceType.D12:
                    die = new D12Die();
                    break;
                case DiceType.D20:
                    die = new D20Die();
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(type), type, null);
            }

            return die;
        }
    }
}