using System;
using System.Collections.Generic;
using System.Linq;

namespace Monkiato.BoardGame.Core.Cards
{
    public class Deck : IDeck
    {
        private static readonly Random random = new Random();
        private List<ICard> _cards; 

        public Deck(List<ICard> cards)
        {
            _cards = cards;
        }

        public Deck(int size)
        {
            _cards = new List<ICard> {Capacity = size};
        }
        
        public ICard DrawTop()
        {
            if (_cards.Count == 0)
            {
                //TODO: throw exception?
                return null;
            }
            var foundCard = _cards.Last();
            _cards.RemoveAt(_cards.Count - 1);
            return foundCard;
        }
        
        public IList<ICard> DrawTop(int amount)
        {
            if (_cards.Count < amount)
            {
                //TODO: throw exception?
                return null;
            }
            // get last X amount of cards
            var foundCards = _cards.Skip(_cards.Count - amount).ToList();
            // remove cards from deck
            _cards.RemoveRange(_cards.Count - amount, amount);

            return foundCards;
        }

        public ICard DrawBottom()
        {
            if (_cards.Count == 0)
            {
                //TODO: throw exception?
                return null;
            }
            var foundCard = _cards.First();
            _cards.RemoveAt(0);
            return foundCard;
        }
        
        public IList<ICard> DrawBottom(int amount)
        {
            if (_cards.Count < amount)
            {
                //TODO: throw exception?
                return null;
            }
            // get last X amount of cards
            var foundCards = _cards.Take(amount).ToList();
            // remove cards from deck
            _cards.RemoveRange(0, amount);

            return foundCards;
        }

        public void AddTop(ICard card)
        {
            if (card == null)
            {
                //TODO: throw exception
                return;
            }
            _cards.Add(card);
        }

        public void AddBottom(ICard card)
        {
            if (card == null)
            {
                //TODO: throw exception
                return;
            }
            _cards.Insert(0, card);
        }

        public void Shuffle()
        {
            _cards = _cards.OrderBy(card => random.Next()).ToList();
        }

        public ICard FindCard(string cardName)
        {
            return _cards.Find(card => cardName.Equals(card.Name));
        }

        public T FindCard<T>(string cardName)
        {
            return (T) FindCard(cardName);
        }

        public ICard FindCard(Enum cardType)
        {
            return _cards.Find(card => cardType.Equals(card.Type));
        }

        public T FindCard<T>(Enum cardType)
        {
            return (T) FindCard(cardType);
        }

        public int Count()
        {
            return _cards.Count;
        }

        public void AddCards(Deck otherDeck, bool remove = false)
        {
            _cards.AddRange(otherDeck._cards);
            if (remove)
            {
                otherDeck._cards.Clear();
            }
        }
    }
}