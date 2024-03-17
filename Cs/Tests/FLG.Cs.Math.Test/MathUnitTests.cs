using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Numerics;


namespace FLG.Cs.Math.Test {
    [TestClass]
    public class MathUnitTests {
        [TestMethod]
        public void TestSizeClass()
        {
            Size s0 = Size.Zero;
            Assert.IsTrue(s0.Width == 0f && s0.Height == 0f);

            Size s1 = new(10f);
            Assert.IsTrue(s1.Width == 10f && s1.Height == 10f);

            Size s2 = new(5f, 10f);
            Assert.IsTrue(s2.Width == 5f && s2.Height == 10f);
        }

        [TestMethod]
        public void TestSpacingClass()
        {
            Spacing s0 = Spacing.Zero;
            Assert.IsTrue(s0.Right == 0f && s0.Top == 0f && s0.Left == 0f && s0.Bottom == 0f);

            Spacing s1 = new(10f);
            Assert.IsTrue(s1.Right == 10f && s1.Top == 10f && s1.Left == 10f && s1.Bottom == 10f);

            Spacing s2 = new(5f, 10f);
            Assert.IsTrue(s2.Right == 5f && s2.Top == 10f && s2.Left == 5f && s2.Bottom == 10f);

            Spacing s3 = new(5f, 10f, 20f);
            Assert.IsTrue(s3.Right == 5f && s3.Top == 20f && s3.Left == 10f && s3.Bottom == 20f);

            Spacing s4 = new(5f, 10f, 20f, 25f);
            Assert.IsTrue(s4.Right == 5f && s4.Top == 10f && s4.Left == 20f && s4.Bottom == 25f);
        }

        [TestMethod]
        public void TestRectXformClass()
        {
            Spacing margins = new(5f, 10f, 15f, 20f);
            Spacing padding = new(25f, 30f, 35f, 40f);
            RectXform rectXform = new(margins, padding);

            Size bounds = new(250f, 400f);
            Vector2 position = new(20f, 760f);
            rectXform.SetSizesAndPosition(bounds, position);

            Vector2 expectedWrapprPosition = new(20f + 35f, 760f + 30f);
            Assert.IsTrue(rectXform.GetWrapperPosition().Equals(expectedWrapprPosition));

            Size expectedDimensions = new(250f - (25f + 35f), 400f - (30f + 40f));
            Assert.IsTrue(rectXform.GetDimensions().Equals(expectedDimensions));
        }
    }
}