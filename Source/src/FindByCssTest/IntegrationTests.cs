using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using NUnit.Framework;
using WatiN.Core;
using FindByCss;

namespace FindByCssTest
{
    [TestFixture]
    [Explicit]
    public class IntegrationTests
    {
       [Test]
        [STAThread]
        public void TestEnterMatch_loadsJQueryIfNotLoaded()
        {
            ScriptLoader loader = new ScriptLoader();

            FireFox ff = new FireFox();

//            ff.Eval("window.alert('hi');");

            //string js = loader.GetJQueryInstallScript();
            //ff.RunScript(js);

            for (int i = 1; i < Math.Pow(2, 24); i = i * 2)
            {
                string script = String.Join("", Enumerable.Range(1, i).Select(nn => "var i = 1;").ToArray());
                Console.WriteLine("running for script of length " + script.Length);
                ff.RunScript(script);
            }

        }

       [Test]
        public void TestEnterMatch_doesNotReloadJQueryOnceLoaded()
        {
            Assert.Fail("Run browser against page that has jquery loaded, verify it is not overwritten.");
        }

        [Test]
        public void Test_generatedScriptCanSetClassWithCssSelector()
        {
            
        }

        [Test]
        public void Test_generatedScriptCanRemoveClassWithCssSelector()
        {
            
        }


    }
}
