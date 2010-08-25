using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WatiN.Core;

namespace FindByCss
{
    public static class DomContainerUtil
    {
        static int _cssMarkerIndex = 0;

        public static Element ElementByCss(this DomContainer domContainer, string cssSelector)
        {
            CssSelectorConstraint constraint = FindByCss(domContainer, cssSelector);

            return domContainer.Element(constraint);
        }

        public static CssSelectorConstraint FindByCss(DomContainer domContainer, string cssSelector)
        {
            string cssMarker = "findByCssMarker" + ++_cssMarkerIndex;

            var constraint = new CssSelectorConstraint(new ScriptLoader(), domContainer);
            constraint.Initialize(cssSelector, cssMarker);
            return constraint;
        }
    }
}
