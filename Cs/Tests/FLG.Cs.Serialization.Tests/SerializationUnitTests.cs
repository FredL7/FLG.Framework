global using Microsoft.VisualStudio.TestTools.UnitTesting;

using FLG.Cs.IDatamodel;
using FLG.Cs.Framework;
using FLG.Cs.ServiceLocator;


namespace FLG.Cs.Serialization.Tests {
    [TestClass]
    public class SerializationUnitTests {
        private const string SAVES_DIR = "../../../../../../_saves";
        private const string LOGS_DIR = "../../../../../../_logs";

        private const bool _boolvalue = true;
        private const uint _uintvalue = uint.MaxValue;
        private const int _intvalue = int.MinValue;
        private const float _floatvalue = float.Epsilon;
        private const string _stringvalue = "Hello World!";
        private static DateTime _datevalue;

        private static SerializableItem? _item;

        #region Test
        private static void Initialize(ESerializerType t)
        {
            Preferences pref = new();
            PreferencesSerialization prefSerialization = new()
            {
                savesDir = SAVES_DIR,
                serializerType = t
            };
            FrameworkManager.Instance.Initialize(pref);
            FrameworkManager.Instance.InitializeSerializer(prefSerialization);

            ISerializerManager serializer = Locator.Instance.Get<ISerializerManager>();
            _datevalue = DateTime.Now;
            _item = new(_boolvalue, _uintvalue, _intvalue, _floatvalue, _stringvalue, _datevalue);
            serializer.AddSerializable(_item);
        }

        private static void Test()
        {
            var filename = MakeFilename();
            Serialize(filename);
            ChangeItemValues();
            Deserialize(filename);

            Assert.IsTrue(_item.GetBoolValue() == _boolvalue);
            Assert.IsTrue(_item.GetUintValue() == _uintvalue);
            Assert.IsTrue(_item.GetIntValue() == _intvalue);
            Assert.IsTrue(_item.GetFloatValue() == _floatvalue);
            Assert.IsTrue(_item.GetStringValue() == _stringvalue);
            Assert.IsTrue(_item.GetDateValue() == _datevalue);
        }
        #endregion Test

        #region Utils
        private static string MakeFilename()
        {
            var now = DateTime.Now.ToString("yyyyddM_HH-mm");
            return $"test-{now}";
        }

        private static void Serialize(string filename)
        {
            ISerializerManager serializer = Locator.Instance.Get<ISerializerManager>();
            serializer.Serialize(filename);
        }

        private static void ChangeItemValues()
        {
            _item.Set(false, uint.MinValue, int.MaxValue, float.MaxValue, "Goodbye World!", DateTime.Now);
        }

        private static void Deserialize(string filename)
        {
            ISerializerManager serializer = Locator.Instance.Get<ISerializerManager>();
            foreach (var saveFile in serializer.GetSaveFiles())
                if (saveFile.Name == filename)
                    serializer.Deserialize(saveFile);
        }
        #endregion Utils

        [TestMethod]
        public void TestBinarySerialization()
        {
            Initialize(ESerializerType.BIN);
            Test();
        }

        [TestMethod]
        public void TestJsonSerialization()
        {
            Initialize(ESerializerType.JSON);
            Test();
        }

        [TestMethod]
        public void TestXmlSerialization()
        {
            Initialize(ESerializerType.XML);
            Test();
        }
    }
}
