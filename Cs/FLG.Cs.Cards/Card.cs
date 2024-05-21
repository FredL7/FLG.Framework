using FLG.Cs.Datamodel;

namespace FLG.Cs.Cards {
    internal class Card: ICard {
        public string Name { get; private set; }
        public int Number { get; private set; }
        public string Color { get; private set; }

        public Card(string name, int number, string color)
        {
            Name = name;
            Number = number;
            Color = color;
        }
    }
}
