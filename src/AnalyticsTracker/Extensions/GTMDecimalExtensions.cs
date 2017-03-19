using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;

namespace Paragon.Analytics.Extensions
{
    public static class GTMDecimalExtensions
    {
        public static string ToGTMCurrencyString(this decimal d)
        {
            return d.ToString("F", NumberFormatInfo.InvariantInfo);

        }
    }
}