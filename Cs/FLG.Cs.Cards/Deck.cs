using FLG.Cs.IDatamodel;
using FLG.Cs.Utils;

/* Consider:
 *   - Scry
 *   - Add to top or bottom of library
 *   - Singleplayer deck vs multiplayer deck (multiple hands, but that logic could be handled elsewhere)
 *   - Exile
 *   - Play a card = discard that card
 *   - Shuffle graveyard into library when library empty
 */
namespace FLG.Cs.Cards {
    public class Deck : IDeck {
        private List<ICard> _cards;

        private List<ICard> _library;
        private List<ICard> _hand;
        private List<ICard> _graveyard;

        public List<ICard> Hand { get => _hand; }

        public Deck()
        {
            _cards = new();
            _library = new();
            _hand = new();
            _graveyard = new();
        }

        internal void SetCards(List<ICard> cards)
        {
            _cards = cards;
            Reset();
        }

        public ICard Draw()
        {
            ICard card = _library.Last();
            _library.RemoveAt(_library.Count - 1);
            _hand.Add(card);
            return card;
        }

        public List<ICard> Draw(uint n)
        {
            List<ICard> cards = new((Int32)n);

            for (int i = 0; i < n; ++i)
            {
                var card = Draw();
                cards.Add(card);
            }

            return cards;
        }

        public void Shuffle()
        {
            CollectionUtils.Shuffle(_library);
        }

        public void Discard(ICard card)
        {
            _hand.Remove(card);
            _graveyard.Add(card);
        }

        public ICard Search(string name, bool searchEverywhere)
        {
            var card = _library.Find(x => x.Name == name);
            if (card != null)
            {
                _library.Remove(card);
                _hand.Add(card);
                return card;
            }

            if (searchEverywhere)
            {
                card = _graveyard.Find(x => x.Name == name);
                if (card != null)
                {
                    _graveyard.Remove(card);
                    _hand.Add(card);
                    return card;
                }
            }

            throw new KeyNotFoundException($"Could not find card named {name}");
        }

        public void Reset()
        {
            _library.Clear();
            _graveyard.Clear();
            _hand.Clear();
            _library = _cards;
        }

        public int CountCards() => _cards.Count;
        public int CountLibrary() => _library.Count;
        public int CountHand() => _hand.Count;
        public int CountGraveyard() => _graveyard.Count;
    }
}
