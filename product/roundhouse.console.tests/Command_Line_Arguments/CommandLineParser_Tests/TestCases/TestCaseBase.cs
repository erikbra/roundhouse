using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using static roundhouse.console.tests.ListHelpers;

namespace roundhouse.console.tests.Command_Line_Arguments
{
    [TestFixture]
    public abstract class TestCaseBase : IEnumerable
    {
        private readonly string actual;
        private readonly bool add_required;

        protected TestCaseBase(string actual, bool add_required)
        {
            this.actual = actual;
            this.add_required = add_required;
        }

        private IEnumerable<string> Compact(string f, string a) => List($"{f}{a}={actual}");
        private IEnumerable<string> TwoSeparate(string f, string a) => List($"{f}{a}", actual);

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