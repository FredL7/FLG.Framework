namespace FLG.Cs.Cards {
    public class Card(string name, int number, string color) {
        public string Name { get; private set; } = name;
        public int Number { get; private set; } = number;
        public string Color { get; private set; } = color;
    }
}
