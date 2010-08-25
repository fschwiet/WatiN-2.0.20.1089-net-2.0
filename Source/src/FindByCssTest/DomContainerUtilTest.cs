using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using FindByCss;
using WatiN.Core;

namespace FindByCssTest
{
    [TestFixture]
    public class DomContainerUtilTest : BrowserTestFixture
    {
        [Test]
        [STAThread]
        [TestCase("IE")]
        [TestCase("FireFox")]
        public void TestFindByCss(string browserType)
        {
            UseBrowser(browserType);

            Browser.GoTo("http://allrecipes.com/");
            LeakBrowser();
            Browser.Link(Find.ByClass("linkRequiresLogin")).WaitUntilExists(5);

            Browser.Link(DomContainerUtil.FindByCss(Browser.DomContainer, ".linkRequiresLogin")).WaitUntilExists(5);
            //.And(DomContainerUtil.FindByCss(Browser, "body"))
            //Browser.Element(Find.ById("main")
            //    .And(DomContainerUtil.FindByCss(Browser, "#main"))
            //    ).WaitUntilExists();
        }
    }
}
