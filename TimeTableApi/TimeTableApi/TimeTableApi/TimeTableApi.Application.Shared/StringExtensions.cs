using System;
using System.Collections.Generic;
using System.Text;

namespace TimeTableApi.Application.Shared
{
    public static class StringExtensions
    {
        public static string RemovePostFix(this string str, params string[] postFixes)
        {
            if (str == null) {
                return null;
            }

            if (str == string.Empty)
            {
                return string.Empty;
            }

            if (postFixes.Length == 0)
            {
                return str;
            }

            foreach (var postFix in postFixes)
            {
                if(str.EndsWith(postFix))
                {
                    return str.Left(str.Length - postFix.Length);
                }
            }

            return str;
        }

        public static string Left(this string str, int len)
        {
            if (str == null)
            {
                throw new ArgumentNullException("str");
            }

            if (str.Length < len)
            {
                throw new ArgumentException("len argument can not be bigger than given string's length!");
            }

            return str.Substring(0, len);
        }
    }
}
