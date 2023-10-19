using FLG.Cs.UI.Layouts;
using FLG.Cs.Math;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace FLG.Cs.UI.Tests {
    [TestClass]
    public class UIUnitTests {
        [TestMethod]
        public void TestLayoutHStack()
        {
            IUIManager uiManager = new UIManager();
            uiManager.RegisterLayouts("../../../Layouts");
            uiManager.RegisterPages("../../../Pages");

            foreach(var layout in uiManager.GetLayouts())
            {
                var root = layout.Root;
                var rootDimensions = root.RectXform.GetDimensions();
                var rootPosition = root.RectXform.GetContainerPosition();
                Assert.IsTrue(rootDimensions.Width == 1920 && rootDimensions.Height == 1080);
                Assert.IsTrue(rootPosition.X == 0 && rootPosition.Y == 0);

                Assert.IsTrue(root.HasChildren());

                int count = 0;
                foreach(var child in root.GetChildrens())
                {
                    if (count == 0)
                    {
                        var left1Dimensions = child.RectXform.GetDimensions();
                        var left1Position = child.RectXform.GetContainerPosition();
                        Assert.IsTrue(left1Dimensions.Width == 140 && left1Dimensions.Height == 980);
                        Assert.IsTrue(left1Position.X == 50 && left1Position.Y == 40);
                    }
                    else if (count == 1)
                    {
                        var left2Dimensions = child.RectXform.GetDimensions();
                        var left2Position = child.RectXform.GetContainerPosition();
                        Assert.IsTrue(left2Dimensions.Width == 1585 && left2Dimensions.Height == 940);
                        Assert.IsTrue(left2Position.X == 265 && left2Position.Y == 80);
                    }
                    else { Assert.Fail(); }
                    ++count;
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
