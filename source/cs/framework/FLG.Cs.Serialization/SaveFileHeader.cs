namespace FLG.Cs.Serialization {
    public struct SaveFileHeader {
        public uint version;
        public string name;
        public DateTime dateCreated;
        public DateTime dateLastModified;
    }
}
