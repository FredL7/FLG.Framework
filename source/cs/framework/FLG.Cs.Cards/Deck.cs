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
    public class Deck {
        private List<Card> _cards;

        private List<Card> _library;
        private List<List<Card>> _hand;
        private List<Card> _graveyard;

        public List<Card> GetHand(int playerIndex = 0) => _hand[playerIndex];

        public Deck(int nbPlayers = 1)
        {
            _cards = [];
            _library = [];
            _graveyard = [];

            _hand = new(nbPlayers);
            for (int i = 0; i < nbPlayers; ++i)
                _hand.Add([]);
        }

        internal void SetCards(List<Card> cards)
        {
            _cards = cards;
            Reset();
        }

        public Card DrawTop(int playerIndex = 0)
        {
            Card card = _library.Last();
            _library.RemoveAt(_library.Count - 1);
            _hand[playerIndex].Add(card);
            return card;
        }

        public List<Card> DrawMultiple(uint n, int playerIndex = 0)
        {
            List<Card> cards = new((Int32)n);

            for (int i = 0; i < n; ++i)
            {
                var card = DrawTop(playerIndex);
                cards.Add(card);
            }
            return cards;
        }

        public void Shuffle() => CollectionUtils.Shuffle(_library);

        public void Discard(Card card, int playerIndex = 0)
        {
            _hand[playerIndex].Remove(card);
            _graveyard.Add(card);
        }

        public Card Search(string name, int playerIndex = 0, bool searchEverywhere = false)
        {
            var card = _library.Find(x => x.Name == name);
            if (card != null)
            {
                _library.Remove(card);
                _hand[playerIndex].Add(card);
                return card;
            }

            if (searchEverywhere)
            {
                card = _graveyard.Find(x => x.Name == name);
                if (card != null)
                {
                    _graveyard.Remove(card);
                    _hand[playerIndex].Add(card);
                    return card;
                }
            }

            throw new KeyNotFoundException($"Could not find card named {name}");
        }

        public void Reset()
        {
            _library.Clear();
            _graveyard.Clear();
            _library = new(_cards);

            foreach (var hand in _hand)
                hand.Clear();
        }

        public int CountCards() => _cards.Count;
        public int CountLibrary() => _library.Count;
        public int CountHand(int playerIndex = 0) => _hand[playerIndex].Count;
        public int CountGraveyard() => _graveyard.Count;
    }
}
