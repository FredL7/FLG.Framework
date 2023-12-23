namespace FLG.Cs.Serialization {
    public class SaveFile : ISaveFile {
        private readonly string _name;
        private readonly string _filepath;
        private ESerializerType _serializerType;
        private readonly DateTime _dateCreated;
        private DateTime _dateLastModified;

        internal SaveFile(string name, string filepath, ESerializerType serializerType)
        {
            _name = name;
            _filepath = filepath;
            _serializerType = serializerType;

            DateTime now = GetDateTimeNow();
            _dateCreated = now;
            _dateLastModified = now;
        }

        internal SaveFile(string name, string filepath, ESerializerType serializerType, DateTime dateCreated, DateTime dateLastModified)
        {
            _name = name;
            _filepath = filepath;
            _serializerType = serializerType;
            _dateCreated = dateCreated;
            _dateLastModified = dateLastModified;
        }

        public string GetName() => _name;
        public string GetPath() => _filepath;
        public ESerializerType GetSerializerType() => _serializerType;
        public DateTime GetDateCreated() => _dateCreated;
        public DateTime GetDateLastModified() => _dateLastModified;

        public void UpdateDateLastModified()
        {
            _dateLastModified = GetDateTimeNow();
        }

        private static DateTime GetDateTimeNow() => DateTime.UtcNow;
    }
}
