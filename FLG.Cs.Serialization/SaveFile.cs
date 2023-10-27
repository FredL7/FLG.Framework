﻿namespace FLG.Cs.Serialization {
    public class SaveFile : ISaveFile { 
        private readonly string _name;
        private readonly string _filepath;
        private readonly DateTime _dateCreated;
        private DateTime _dateLastModified;

        public SaveFile(string name, string filepath, DateTime dateCreated, DateTime dateLastModified)
        {
            _name = name;
            _filepath = filepath;
            _dateCreated = dateCreated;
            _dateLastModified = dateLastModified;
        }

        public string GetName() => _name;

        public string GetPath() => _filepath;

        public DateTime GetDateCreated() => _dateCreated;
        public DateTime GetDateLastModified() => _dateLastModified;

        public void UpdateDateLastModified()
        {
            // TODO: UTC vs non-UTC
            _dateLastModified = DateTime.UtcNow;
        }
    }
}