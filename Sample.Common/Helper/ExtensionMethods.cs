using System;
using System.Collections.Generic;
using System.Text;

namespace Sample.Common.Helper
{
    public static class ExtensionMethods
    {
        public static string MyDateFormat(this DateTime input)
        {
            return input.ToString("MMM dd, yyyy");
        }
    }
}
