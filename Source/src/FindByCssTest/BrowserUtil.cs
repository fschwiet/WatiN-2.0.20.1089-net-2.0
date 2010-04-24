using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WatiN.Core;

namespace FindByCssTest
{
    class BrowserUtil
    {
        public static Browser GetBrowser(string browserType)
        {
            if (browserType == "IE")
            {
                return new IE();
            }
            else if (browserType == "FireFox")
            {
                return new FireFox();
            }
            else
            {
                throw new Exception("unrecognized browser attribute");
            }
        }
    }
}
