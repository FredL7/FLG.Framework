namespace FLG.Cs.Datamodel {
    public interface IDeck {
        public List<ICard> GetHand(int playerIndex = 0);

        public ICard DrawTop(int playerIndex = 0);
        public List<ICard> DrawMultiple(uint n, int playerIndex = 0);
        public ICard Search(string name, int playerIndex = 0, bool searchEverywhere = false);
        public void Shuffle();
        public void Discard(ICard card, int playerIndex = 0);
        public void Reset();

        public int CountCards();
        public int CountLibrary();
        public int CountHand(int playerIndex = 0);
        public int CountGraveyard();
    }
}
