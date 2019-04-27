using System;
using System.Globalization;
using System.Linq;
using System.Text;

namespace Membership_Management.Infrastructure.Helpers
{
    public static class StringExtensions
    {
        private static TextInfo myTI = new CultureInfo("hr-HR", false).TextInfo;
        private static Encoding defaultEncoder = Encoding.Default;

        public static string ToPascalCase(this string str)
        {

            string sample = string.Join("", str?.Select(c => Char.IsLetterOrDigit(c) ? c.ToString().ToLower() : "_").ToArray());

            var arr = sample?
                .Split(new[] { '_' }, StringSplitOptions.RemoveEmptyEntries)
                .Select(s => $"{s.Substring(0, 1).ToUpper()}{s.Substring(1)}");

            return string.Join("", arr);
        }

        public static string ToPascalHRCase(this string str)
        {
            return myTI.ToTitleCase(Encoding.UTF8.GetString(defaultEncoder.GetBytes(str)));
        }
    }
}
