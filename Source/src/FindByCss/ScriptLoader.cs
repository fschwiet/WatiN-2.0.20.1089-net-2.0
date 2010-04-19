using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using System.IO;
using System.Web.Script.Serialization;

namespace FindByCss
{
    public interface IScriptLoader
    {
        string GetJQueryInstallScript();
        string GetCssMarkingScript(string cssSelector, string markerClass);
        string GetCssMarkRemovalScript(string cssSelector, string markerClass);
    }

    public class ScriptLoader : IScriptLoader
    {
        JavaScriptSerializer serializer = new JavaScriptSerializer();

        private string LoadResourceByName(string name)
        {
            Assembly assembly = Assembly.GetAssembly(typeof(ScriptLoader));

            string resourceName = assembly.GetName().Name + ".Resources." + name;

            StreamReader reader = new StreamReader(assembly.GetManifestResourceStream(resourceName));

            return reader.ReadToEnd();
        }

        public string GetJQueryInstallScript()
        {
            string jQueryIncludeFile = LoadResourceByName("jquery-1.4.2.js");

            return "if (typeof(jQuery) != 'function') {" + jQueryIncludeFile +  "; jQuery.noConflict();}";
        }

        public string GetCssMarkingScript(string cssSelector, string markerClass)
        {
            return
            @"(function(cssSelector, markerClass) 
            { 
                $(cssSelector).addClass(markerClass);
            })(" + serializer.Serialize(cssSelector) + ", " + serializer.Serialize(markerClass) + ");";
        }
            

        public string GetCssMarkRemovalScript(string cssSelector, string markerClass)
        {
            return
            @"(function(cssSelector, markerClass) 
            { 
                $(cssSelector).removeClass(markerClass);
            })(" + serializer.Serialize(cssSelector) + ", " + serializer.Serialize(markerClass) + ");";
        }
    }
}
