using System.Text;

using FLG.Cs.IDatamodel;
using FLG.Cs.ServiceLocator;
using FLG.Cs.Framework;


namespace FLG.Cs.Cards.Tests {
    [TestClass]
    public class CardsTests {
        private const string LOGS_DIR = "../../../../../../_logs";

        [TestMethod]
        public void CardsTest()
        {
            IDeck deck = new StandardDeck52();

            deck.Shuffle();
            Assert.IsTrue(deck.CountCards() == 52);
            Assert.IsTrue(deck.CountLibrary() == 52);
            Assert.IsTrue(deck.CountHand() == 0);
            Assert.IsTrue(deck.CountGraveyard() == 0);

            deck.Draw(7);
            var hand = deck.Hand;
            Assert.IsTrue(deck.CountCards() == 52 - 7);
            Assert.IsTrue(deck.CountLibrary() == 52 - 7);
            Assert.IsTrue(deck.CountHand() == 7);
            Assert.IsTrue(deck.CountGraveyard() == 0);

            for (int i = 0; i < 3; ++i)
                deck.Discard(hand[i]);
            Assert.IsTrue(deck.CountCards() == 52 - 7);
            Assert.IsTrue(deck.CountLibrary() == 52 - 7);
            Assert.IsTrue(deck.CountHand() == 7 - 3);
            Assert.IsTrue(deck.CountGraveyard() == 3);
        }

        [TestMethod]
        public void MultipleAces()
        {
            IDeck deck = new StandardDeck52();
            deck.Search("King of SPADES");
            deck.Search("Ace of SPADES");
            deck.Search("Ace of HEARTS");
            var hand = deck.Hand;
            var value = HandTotal(hand);

            Assert.IsTrue(value == 12);
        }

        [TestMethod]
        public void BlackJackMock()
        {
            int playerWins = 0;
            int dealerWins = 0;
            int round = 1;

            Preferences pref = new();
            PreferencesLogs prefsLogs = new()
            {
                logsDir = LOGS_DIR
            };
            FrameworkManager.Instance.InitializeFramework(pref);
            FrameworkManager.Instance.InitializeLogs(prefsLogs);
            var logger = Locator.Instance.Get<ILogManager>();

            while (playerWins < 3 && dealerWins < 3)
            {
                logger.Debug($"=== Round {round++} ===");
                IDeck deck = new StandardDeck52();
                deck.Shuffle();

                List<ICard> playerHand = deck.Draw(2);
                List<ICard> dealerHand = deck.Draw(2);

                int playerSum = HandTotal(playerHand);
                int dealerSum = HandTotal(dealerHand);

                while (playerSum < 17)
                {
                    var card = deck.Draw();
                    playerHand.Add(card);
                    playerSum = HandTotal(playerHand);
                }

                if (playerSum > 21)
                {
                    logger.Debug("Player bust");
                    ++dealerWins;
                }
                else
                {
                    if (playerSum != 21)
                    {
                        while (dealerSum < 17)
                        {
                            var card = deck.Draw();
                            dealerHand.Add(card);
                            dealerSum = HandTotal(dealerHand);
                        }
                    }

                    if (dealerSum > 21)
                    {
                        logger.Debug("Dealer bust");
                        ++playerWins;
                    }
                    else
                    {
                        if (playerSum > dealerSum)
                        {
                            logger.Debug("Player wins!");
                            ++playerWins;
                        }
                        else if (playerSum == dealerSum)
                        {
                            logger.Debug("Tie");
                        }
                        else
                        {
                            logger.Debug("Player loses");
                            ++dealerWins;
                        }
                    }
                }

                string playerHandPrettyString = HandPrettyString(playerHand, playerSum);
                string dealerHandPrettyString = HandPrettyString(dealerHand, dealerSum);
                logger.Debug($"Player: {playerWins} | dealer: {dealerWins}");
                logger.Debug($"Player hand: {playerHandPrettyString}");
                logger.Debug($"Dealer hand: {dealerHandPrettyString}");
            }

            logger.Debug("=== Game End ===");
            logger.Debug($"Player: {playerWins} | dealer: {dealerWins}");
        }

        private static string HandPrettyString(List<ICard> hand, int total)
        {
            StringBuilder sb = new();
            foreach (var card in hand)
            {
                sb.Append(card.Name);
                sb.Append(", ");
            }
            sb.Append("SUM: ");
            sb.Append(total);
            if (total == 21)
            {
                sb.Append(" !!!BLACK JACK!!!");
            }

            return sb.ToString();
        }

        private static int HandTotal(List<ICard> hand)
        {
            int total = 0;
            int aces = 0;
            foreach(var card in hand)
            {
                if (card.Number == 1)
                {
                    ++aces;
                }
                else if (card.Number > 10)
                {
                    total += 10;
                }
                else
                {
                    total += card.Number;
                }
            }

            for (int i = 0; i < aces; ++i)
            {
                if (total + 11 + (aces - 1) <= 21)
                {
                    total += 11;
                }
                else
                {
                    total += 1;
                }
            }

            return total;
        }
    }
}
