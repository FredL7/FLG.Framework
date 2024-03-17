using Microsoft.VisualStudio.TestTools.UnitTesting;

using FLG.Cs.Framework;
using FLG.Cs.IDatamodel;
using FLG.Cs.ServiceLocator;
using FLG.Cs.Math;


namespace FLG.Cs.UI.Tests {
    [TestClass]
    public class UIUnitTests {
        private const string LOGS_DIR =     "../../../../../../_logs";
        private const string LAYOUTS_DIR =  "../../../../../ProjectDefs/ProjectDefs.UI/Layouts";
        private const string PAGES_DIR =    "../../../../../ProjectDefs/ProjectDefs.UI/Pages";

        [ClassInitialize]
        public static void Init(TestContext _)
        {
            Preferences prefs = new();
            FrameworkManager.Instance.InitializeFramework(prefs);

            PreferencesLogs prefsLogs = new()
            {
                logsDir = LOGS_DIR
            };
            FrameworkManager.Instance.InitializeLogs(prefsLogs);

            PreferencesUI prefsUI = new()
            {
                layoutsDir = LAYOUTS_DIR,
                pagesDir = PAGES_DIR,
                windowSize = new Size(1920, 1080)
            };
            FrameworkManager.Instance.InitializeUI(prefsUI);
        }

