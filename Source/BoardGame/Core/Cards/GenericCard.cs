using System;

namespace Monkiato.BoardGame.Core.Cards
{
    public class GenericCard : ICard
    {
        private GenericCardInfo _info;

        public GenericCard(GenericCardInfo info)
        {
            _info = info;
        }
        public string Name => _info.Name;
        public Enum Type => _info.Type;
        
        // Card texts
        public string LoreText => _info.LoreText;
        public string AbilityText => _info.AbilityText;
        
        // Card images
        public string FrontBackgroundImage => _info.FrontBackgroundImage;
        public string BackImage => _info.BackImage;
        public string MainImage => _info.MainImage;
        
        public CardSize Size => _info.Size;
    }
}