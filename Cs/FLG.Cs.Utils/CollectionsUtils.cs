namespace FLG.Cs.Utils {
    public static class CollectionUtils {
        private static readonly System.Random rng = new();

        public static void Shuffle<T>(T[] array)
        {
            int n = array.Length;
            while (n > 1)
            {
                int k = rng.Next(n--);
                (array[k], array[n]) = (array[n], array[k]);
            }
        }

        public static void Shuffle<T>(List<T> collection)
        {
            int n = collection.Count;
            while (n > 1)
            {
                int k = rng.Next(n--);
                (collection[k], collection[n]) = (collection[n], collection[k]);
            }
        }
    }
}
