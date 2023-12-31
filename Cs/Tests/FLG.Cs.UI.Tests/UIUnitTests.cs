using Microsoft.VisualStudio.TestTools.UnitTesting;

using FLG.Cs.ServiceLocator;
using FLG.Cs.Framework;
using FLG.Cs.Logger;


namespace FLG.Cs.UI.Tests {
    [TestClass]
    public class UIUnitTests {
        private const string LOGS_DIR = "../../../../../../_logs";
        private const string LAYOUTS_DIR = "../../../Layouts";

        [ClassInitialize]
        public static void Init(TestContext _)
        {
            Preferences prefs = new();
            FrameworkManager.Instance.Initialize(prefs);

            PreferencesLogs prefsLogs = new()
            {
                logsDir = LOGS_DIR
            };
            FrameworkManager.Instance.InitializeLogs(prefsLogs);

            // TODO: Register as UI observer
            // TODO: Register additional pages and layouts (for Widgets / Controllers)

            PreferencesUI prefsUI = new()
            {
                layoutsDir = LAYOUTS_DIR
            };
            FrameworkManager.Instance.InitializeUI(prefsUI);
        }

        [TestMethod]
        public void TestLayout()
        {
            IUIManager uiManager = Locator.Instance.Get<IUIManager>();

            var layouts = uiManager.GetLayouts();
            Assert.IsTrue(layouts.Count() > 0);
            foreach (var layout in layouts)
            {
                if (layout.GetName() != "sample")
                    Assert.Fail();

                var root = layout.GetRoot();
                var rootDimensions = root.GetDimensions();
                var rootPosition = root.GetPosition();
                Assert.IsTrue(rootDimensions.Width == 1920 && rootDimensions.Height == 1080);
                Assert.IsTrue(rootPosition.X == 0 && rootPosition.Y == 0);

                Assert.IsTrue(root.HasChildren());
                Assert.IsTrue(root.GetName() == "sample");

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
                        var contentDimensions = child.GetDimensions();
                        var contentPosition = child.GetPosition();
                        Assert.IsTrue(contentDimensions.Width == 1920 && contentDimensions.Height == 1040);
                        Assert.IsTrue(contentPosition.X == 0 && contentPosition.Y == 40);

                        Assert.IsFalse(child.HasChildren());
                        Assert.IsTrue(child.HasChildren("content"));
                        foreach (var child2 in child.GetChildrens("content"))
                        {
                            var name = child2.GetName();
                            Assert.IsTrue(name == "page-test-1" || name == "page-test-2" || name == "page-test-3");
                            var child2Dimensions = child2.GetDimensions();
                            var child2Position = child2.GetPosition();
                            Assert.IsTrue(child2Dimensions.Width == 640 && child2Dimensions.Height == 1040);
                            float expectedX = 0;
                            if (name == "page-test-1")
                            {
                                expectedX = 0;
                            }
                            else if (name == "page-test-2")
                            {
                                expectedX = 640;
                            }
                            else if (name == "page-test-3")
                            {
                                expectedX = 1280;
                            }
                            Assert.IsTrue(child2Position.X == expectedX && child2Position.Y == 0);
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
