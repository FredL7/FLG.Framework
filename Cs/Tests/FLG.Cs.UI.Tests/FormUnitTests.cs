using Microsoft.VisualStudio.TestTools.UnitTesting;

using FLG.Cs.Model;

namespace FLG.Cs.UI.Tests {
    [TestClass]
    public class FormUnitTests {
        [TestMethod]
        public void TestBoolModel()
        {
            SimpleBoolModel model = new(true);
            var value = model.GetValueAsBool();
            Assert.IsTrue(value == true);

            model.SetValue("false");
            value = model.GetValueAsBool();
            Assert.IsTrue(value == false);

            model.SetValue("true");
            value = model.GetValueAsBool();
            Assert.IsTrue(value == true);
        }

        [TestMethod]
        public void TestFloatModel()
        {
            SimpleFloatModel model = new(1.5f);
            var value = model.GetValueAsFloat();
            Assert.IsTrue(value == 1.5f);

            model.SetValue("14.33");
            value = model.GetValueAsFloat();
            Assert.IsTrue(value == 14.33f);
        }

        [TestMethod]
        public void TestIntegerModel()
        {
            SimpleIntegerModel model = new(14);
            var value = model.GetValueAsInt();
            Assert.IsTrue(value == 14);

            model.SetValue("-15");
            value = model.GetValueAsInt();
            Assert.IsTrue(value == -15);
        }

        [TestMethod]
        public void TestNetworkingModel()
        {
            NetworkingInputModel model = new();
            var value = model.GetValueAsString();
            Assert.IsTrue(value == "127.0.0.1");

            string newValue = "196.128.255.255";
            model.SetValue(newValue);
            value = model.GetValueAsString();
            Assert.IsTrue(value == newValue);
        }
    }
}
