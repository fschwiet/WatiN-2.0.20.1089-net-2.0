using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using WatiN.Core.Constraints;
using WatiN.Core.Interfaces;
using WatiN.Core;

namespace FindByCss
{
    public class CssSelectorConstraint : WatiN.Core.Constraints.Constraint
    {
        private readonly DomContainer _domContainer;
        private readonly IScriptLoader _scriptLoader;
        private string _cssSelector;
        private string _markerClass;

        public virtual Constraint ActualConstraint { get; protected set; }

        public CssSelectorConstraint(IScriptLoader scriptLoader, DomContainer domContainer)
        {
            _scriptLoader = scriptLoader;
            _domContainer = domContainer;
        }

        public void Initialize(string cssSelector, string markerClass)
        {
            _cssSelector = cssSelector;
            _markerClass = markerClass;

            ActualConstraint = new AttributeConstraint("class",
                new Regex(@"(^|\s)" + Regex.Escape(markerClass) + @"(\s|$)"));
        }

        public override void WriteDescriptionTo(TextWriter writer)
        {
            writer.Write(String.Format("CssSelectorConstraint: {0}", _cssSelector));
        }

        public override void EnterMatch()
        {
            base.EnterMatch();

            var jqInstallScript = _scriptLoader.GetJQueryInstallScript();
            _domContainer.Eval(jqInstallScript);

            var markingScript = _scriptLoader.GetCssMarkingScript(_cssSelector, _markerClass);
            _domContainer.Eval(markingScript);
        }

        public override bool MatchesImpl(IAttributeBag attributeBag, ConstraintContext context)
        {
            return ActualConstraint.MatchesImpl(attributeBag, context);
        }

        public override void ExitMatch()
        {
            var unmarkingScript = _scriptLoader.GetCssMarkRemovalScript(_cssSelector, _markerClass);
            _domContainer.Eval(unmarkingScript);

            base.ExitMatch();
        }
    }
}
