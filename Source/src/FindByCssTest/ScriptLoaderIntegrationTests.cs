using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using NUnit.Framework;
using WatiN.Core;
using FindByCss;
using WatiN.Core.Exceptions;
using WatiN.Core.UnitTests.TestUtils;

namespace FindByCssTest
{
    [TestFixture]
    public class ScriptLoaderIntegrationTests : BrowserTestFixture
    {
        [Test]
        [STAThread]
        [TestCase("IE")]
        [TestCase("FireFox")]
        public void TestJQueryInstallScript_loadsJQueryIfNotLoaded(string browserType)
        {
            UseBrowser(browserType);

            ScriptLoader loader = new ScriptLoader();

            GoToResource("burger.htm");

            Assert.AreEqual("undefined", Browser.Eval("typeof window.jQuery"));

            string js = loader.GetJQueryInstallScript();
            Browser.RunScript(js);

            Browser.Element(e => e.TagName.ToLower() == "script" && e.GetAttributeValue("src") == "http://ajax.googleapis.com/ajax/libs/jquery/1.2.6/jquery.min.js").WaitUntilExists();

            var content = Browser.Eval("window.jQuery('a.plainburger').length");

            Assert.AreEqual("1", content);
            Assert.IsTrue(Browser.Eval("window.jQuery").StartsWith("function"));
        }

        [Test]
        [STAThread]
        [TestCase("IE")]
        [TestCase("FireFox")]
        public void TestJQueryInstallScript_doesNotReloadJQuery(string browserType)
        {
            UseBrowser(browserType);

            ScriptLoader loader = new ScriptLoader();

            GoToResource("cheeseburger.htm");

            Browser.Link(Find.ByClass("cheeseburger")).WaitUntilExists();
            Browser.RunScript("window.$.isThisTheOriginal$=true;");

            var marker2 = Browser.Eval("window.$.isThisTheOriginal$");
            Assert.AreEqual("true", marker2);
            
            //  The value $.isThisTheOriginal$ is set to true, so we can detect if $ is overwritten

            string js = loader.GetJQueryInstallScript();
            Browser.RunScript(js);
            try
            {
                Browser.WaitUntilContainsText("textnotfound", 5);
            }
            catch (Exception)
            {
            }

            Assert.AreEqual("true", Browser.Eval("window.$.isThisTheOriginal$"));
        }


        [Test]
        [STAThread]
        [TestCase("IE")]
        [TestCase("FireFox")]
        [Explicit]
        public void TestJQueryInstallScript__loadsJQueryInCompatabilityMode(string browser)
        {
            Assert.Fail("not implemented");
        }

        [Test]
        [Explicit]
        public void Test_doesNotPutExistingjQueryIntoCompatibilityMode()
        {
            Assert.Fail("not implemented");
        }


        [Test]
        [STAThread]
        [TestCase("IE")]
        [TestCase("FireFox")]
        public void TestGetCssMarkingScript_marksElements(string browserType)
        {
            UseBrowser(browserType);

            ScriptLoader loader = new ScriptLoader();

            GoToResource("veggieburger.htm");

            Browser.Link(Find.ByClass("veggieburger")).WaitUntilExists();

            Assert.AreEqual("0", Browser.Eval("window.jQuery('.marker').length"));

            var script = loader.GetCssMarkingScript("a.veggieburger", "marker");
            Browser.RunScript(script);

            Assert.AreEqual("1", Browser.Eval("window.jQuery('.marker').length"));
        }

        [Test]
        [STAThread]
        [TestCase("IE")]
        [TestCase("FireFox")]
        public void TestGetCssMarkRemovalScript_unmarksElements(string browserType)
        {
            UseBrowser(browserType);

            ScriptLoader loader = new ScriptLoader();

            GoToResource("veggieburger.htm");

            Browser.Link(Find.ByClass("veggieburger")).WaitUntilExists();

            Assert.AreEqual("1", Browser.Eval("window.jQuery('.veggieburger').length"));

            var script = loader.GetCssMarkRemovalScript("a.veggieburger", "veggieburger");
            Browser.RunScript(script);

            Assert.AreEqual("0", Browser.Eval("window.jQuery('.veggieburger').length"));
        }
    }
}
