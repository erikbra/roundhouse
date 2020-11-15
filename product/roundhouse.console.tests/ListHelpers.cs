using System.Collections.Generic;

namespace roundhouse.console.tests
{
    public static class ListHelpers
    {
        public static IEnumerable<T> List<T>(params T[] values) => values;
    }
}