using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using FindByCss;
using NUnit.Framework;
using WatiN.Core;

namespace FindByCssTest
{
    [TestFixture]
    class MarkerConstraintTest : BrowserTestFixture
    {
        [Test]
        [STAThread]
        [TestCase("IE")]
        [TestCase("FireFox")]
        public void matches_class_at_beginning_of_string(string browserType)
        {
            UseBrowser(browserType);

            ScriptLoader loader = new ScriptLoader();

            GoToResource("MarkerConstraintTest.htm");

            Browser.Element(Find.ById("first").And(new MarkerConstraint("marker"))).WaitUntilExists(5);
        }

        [Test]
        [STAThread]
        [TestCase("IE")]
        [TestCase("FireFox")]
        public void matches_class_in_middle_of_string(string browserType)
        {
            UseBrowser(browserType);

            ScriptLoader loader = new ScriptLoader();

            GoToResource("MarkerConstraintTest.htm");

            Browser.Element(Find.ById("second").And(new MarkerConstraint("marker"))).WaitUntilExists(5);
        }

        [Test]
        [STAThread]
        [TestCase("IE")]
        [TestCase("FireFox")]
        public void matches_class_at_end_of_string(string browserType)
        {
            UseBrowser(browserType);

            ScriptLoader loader = new ScriptLoader();

            GoToResource("MarkerConstraintTest.htm");

            Browser.Element(Find.ById("third").And(new MarkerConstraint("marker"))).WaitUntilExists(5);
        }

        [Test]
        [STAThread]
        [TestCase("IE")]
        [TestCase("FireFox")]
        public void matches_only_class(string browserType)
        {
            UseBrowser(browserType);

            ScriptLoader loader = new ScriptLoader();

            GoToResource("MarkerConstraintTest.htm");

            Browser.Element(Find.ById("fourth").And(new MarkerConstraint("marker"))).WaitUntilExists(5);
        }
        [Test]
        [STAThread]
        [TestCase("IE")]
        [TestCase("FireFox")]
        public void doesnt_match_other(string browserType)
        {
            UseBrowser(browserType);

            ScriptLoader loader = new ScriptLoader();

            GoToResource("MarkerConstraintTest.htm");

            Thread.Sleep(TimeSpan.FromSeconds(5).Milliseconds);

            Browser.Element(Find.ById("fifth").And( new MarkerConstraint("marker").Not())).WaitUntilExists(5);
        }
    }
}
