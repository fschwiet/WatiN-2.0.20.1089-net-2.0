using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FindByCss;
using NUnit.Framework;

namespace FindByCssTest
{
    [TestFixture]
    class ScriptLoaderTest
    {
        [Test]
        public void TestCanLoadJqueryInstallScript()
        {
            var sut = new ScriptLoader();

            string result = sut.GetJQueryInstallScript();

            Assert.IsTrue(result.Length > 100);
        }


        [Test]
        public void TestGetCssMarkingScript()
        {
            string cssSelector = Some.String();
            string markerClass = Some.String();

            var sut = new ScriptLoader();

            var result = sut.GetCssMarkingScript(cssSelector, markerClass);

            Assert.IsTrue(result.Length > 10);
        }


        [Test]
        public void TestGetCssMarkRemovalScript()
        {
            string cssSelector = Some.String();
            string markerClass = Some.String();

            var sut = new ScriptLoader();

            var result = sut.GetCssMarkRemovalScript(cssSelector, markerClass);

            Assert.IsTrue(result.Length > 10);
        }
    }
}
