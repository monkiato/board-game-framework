using System;

namespace Monkiato.BoardGame.Core.Cards
{
    /// <summary>
    /// Base info parsed from config, it's used to populate a GenericCard and keep data protected and read-only for the card
    /// </summary>
    public class GenericCardInfo
    {
        public string Name { get; set; }
        public Enum Type { get; set; }
        
        // Card texts
        public string LoreText { get; set; }
        public string AbilityText { get; set; }
        
        // Card images
        public string FrontBackgroundImage { get; set; }
        public string BackImage { get; set; }
        public string MainImage { get; set; }
        
        public CardSize Size { get; set; }
    }
}