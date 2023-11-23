global using Microsoft.VisualStudio.TestTools.UnitTesting;

using FLG.Cs.Factory;
using FLG.Cs.Logger;
using FLG.Cs.ServiceLocator;

namespace FLG.Cs.Serialization.Tests {
    [TestClass]
    public class SerializationUnitTests {
        private const string savedir = "../../../../../../_saves";

        private const bool _boolvalue = true;
        private const uint _uintvalue = uint.MaxValue;
        private const int _intvalue = int.MinValue;
        private const float _floatvalue = float.Epsilon;
        private const string _stringvalue = "Hello World!";
        private static DateTime _datevalue;

        private static SerializableItem? _item;

        [ClassInitialize]
        public static void Init(TestContext _)
        {
            LogManager.Instance.Initialize("../../../../../../_logs");
            ManagerFactory.CreateBinarySerializer(savedir);

            ISerializerManager serializer = Locator.Instance.Get<ISerializerManager>();
            _datevalue = DateTime.Now;
            _item = new(_boolvalue, _uintvalue, _intvalue, _floatvalue, _stringvalue, _datevalue);
            serializer.AddSerializable(_item);

        }

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
                if (saveFile.GetName() == filename)
                    serializer.Deserialize(saveFile);
        }
        #endregion Utils

        [TestMethod]
        public void TestBinarySerialization()
        {
            ISerializerManager serializer = Locator.Instance.Get<ISerializerManager>();
            serializer.SetSerializerBinary();

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

        [TestMethod]
        public void TestJsonSerialization()
        {
            ISerializerManager serializer = Locator.Instance.Get<ISerializerManager>();
            serializer.SetSerializerJson();

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

        [TestMethod]
        public void TestXmlSerialization()
        {
            ISerializerManager serializer = Locator.Instance.Get<ISerializerManager>();
            serializer.SetSerializerXml();

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
    }
}
