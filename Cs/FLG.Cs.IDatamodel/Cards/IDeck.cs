namespace FLG.Cs.IDatamodel {
    public interface IDeck {
        public ICard Draw();
        public List<ICard> Draw(uint n);
        public void Shuffle();
        public void Discard(ICard card);
        public void Reset();

        public int CountCards();
        public int CountLibrary();
        public int CountHand();
        public int CountGraveyard();
    }
}
