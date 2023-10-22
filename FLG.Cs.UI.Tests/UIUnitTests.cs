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
                if (layout.GetName() != "sample")
                    Assert.Fail();

                var root = layout.GetRoot();
                var rootDimensions = root.GetDimensions();
                var rootPosition = root.GetPosition();
                Assert.IsTrue(rootDimensions.Width == 1920 && rootDimensions.Height == 1080);
                Assert.IsTrue(rootPosition.X == 0 && rootPosition.Y == 0);

                Assert.IsTrue(root.HasChildren());
                Assert.IsTrue(root.GetName() == "main");

                foreach(var child in root.GetChildrens())
                {
                    if (child.GetName() == "header")
                    {
                        var position = child.GetPosition();
                        var dimensions = child.GetDimensions();
                        Assert.IsTrue(position.X == 10 && position.Y == 10);
                        Assert.IsTrue(dimensions.Width == 1900 && dimensions.Height == 20);
                        // TODO: Other childrens
                    }
                    else if (child.GetName() == "content")
                    {
                        foreach (var child2 in child.GetChildrens())
                        {
                            if (child2.GetName() == "first")
                            {
                                var left1Dimensions = child2.GetDimensions();
                                var left1Position = child2.GetPosition();
                                Assert.IsTrue(left1Dimensions.Width == 140 && left1Dimensions.Height == 940);
                                Assert.IsTrue(left1Position.X == 50 && left1Position.Y == 40);
                            }
                            else if (child2.GetName() == "second")
                            {
                                var left2Dimensions = child2.GetDimensions();
                                var left2Position = child2.GetPosition();
                                Assert.IsTrue(left2Dimensions.Width == 1585 && left2Dimensions.Height == 900);
                                Assert.IsTrue(left2Position.X == 265 && left2Position.Y == 80);
                            }
                            else { Assert.Fail(); }
                        }
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
