using System.Text;
using NUnit.Framework.Internal.Execution;

namespace TAEssentials.UI.Extensions
{
    public static class StringExtensions
    {
        public static string SubstringIfLongerThan(this string str, int maxLength) => str.Length >= maxLength ? str.Substring(0, maxLength) : str;

        public static string ToRandomCase(this string str)
        {
            var rand = new Random();
            StringBuilder result = new StringBuilder(str.Length);

            foreach (char c in str)
            {
                bool toUpper = rand.Next(2) == 0;
                result.Append(toUpper ? char.ToUpper(c) : char.ToLower(c));
            }
            return result.ToString();
        }
    }
}