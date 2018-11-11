using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Erp.BackOffice.Helpers
{
    public static class StringExtention
    {
        public static string ToLowerOrEmpty(this string input)
        {
            return (input != null ? input.ToLower() : "");
        }
    }
}