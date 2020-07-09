using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using log4net;
using NSubstitute;
using NUnit.Framework;
using roundhouse.consoles;
using roundhouse.infrastructure.app;
using static roundhouse.console.tests.ListHelpers;

namespace roundhouse.console.tests.Command_Line_Arguments
{
    [TestFixture]
    public abstract class TestCaseBase<T>: IEnumerable
    {
        private T expected;
        private readonly bool add_required;

        protected TestCaseBase(T expected, bool add_required)
        {
            this.expected = expected;
            this.add_required = add_required;
        }

        private IEnumerable<string> Compact(string f, string a) => List($"{f}{a}={expected}");
        private IEnumerable<string> TwoSeparate(string f, string a) => List($"{f}{a}", expected.ToString());

        private static IEnumerable<IEnumerable<string>> AddRequired(IEnumerable<IEnumerable<string>> test_cases) => 
            test_cases.Select(t => t.Concat(List("-d=database")).ToArray());

        IEnumerator IEnumerable.GetEnumerator() =>
            (add_required 
                ? AddRequired(AllTestCases()) 
                : AllTestCases()
            ).GetEnumerator();

        private IEnumerable<IEnumerable<string>> AllTestCases() => 
            TestCases(Compact)
                .Concat(
                    TestCases(TwoSeparate));

        private IEnumerable<IEnumerable<string>> TestCases(Func<string, string, IEnumerable<string>> map) =>
            from f in List("-", "/", "--")
            from a in variants()
            select map(f, a);

        protected abstract IEnumerable<string> variants();
    }
}