using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Automation.TestRail.Extensions
{
    internal static class StringExtensions
    {
        public static string SnakeCaseToPascalCase(this string str)
        {
            return str
                .Split(new[] { "_" }, StringSplitOptions.RemoveEmptyEntries)
                .Select(s => char.ToUpperInvariant(s[0]) + s.Substring(1, s.Length - 1))
                .Aggregate(string.Empty, (s1, s2) => s1 + s2);
        }
    }
}