using FLG.Cs.IDatamodel;

namespace FLG.Cs.Cards {
    public class StandardDeck52 : Deck {
        public StandardDeck52()
        {
            List<ICard> cards = new();
            string[] colors = { EBaseColors.CLUBS.ToString(), EBaseColors.DIAMONDS.ToString(), EBaseColors.HEARTS.ToString(), EBaseColors.SPADES.ToString() };

            foreach (string color in colors)
            {
                for (int i = 1; i <= 13; ++i)
                {
                    string name = i.ToString();
                    if (i == 1)
                    {
                        name = "Ace of";
                    }
                    else if (i == 11)
                    {
                        name = "Jack of";
                    }
                    else if (i == 12)
                    {
                        name = "Queen of";
                    }
                    else if (i == 13)
                    {
                        name = "King of";
                    }

                    Card c = new(name, i, color);
                    cards.Add(c);
                }
            }

            SetCards(cards);
        }
    }
}
