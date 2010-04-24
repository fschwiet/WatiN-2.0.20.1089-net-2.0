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
    public class ScriptLoaderIntegrationTests
    {
        static public Uri GetHtmlUrl(string htmlFilename)
        {
            return new Uri(BaseWatiNTest.GetHtmlTestFilesLocation("html") + htmlFilename);
        }
        
        static Browser GetBrowserWithPage(string browserType, string htmlFilename)
        {
            var browser = BrowserUtil.GetBrowser(browserType);
            browser.GoTo(GetHtmlUrl(htmlFilename));
            return browser;
        }

        Browser _browser = null;

        [TearDown]
        public void OnDone()
        {
            if (_browser != null)
                _browser.Close();

            _browser = null;
        }

        [Test]
        [STAThread]
        [TestCase("IE")]
        [TestCase("FireFox")]
        public void TestJQueryInstallScript_loadsJQueryIfNotLoaded(string browser)
        {
            ScriptLoader loader = new ScriptLoader();

            _browser = GetBrowserWithPage(browser, "burger.htm");

            Assert.AreEqual("undefined", _browser.Eval("typeof window.jQuery"));

            string js = loader.GetJQueryInstallScript();
            _browser.RunScript(js);

            _browser.Element(e => e.TagName.ToLower() == "script" && e.GetAttributeValue("src") == "http://ajax.googleapis.com/ajax/libs/jquery/1.2.6/jquery.min.js").WaitUntilExists();

            var content = _browser.Eval("window.jQuery('a.plainburger').length");

            Assert.AreEqual("1", content);
            Assert.IsTrue(_browser.Eval("window.jQuery").StartsWith("function"));
        }

        [Test]
        [STAThread]
        [TestCase("IE")]
        [TestCase("FireFox")]
        public void TestJQueryInstallScript_doesNotReloadJQuery(string browser)
        {
            ScriptLoader loader = new ScriptLoader();

            _browser = GetBrowserWithPage(browser, "cheeseburger.htm");

            _browser.Link(Find.ByClass("cheeseburger")).WaitUntilExists();
            _browser.RunScript("window.$.isThisTheOriginal$=true;");

            var marker2 = _browser.Eval("window.$.isThisTheOriginal$");
            Assert.AreEqual("true", marker2);
            
            //  The value $.isThisTheOriginal$ is set to true, so we can detect if $ is overwritten

            string js = loader.GetJQueryInstallScript();
            _browser.RunScript(js);
            try
            {
                _browser.WaitUntilContainsText("textnotfound", 5);
            }
            catch (Exception)
            {
            }

            Assert.AreEqual("true", _browser.Eval("window.$.isThisTheOriginal$"));
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
        public void TestGetCssMarkingScript_marksElements(string browser)
        {
            ScriptLoader loader = new ScriptLoader();

            _browser = GetBrowserWithPage(browser, "veggieburger.htm");

            _browser.Link(Find.ByClass("veggieburger")).WaitUntilExists();

            Assert.AreEqual("0", _browser.Eval("window.jQuery('.marker').length"));

            var script = loader.GetCssMarkingScript("a.veggieburger", "marker");
            _browser.RunScript(script);

            Assert.AreEqual("1", _browser.Eval("window.jQuery('.marker').length"));
        }

        [Test]
        [STAThread]
        [TestCase("IE")]
        [TestCase("FireFox")]
        public void TestGetCssMarkRemovalScript_unmarksElements(string browser)
        {
            ScriptLoader loader = new ScriptLoader();

            _browser = GetBrowserWithPage(browser, "veggieburger.htm");

            _browser.Link(Find.ByClass("veggieburger")).WaitUntilExists();

            Assert.AreEqual("1", _browser.Eval("window.jQuery('.veggieburger').length"));

            var script = loader.GetCssMarkRemovalScript("a.veggieburger", "veggieburger");
            _browser.RunScript(script);

            Assert.AreEqual("0", _browser.Eval("window.jQuery('.veggieburger').length"));
        }
    }
}
