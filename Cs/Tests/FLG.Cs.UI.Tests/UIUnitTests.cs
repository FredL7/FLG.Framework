using Microsoft.VisualStudio.TestTools.UnitTesting;

using FLG.Cs.ServiceLocator;
using FLG.Cs.Logger;
using FLG.Cs.Factory;

namespace FLG.Cs.UI.Tests {
    [TestClass]
    public class UIUnitTests {

        [ClassInitialize]
        public static void Init(TestContext _)
        {
            LogManager.Instance.SetLogLocation("../../../../_logs");
            ManagerFactory.CreateUIManager();

            SetupUIManager();
        }

        private static void SetupUIManager()
        {
            IUIManager uiManager = SingletonManager.Instance.Get<IUIManager>();
            uiManager.RegisterLayouts("../../../Layouts");
            uiManager.RegisterPages("../../../Pages");
        }

        [TestMethod]
        public void TestLayout()
        {
            IUIManager uiManager = SingletonManager.Instance.Get<IUIManager>();

            foreach (var layout in uiManager.GetLayouts())
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

                foreach (var child in root.GetChildrens())
                {
                    if (child.GetName() == "header")
                    {
                        var position = child.GetPosition();
                        var dimensions = child.GetDimensions();
                        Assert.IsTrue(position.X == 10 && position.Y == 10);
                        Assert.IsTrue(dimensions.Width == 1900 && dimensions.Height == 20);
                        foreach (var child2 in child.GetChildrens())
                        {
                            if (child2.GetName() == "header-title")
                            {
                                var titlePosition = child2.GetPosition();
                                var titleDimensions = child2.GetDimensions();
                                Assert.IsTrue(titlePosition.X == 0 && titlePosition.Y == 0);
                                Assert.IsTrue(titleDimensions.Width == 950 && titleDimensions.Height == 20);
                                foreach (var child3 in child2.GetChildrens())
                                {
                                    if (child3.GetName() == "header-title-icon")
                                    {
                                        var iconPosition = child3.GetPosition();
                                        var iconDimensions = child3.GetDimensions();
                                        Assert.IsTrue(iconPosition.X == 0 && iconPosition.Y == 0);
                                        Assert.IsTrue(iconDimensions.Width == 20 && iconDimensions.Height == 20);
                                    }
                                    else if (child3.GetName() == "header-title-title")
                                    {
                                        var titletitlePosition = child3.GetPosition();
                                        var titletitleDimensions = child3.GetDimensions();
                                        Assert.IsTrue(titletitlePosition.X == 30 && titletitlePosition.Y == 0);
                                        Assert.IsTrue(titletitleDimensions.Width == 920 && titletitleDimensions.Height == 20);
                                    }
                                    else { Assert.Fail(); }
                                }
                            }
                            else if (child2.GetName() == "header-controls")
                            {
                                var controlsPosition = child2.GetPosition();
                                var controlsDimensions = child2.GetDimensions();
                                Assert.IsTrue(controlsPosition.X == 950 && controlsPosition.Y == 0);
                                Assert.IsTrue(controlsDimensions.Width == 950 && controlsDimensions.Height == 20);
                                foreach (var child3 in child2.GetChildrens())
                                {
                                    if (child3.GetName() == "header-controls-minimize")
                                    {
                                        var minimizePosition = child3.GetPosition();
                                        var minimizeDimensions = child3.GetDimensions();
                                        Assert.IsTrue(minimizePosition.X == 890 && minimizePosition.Y == 0);
                                        Assert.IsTrue(minimizeDimensions.Width == 20 && minimizeDimensions.Height == 20);
                                    }
                                    else if (child3.GetName() == "header-controls-maximize")
                                    {
                                        var maximizePosition = child3.GetPosition();
                                        var maximizeDimensions = child3.GetDimensions();
                                        Assert.IsTrue(maximizePosition.X == 910 && maximizePosition.Y == 0);
                                        Assert.IsTrue(maximizeDimensions.Width == 20 && maximizeDimensions.Height == 20);
                                    }
                                    else if (child3.GetName() == "header-controls-quit")
                                    {
                                        var quitPosition = child3.GetPosition();
                                        var quitDimensions = child3.GetDimensions();
                                        Assert.IsTrue(quitPosition.X == 930 && quitPosition.Y == 0);
                                        Assert.IsTrue(quitDimensions.Width == 20 && quitDimensions.Height == 20);
                                    }
                                    else { Assert.Fail(); }
                                }
                            }
                            else { Assert.Fail(); }
                        }
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
