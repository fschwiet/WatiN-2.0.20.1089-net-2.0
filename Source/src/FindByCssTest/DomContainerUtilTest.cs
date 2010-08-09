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
    public class DomContainerUtilTest
    {
        Browser _browser = null;

        [TearDown]
        public void OnDone()
        {
            //if (_browser != null)
            //    _browser.Close();

            _browser = null;
        }
        
        [Test]
        [STAThread]
        [TestCase("IE")]
        [TestCase("FireFox")]
        public void TestFindByCss(string browserType)
        {
            _browser = BrowserUtil.GetBrowser(browserType);

            _browser.GoTo("http://www.google.com");

            _browser.FindByCss("input").WaitUntilExists();
        }
    }
}