        [TestMethod]
        public void TestLayout()
        {
            IUIManager uiManager = Locator.Instance.Get<IUIManager>();

            var layouts = uiManager.GetLayouts();
            Assert.IsTrue(layouts.Any());
            foreach (var layout in layouts)
            {
                if (layout.Name != "sample")
                    Assert.Fail();

                var root = layout.Root;
                var rootDimensions = root.Dimensions;
                var rootPosition = root.Position;
                Assert.IsTrue(rootDimensions.Width == 1920 && rootDimensions.Height == 1080);
                Assert.IsTrue(rootPosition.X == 0 && rootPosition.Y == 0);

                Assert.IsTrue(root.HasChildren());
                Assert.IsTrue(root.Name == "sample");

                foreach (var child in root.GetChildrens())
                {
                    if (child.Name == "header")
                    {
                        var position = child.Position;
                        var dimensions = child.Dimensions;
                        Assert.IsTrue(position.X == 10 && position.Y == 10);
                        Assert.IsTrue(dimensions.Width == 1900 && dimensions.Height == 20);
                        foreach (var child2 in child.GetChildrens())
                        {
                            if (child2.Name == "header-title")
                            {
                                var titlePosition = child2.Position;
                                var titleDimensions = child2.Dimensions;
                                Assert.IsTrue(titlePosition.X == 0 && titlePosition.Y == 0);
                                Assert.IsTrue(titleDimensions.Width == 950 && titleDimensions.Height == 20);
                                foreach (var child3 in child2.GetChildrens())
                                {
                                    if (child3.Name == "header-title-icon")
                                    {
                                        var iconPosition = child3.Position;
                                        var iconDimensions = child3.Dimensions;
                                        Assert.IsTrue(iconPosition.X == 0 && iconPosition.Y == 0);
                                        Assert.IsTrue(iconDimensions.Width == 20 && iconDimensions.Height == 20);
                                    }
                                    else if (child3.Name == "header-title-title")
                                    {
                                        var titletitlePosition = child3.Position;
                                        var titletitleDimensions = child3.Dimensions;
                                        Assert.IsTrue(titletitlePosition.X == 30 && titletitlePosition.Y == 0);
                                        Assert.IsTrue(titletitleDimensions.Width == 920 && titletitleDimensions.Height == 20);
                                    }
                                    else { Assert.Fail(); }
                                }
                            }
                            else if (child2.Name == "header-controls")
                            {
                                var controlsPosition = child2.Position;
                                var controlsDimensions = child2.Dimensions;
                                Assert.IsTrue(controlsPosition.X == 950 && controlsPosition.Y == 0);
                                Assert.IsTrue(controlsDimensions.Width == 950 && controlsDimensions.Height == 20);
                                foreach (var child3 in child2.GetChildrens())
                                {
                                    if (child3.Name == "header-controls-minimize")
                                    {
                                        var minimizePosition = child3.Position;
                                        var minimizeDimensions = child3.Dimensions;
                                        Assert.IsTrue(minimizePosition.X == 890 && minimizePosition.Y == 0);
                                        Assert.IsTrue(minimizeDimensions.Width == 20 && minimizeDimensions.Height == 20);
                                    }
                                    else if (child3.Name == "header-controls-maximize")
                                    {
                                        var maximizePosition = child3.Position;
                                        var maximizeDimensions = child3.Dimensions;
                                        Assert.IsTrue(maximizePosition.X == 910 && maximizePosition.Y == 0);
                                        Assert.IsTrue(maximizeDimensions.Width == 20 && maximizeDimensions.Height == 20);
                                    }
                                    else if (child3.Name == "header-controls-quit")
                                    {
                                        var quitPosition = child3.Position;
                                        var quitDimensions = child3.Dimensions;
                                        Assert.IsTrue(quitPosition.X == 930 && quitPosition.Y == 0);
                                        Assert.IsTrue(quitDimensions.Width == 20 && quitDimensions.Height == 20);
                                    }
                                    else { Assert.Fail(); }
                                }
                            }
                            else { Assert.Fail(); }
                        }
                    }
                    else if (child.Name == "content")
                    {
                        var contentDimensions = child.Dimensions;
                        var contentPosition = child.Position;
                        Assert.IsTrue(contentDimensions.Width == 1920 && contentDimensions.Height == 1040);
                        Assert.IsTrue(contentPosition.X == 0 && contentPosition.Y == 40);
                        Assert.IsFalse(child.HasChildren()); // No children in default container

                        {
                            Assert.IsTrue(child.HasChildren("Sample1"));
                            foreach (var child2 in child.GetChildrens("Sample1"))
                            {
                                var name = child2.Name;
                                switch (name)
                                {
                                    case "page1-test-1":
                                        Assert.IsTrue(child2.Dimensions.Width == 128 && child2.Dimensions.Height == 40);
                                        Assert.IsTrue(child2.Position.X == 896 && child2.Position.Y == 336);
                                        break;
                                    case "page1-test-label":
                                        Assert.IsTrue(child2.Dimensions.Width == 128 && child2.Dimensions.Height == 40);
                                        Assert.IsTrue(child2.Position.X == 896 && child2.Position.Y == 396);
                                        break;
                                    case "page1-test-sprite":
                                        Assert.IsTrue(child2.Dimensions.Width == 128 && child2.Dimensions.Height == 128);
                                        Assert.IsTrue(child2.Position.X == 896 && child2.Position.Y == 456);
                                        break;
                                    case "page1-test-button":
                                        Assert.IsTrue(child2.Dimensions.Width == 128 && child2.Dimensions.Height == 40);
                                        Assert.IsTrue(child2.Position.X == 896 && child2.Position.Y == 604);
                                        break;
                                    case "page1-test-text":
                                        Assert.IsTrue(child2.Dimensions.Width == 128 && child2.Dimensions.Height == 40);
                                        Assert.IsTrue(child2.Position.X == 896 && child2.Position.Y == 664);
                                        break;
                                    default:
                                        Assert.Fail();
                                        break;
                                }
                            }
                        }

                        {
                            Assert.IsTrue(child.HasChildren("Sample2"));
                            foreach (var child2 in child.GetChildrens("Sample2"))
                            {
                                var name = child2.Name;
                                switch (name)
                                {
                                    case "page2-test-1":
                                        Assert.IsTrue(child2.Dimensions.Width == 128 && child2.Dimensions.Height == 40);
                                        Assert.IsTrue(child2.Position.X == 896 && child2.Position.Y == 336);
                                        break;
                                    case "page2-test-label":
                                        Assert.IsTrue(child2.Dimensions.Width == 128 && child2.Dimensions.Height == 40);
                                        Assert.IsTrue(child2.Position.X == 896 && child2.Position.Y == 396);
                                        break;
                                    case "page2-test-sprite":
                                        Assert.IsTrue(child2.Dimensions.Width == 128 && child2.Dimensions.Height == 128);
                                        Assert.IsTrue(child2.Position.X == 896 && child2.Position.Y == 456);
                                        break;
                                    case "page2-test-button":
                                        Assert.IsTrue(child2.Dimensions.Width == 128 && child2.Dimensions.Height == 40);
                                        Assert.IsTrue(child2.Position.X == 896 && child2.Position.Y == 604);
                                        break;
                                    case "page2-test-text":
                                        Assert.IsTrue(child2.Dimensions.Width == 128 && child2.Dimensions.Height == 40);
                                        Assert.IsTrue(child2.Position.X == 896 && child2.Position.Y == 664);
                                        break;
                                    default:
                                        Assert.Fail();
                                        break;
                                }
                            }
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
