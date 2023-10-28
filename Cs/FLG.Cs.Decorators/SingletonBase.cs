namespace FLG.Cs.Decorators {
    public abstract class SingletonBase<T> where T : SingletonBase<T> {
        private static readonly Lazy<T> _instance = new(() => (Activator.CreateInstance(typeof(T), true) as T)!);
        public static T Instance { get => _instance.Value; }
    }
}
