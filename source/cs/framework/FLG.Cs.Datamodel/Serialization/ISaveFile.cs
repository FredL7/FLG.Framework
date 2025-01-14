namespace FLG.Cs.Datamodel.Serialization {
    public interface ISaveFile {
        public string Name { get; }
        public string Path { get; }
        public ESerializerType Type { get; }
        public DateTime DateCreated { get; }
        public DateTime DateLastModified { get; }

        public void UpdateDateLastModified();
    }
}
