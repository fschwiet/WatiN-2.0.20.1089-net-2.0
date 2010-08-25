using System;
using NUnit.Framework;
using WatiN.Core;
using WatiN.Core.UnitTests.TestUtils;

namespace FindByCssTest
{
    public class BrowserTestFixture
    {
        public enum BrowserType
        {
            IE,
            FireFox
        }

        private Browser _browser;
        private Browser _browserToDispose;
        private BrowserType _browserType;

        protected void UseBrowser(BrowserType browserType)
        {
            ClearBrowser();
            _browserType = browserType;
        }

        protected void UseBrowser(string browserType)
        {
            UseBrowser((BrowserType)Enum.Parse(typeof(BrowserType), browserType));
        }

        protected void LeakBrowser()
        {
            _browserToDispose = null;
        }

        protected Browser Browser
        {
            get {
                if (_browser == null)
                { 
                    if (_browserType == BrowserType.IE)
                        _browser = new IE();
                    else
                        _browser = new FireFox();

                    _browserToDispose = _browser;
                }

                return _browser;
            }
        }

        [SetUp]
        public void LoadBrowser()
        {
            _browser = null;
            _browserToDispose = null;
        }

        [TearDown]
        public void ClearBrowser()
        {
            if (_browserToDispose != null)
            {
                _browserToDispose.Dispose();
            }

            _browser = null;
            _browserToDispose = null;
        }

        protected void GoToResource(string htmlFilename)
        {
            var url = new Uri(BaseWatiNTest.GetHtmlTestFilesLocation("html") + htmlFilename);
            Browser.GoTo(url);
        }
    }
}