namespace FLG.Cs.Serialization {
    public interface ISaveFile {
        public string GetName();
        public string GetPath();
        public ESerializerType GetSerializerType();
        public DateTime GetDateCreated();
        public DateTime GetDateLastModified();

        public void UpdateDateLastModified();
    }
}
