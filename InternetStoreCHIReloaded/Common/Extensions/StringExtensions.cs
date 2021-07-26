using System;
using System.Collections.Generic;
using System.Text;

namespace Common.Extensions
{
    public static class StringExtensions
    {
        public static bool IsNullEmptyOrWhiteSpace(this string str)
        {
            return string.IsNullOrEmpty(str) || string.IsNullOrWhiteSpace(str);
        }
    }
}
