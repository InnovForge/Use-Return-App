using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Text;
using System.Text.RegularExpressions;
namespace Use_Return_App.Helpers
{
    public class Utils
    {
        public static string GenerateSlug(string input)
        {
            string normalized = input.ToLowerInvariant().Normalize(System.Text.NormalizationForm.FormD);
            var sb = new System.Text.StringBuilder();
            foreach (char c in normalized)
            {
                if (System.Globalization.CharUnicodeInfo.GetUnicodeCategory(c) != System.Globalization.UnicodeCategory.NonSpacingMark)
                {
                    if (char.IsLetterOrDigit(c)) sb.Append(c);
                    else if (char.IsWhiteSpace(c)) sb.Append("-");
                }
            }
            return Regex.Replace(sb.ToString(), "-{2,}", "-").Trim('-');
        }

    }
}