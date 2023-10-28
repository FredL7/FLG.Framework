namespace FLG.Cs.Serialization {
    public interface ISaveFile {
        public string GetName();
        public string GetPath();
        public DateTime GetDateCreated();
        public DateTime GetDateLastModified();

        public void UpdateDateLastModified();
    }
}
