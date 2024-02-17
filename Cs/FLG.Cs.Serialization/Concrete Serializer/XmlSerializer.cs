using System.Globalization;
using System.Xml;

using FLG.Cs.IDatamodel;


namespace FLG.Cs.Serialization {
    internal class XmlSerializer : Serializer {
        internal const string SAVE_EXTENSION = ".xsave";
        protected override string GetSaveExtension() => SAVE_EXTENSION;
        protected override ESerializerType GetSerializerType() => ESerializerType.XML;

        private const string ROOT_NAME = "Root";
        private XmlDocument _document;
        private XmlElement _root;

        internal XmlSerializer(SerializerManager manager) : base(manager) {
            _document = new();
            _root = _document.CreateElement(ROOT_NAME);
        }

        public sealed override void Serialize(ISaveFile saveFile)
        {
            _document = new();
            _root = _document.CreateElement(ROOT_NAME);
            _document.AppendChild(_root);

            SaveHeader(saveFile);
            SerializeSerializables();

            var filepath = saveFile.GetPath();
            using TextWriter writer = new StreamWriter(filepath);
            System.Xml.Serialization.XmlSerializer ser = new(typeof(XmlElement));
            ser.Serialize(writer, _document);
        }

        public sealed override void Deserialize(ISaveFile saveFile)
        {
            var filepath = saveFile.GetPath();
            _document = new();
            _document.Load(filepath);
            _root = _document[ROOT_NAME];
            LoadHeader();
            DeserializeSerializables();
        }

        internal sealed override SaveFileHeader DeserializeHeaderOnly(string filepath)
        {
            _document = new();
            _document.Load(filepath);
            _root = _document[ROOT_NAME];
            return LoadHeader();
        }

        #region Primitive Types
        public override void SaveBool(bool value, string id)
        {
            XmlElement myElement = _document.CreateElement(id);
            myElement.InnerText = value.ToString();
            _root.AppendChild(myElement);
        }
        public override bool LoadBool(string id)
        {
            var value = _root[id].InnerText;
            return bool.Parse(value);
        }

        public override void SaveUint(uint value, string id)
        {
            XmlElement myElement = _document.CreateElement(id);
            myElement.InnerText = value.ToString();
            _root.AppendChild(myElement);
        }
        public override uint LoadUint(string id)
        {
            var value = _root[id].InnerText;
            return uint.Parse(value);
        }

        public override void SaveInt(int value, string id)
        {
            XmlElement myElement = _document.CreateElement(id);
            myElement.InnerText = value.ToString();
            _root.AppendChild(myElement);
        }
        public override int LoadInt(string id)
        {
            var value = _root[id].InnerText;
            return int.Parse(value);
        }

        public override void SaveLong(long value, string id)
        {
            XmlElement myElement = _document.CreateElement(id);
            myElement.InnerText = value.ToString();
            _root.AppendChild(myElement);
        }
        public override long LoadLong(string id)
        {
            var value = _root[id].InnerText;
            return long.Parse(value);
        }

        public override void SaveFloat(float value, string id)
        {
            XmlElement myElement = _document.CreateElement(id);
            myElement.InnerText = value.ToString();
            _root.AppendChild(myElement);
        }
        public override float LoadFloat(string id)
        {
            var value = _root[id].InnerText;
            return float.Parse(value);
        }

        public override void SaveDouble(double value, string id)
        {
            XmlElement myElement = _document.CreateElement(id);
            myElement.InnerText = value.ToString();
            _root.AppendChild(myElement);
        }
        public override double LoadDouble(string id)
        {
            var value = _root[id].InnerText;
            return double.Parse(value);
        }

        public override void SaveString(string value, string id)
        {
            XmlElement myElement = _document.CreateElement(id);
            myElement.InnerText = value;
            _root.AppendChild(myElement);
        }
        public override string LoadString(string id)
        {
            var value = _root[id].InnerText;
            return value;
        }
        #endregion Primitive Types

        #region Complex Types
        public override void SaveDateTime(DateTime value, string id)
        {
            XmlElement myElement = _document.CreateElement(id);
            myElement.InnerText = value.ToString("O", CultureInfo.InvariantCulture);
            _root.AppendChild(myElement);
        }
        public override DateTime LoadDateTime(string id)
        {
            var value = _root[id].InnerText;
            return DateTime.Parse(value);
        }
        #endregion Complex Types
    }
}
