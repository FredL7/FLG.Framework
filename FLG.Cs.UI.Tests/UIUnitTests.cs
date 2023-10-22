using Microsoft.VisualStudio.TestTools.UnitTesting;

using FLG.Cs.ServiceLocator;
using FLG.Cs.Logger;

namespace FLG.Cs.UI.Tests {
    [TestClass]
    public class UIUnitTests {

        [ClassInitialize]
        public static void Init(TestContext _)
        {
            LogManager.Instance.SetLogLocation("../../../../_logs");

            IUIManager uiManager = new UIManager();
            uiManager.RegisterLayouts("../../../Layouts");
            uiManager.RegisterPages("../../../Pages");
            SingletonManager.Instance.Register(uiManager);
        }

        [TestMethod]
        public void TestLayoutHStack()
        {
            IUIManager uiManager = SingletonManager.Instance.Get<IUIManager>();

            foreach(var layout in uiManager.GetLayouts())
            {
                if (layout.GetName() != "Sample")
                    Assert.Fail();

                var root = layout.GetRoot();
                var rootDimensions = root.GetDimensions();
                var rootPosition = root.GetPosition();
                Assert.IsTrue(rootDimensions.Width == 1920 && rootDimensions.Height == 1080);
                Assert.IsTrue(rootPosition.X == 0 && rootPosition.Y == 0);

                Assert.IsTrue(root.HasChildren());

                foreach(var child in root.GetChildrens())
                {
                    if (child.GetName() == "First")
                    {
                        var left1Dimensions = child.GetDimensions();
                        var left1Position = child.GetPosition();
                        Assert.IsTrue(left1Dimensions.Width == 140 && left1Dimensions.Height == 980);
                        Assert.IsTrue(left1Position.X == 50 && left1Position.Y == 40);
                    }
                    else if (child.GetName() == "Second")
                    {
                        var left2Dimensions = child.GetDimensions();
                        var left2Position = child.GetPosition();
                        Assert.IsTrue(left2Dimensions.Width == 1585 && left2Dimensions.Height == 940);
                        Assert.IsTrue(left2Position.X == 265 && left2Position.Y == 80);
                    }
                    else { Assert.Fail(); }
                }
            }
        }

        [TestMethod]
        public void TestPages()
        {
            Assert.IsTrue(true);
        }
    }
}
