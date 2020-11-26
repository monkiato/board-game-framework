namespace Monkiato.BoardGame.Core.Dice
{
    public interface IDie
    {
        DiceType Type { get; }
        int Result { get; }
        void Roll();
    }
}