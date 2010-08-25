using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using WatiN.Core.Comparers;
using WatiN.Core.Constraints;

namespace FindByCss
{
    public class MarkerConstraint : AttributeConstraint
    {
        public MarkerConstraint(string markerClass)
            //: base("class", new StringContainsAndCaseInsensitiveComparer(markerClass)) 
            : base("class", new Regex(@"(^|\s)" + Regex.Escape(markerClass) + @"(\s|$)"))
        {
        }
    }
}
