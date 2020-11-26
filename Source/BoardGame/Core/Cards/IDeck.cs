using System;
using System.Collections.Generic;

namespace Monkiato.BoardGame.Core.Cards
{
    public interface IDeck
    {
        /// <summary>
        /// Get top card from the deck, card will be removed from the deck as if it was taken by a player
        /// </summary>
        /// <returns>the card will be returned or null if deck is empty</returns>
        ICard DrawTop();
        
        /// <summary>
        /// Get top cards from the deck, cards will be removed from the deck as if they were taken by a player
        /// </summary>
        /// <param name="amount">amount of cards to be taken</param>
        /// <returns>the cards will be returned or null if deck is empty</returns>
        IList<ICard> DrawTop(int amount);
        
        /// <summary>
        /// Get card from the bottom of the deck, card will be removed from the deck as if it was taken by a player
        /// </summary>
        /// <returns>the card will be returned or null if deck is empty</returns>
        ICard DrawBottom();

        /// <summary>
        /// Get list of cards from the bottom of the deck, cards will be removed from the deck as if they were taken by a player
        /// </summary>
        /// <param name="amount">amount of cards to be taken</param>
        /// <returns>the list of cards will be returned or null if not enough cards are available in deck</returns>
        IList<ICard> DrawBottom(int amount);

        /// <summary>
        /// Add new card at the top of the deck
        /// </summary>
        /// <param name="card">card to be added</param>
        void AddTop(ICard card);
        
        /// <summary>
        /// Add new card at the bottom of the deck
        /// </summary>
        /// <param name="card">card to be added</param>
        void AddBottom(ICard card);
        
        /// <summary>
        /// Shuffle all cards randomly
        /// </summary>
        void Shuffle();
        
        /// <summary>
        /// Find card by name, if there are multiple cards with the same name it will return the first one found in the deck
        /// </summary>
        /// <param name="cardName">name of the card to find</param>
        /// <returns>the card if found, otherwise null will be returned</returns>
        ICard FindCard(string cardName);

        /// <summary>
        /// Find card by name, if there are multiple cards with the same name it will return the first one found in the deck
        /// </summary>
        /// <param name="cardName">name of the card to find</param>
        /// <returns>the card if found casted to the specified Generic Type, otherwise null will be returned</returns>
        T FindCard<T>(string cardName);
        
        /// <summary>
        /// Find card by type, if there are multiple cards with the same name it will return the first one found in the deck
        /// </summary>
        /// <param name="cardType">type of the card to find</param>
        /// <returns>the card if found, otherwise null will be returned</returns>
        ICard FindCard(Enum cardType);
        
        /// <summary>
        /// Find card by type, if there are multiple cards with the same name it will return the first one found in the deck
        /// </summary>
        /// <param name="cardType">type of the card to find</param>
        /// <returns>the card if found casted to the specified Generic Type, otherwise null will be returned</returns>
        T FindCard<T>(Enum cardType);
        
        /// <summary>
        /// Get the amount of cards in the deck
        /// </summary>
        /// <returns>the amount of cards</returns>
        int Count();
    }
}