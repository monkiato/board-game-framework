using System;

namespace Monkiato.BoardGame.Core.Cards
{
    public interface ICard
    {
        string Name { get; }
        Enum Type { get; }
    }
}