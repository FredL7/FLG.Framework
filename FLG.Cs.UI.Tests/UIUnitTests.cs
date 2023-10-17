using FLG.Cs.UI.Layouts;
using FLG.Cs.Math;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace FLG.Cs.UI.Tests {
    [TestClass]
    public class UIUnitTests {
        [TestMethod]
        public void TestLayoutHStack()
        {
            IUIManager manager = new UIManager();

            ProxyHStack root = new(new RectXform(), Size.Zero);
            ProxyLayoutElementLeaf leaf1 = new(new RectXform(new Spacing(5f, 10f, 15f, 20f), new Spacing(25f, 30f, 35f, 40f)), new Size(200f, 0));
            ProxyLayoutElementLeaf leaf2 = new(new RectXform(new Spacing(45f, 50f, 15f, 20f), new Spacing(25f, 30f, 35f, 40f)), new Size(0, 0));
            root.AddChild(leaf1);
            root.AddChild(leaf2);
            ProxyLayout layout = new(root);

            manager.RegisterLayout(0, layout);
            manager.ComputeLayoutsRectXforms(1920, 1080);

            var rootDimensions = root.RectXform.GetDimensions();
            var rootPosition = root.RectXform.GetContainerPosition();
            Assert.IsTrue(rootDimensions.Width == 1920 && rootDimensions.Height == 1080);
            Assert.IsTrue(rootPosition.X == 0 && rootPosition.Y == 0);

            var left1Dimensions = leaf1.RectXform.GetDimensions();
            var left1Position = leaf1.RectXform.GetContainerPosition();
            Assert.IsTrue(left1Dimensions.Width == 140 && left1Dimensions.Height == 980);
            Assert.IsTrue(left1Position.X == 50 && left1Position.Y == 40);

            var left2Dimensions = leaf2.RectXform.GetDimensions();
            var left2Position = leaf2.RectXform.GetContainerPosition();
            Assert.IsTrue(left2Dimensions.Width == 1585 && left2Dimensions.Height == 940);
            Assert.IsTrue(left2Position.X == 265 && left2Position.Y == 80);
        }

        [TestMethod]
        public void TestPages()
        {
            Assert.IsTrue(true);
        }
    }
}
