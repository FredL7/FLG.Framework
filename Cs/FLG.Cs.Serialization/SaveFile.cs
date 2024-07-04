using FLG.Cs.Datamodel;

namespace FLG.Cs.Serialization {
    public class SaveFile : ISaveFile {
        public string Name { get; private set; }
        public string Path { get; private set; }
        public ESerializerType Type { get; private set; }
        public DateTime DateCreated { get; private set; }
        public DateTime DateLastModified { get; private set; }

        internal SaveFile(string name, string filepath, ESerializerType serializerType)
        {
            Name = name;
            Path = filepath;
            Type = serializerType;

            DateTime now = GetDateTimeNow();
            DateCreated = now;
            DateLastModified = now;
        }

        internal SaveFile(string name, string filepath, ESerializerType serializerType, DateTime dateCreated, DateTime dateLastModified)
        {
            Name = name;
            Path = filepath;
            Type = serializerType;
            DateCreated = dateCreated;
            DateLastModified = dateLastModified;
        }

        public void UpdateDateLastModified()
        {
            DateLastModified = GetDateTimeNow();
        }

        private static DateTime GetDateTimeNow() => DateTime.UtcNow;
    }
}
