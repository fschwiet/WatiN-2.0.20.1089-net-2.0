using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using FindByCss;
using Moq;
using WatiN.Core;
using System.Text.RegularExpressions;
using WatiN.Core.Constraints;
using WatiN.Core.Interfaces;
using NUnit.Framework;
using Match=Moq.Match;


namespace FindByCssTest
{
    [TestFixture]
    class CssSelectorConstraintTest
    {
        [Test]
        public void TestCanBeCreated()
        {
            var sut = new CssSelectorConstraint(null,null);

            Assert.IsInstanceOf(typeof(WatiN.Core.Constraints.Constraint), sut);
        }

        [Test]
        public void TestInitialize_createsAClassConstraint()
        {
            string markerClass = Some.String();

            var sut = new CssSelectorConstraint(null,null);

            sut.Initialize(null, markerClass);

            AttributeConstraint constraint = sut.ActualConstraint as AttributeConstraint;
            Assert.That(constraint.AttributeName.Equals("class"));
        }

        public bool doesRegex_usedByClassConstraint_match(string markerClass, string classValue)
        {
            var sut = new CssSelectorConstraint(null,null);

            sut.Initialize(null, markerClass);

            AttributeConstraint constraint = sut.ActualConstraint as AttributeConstraint;
            return constraint.Comparer.Compare(classValue);
        }

        [Test]
        public void TestInitialize_regex_works()
        {
            Assert.IsTrue(doesRegex_usedByClassConstraint_match("one", "one"));
            Assert.IsTrue(doesRegex_usedByClassConstraint_match("one", "two one"));
            Assert.IsTrue(doesRegex_usedByClassConstraint_match("one", "one two"));
            Assert.IsFalse(doesRegex_usedByClassConstraint_match("one", "two"));
            Assert.IsFalse(doesRegex_usedByClassConstraint_match("one", "onetwo"));
        }

        [Test]
        public void TestEnterMatch_ensuresJQueryIsAvailable()
        {
            string jQueryInstallScript = Some.String();

            Mock<IScriptLoader> scriptLoader = new Mock<IScriptLoader>();
            Mock<WatiN.Core.DomContainer> domContainer = new Mock<DomContainer>();
            
            scriptLoader.Setup(s => s.GetJQueryInstallScript()).Returns(jQueryInstallScript);
            domContainer.Setup(s => s.Eval(jQueryInstallScript)).Verifiable();

            var sut = new CssSelectorConstraint(scriptLoader.Object, domContainer.Object);

            sut.EnterMatch();

            domContainer.Verify();
        }

        [Test]
        public void TestEnterMatch_usesCssSelectorToAddCssClass()
        {
            string cssSelector = Some.String();
            string markerClass = Some.String();

            string markingScript = Some.String();

            Mock<IScriptLoader> scriptLoader = new Mock<IScriptLoader>();
            Mock<WatiN.Core.DomContainer> domContainer = new Mock<DomContainer>();

            scriptLoader.Setup(s => s.GetCssMarkingScript(cssSelector, markerClass)).Returns(markingScript);
            domContainer.Setup(d => d.Eval(markingScript)).Verifiable();

            var sut = new CssSelectorConstraint(scriptLoader.Object, domContainer.Object);

            sut.Initialize(cssSelector, markerClass);

            sut.EnterMatch();

            domContainer.Verify(); 
        }


        [Test]
        public void TestExitMatch_removesCssClass()
        {
            string cssSelector = Some.String();
            string markerClass = Some.String();

            string markingScript = Some.String();

            Mock<IScriptLoader> scriptLoader = new Mock<IScriptLoader>();
            Mock<WatiN.Core.DomContainer> domContainer = new Mock<DomContainer>();

            scriptLoader.Setup(s => s.GetCssMarkRemovalScript(cssSelector, markerClass)).Returns(markingScript);
            domContainer.Setup(d => d.Eval(markingScript)).Verifiable();

            var sut = new CssSelectorConstraint(scriptLoader.Object, domContainer.Object);

            sut.Initialize(cssSelector, markerClass);

            sut.ExitMatch();

            domContainer.Verify();
        }


        [Test]
        public void TestWriteDescriptionTo()
        {
            string cssSelector = Some.String();
            string markerClass = Some.String();

            Mock<System.IO.TextWriter> textWriter = new Mock<TextWriter>();
            textWriter.Setup(tw => tw.Write(It.IsAny<string>())).Callback(
                delegate(string writtenText)
                {
                    Assert.That(writtenText.Contains("CssSelectorConstraint"));
                    Assert.That(writtenText.Contains(cssSelector));
                }
                ).Verifiable();

            var sut = new CssSelectorConstraint(null, null);

            sut.Initialize(cssSelector, markerClass);

            sut.WriteDescriptionTo(textWriter.Object);

            textWriter.Verify();
        }

        [Test]
        public void TestWriteDescriptionTo_uninitialized()
        {
            Mock<System.IO.TextWriter> textWriter = new Mock<TextWriter>();
            textWriter.Setup(tw => tw.Write(It.IsAny<string>())).Callback(
                delegate(string writtenText)
                {
                    Assert.That(writtenText.Contains("CssSelectorConstraint"));
                }
                ).Verifiable();

            var sut = new CssSelectorConstraint(null, null);

            sut.WriteDescriptionTo(textWriter.Object);

            textWriter.Verify();
        }

        class CssSelectorConstraint_allowingMockedInternal : CssSelectorConstraint
        {
            public CssSelectorConstraint_allowingMockedInternal() : base(null, null)
            {
            }

            public void SetActualConstraint(Constraint constraint)
            {
                ActualConstraint = constraint;
            }
        }

        public abstract class AttributeConstraint_withouConstructorParameters : AttributeConstraint
        {
            public AttributeConstraint_withouConstructorParameters()
                : base("class", new Regex(""))
            {
            }
        }

        [Test]
        public void TestMatchesImpl()
        {
            Mock<IAttributeBag> attributeBag = new Mock<IAttributeBag>();
            ConstraintContext constraintContext = new ConstraintContext();
            Mock<Constraint> constraint = new Mock<Constraint>();

            constraint.Expect(c => c.MatchesImpl(attributeBag.Object, constraintContext))
                .Verifiable();

            CssSelectorConstraint_allowingMockedInternal sut = new CssSelectorConstraint_allowingMockedInternal();

            sut.SetActualConstraint(constraint.Object);

            sut.MatchesImpl(attributeBag.Object, constraintContext);

            constraint.Verify();
        }
    }
}
