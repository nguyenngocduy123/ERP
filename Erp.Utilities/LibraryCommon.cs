using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Web;

namespace Erp.Utilities
{
    using System.IO;

    public sealed class LibraryCommon
    {
        private LibraryCommon() { }

        private static readonly string[] VietnameseSigns = new string[]
        {
            "aAeEoOuUiIdDyY",
            "áàạảãâấầậẩẫăắằặẳẵ",
            "ÁÀẠẢÃÂẤẦẬẨẪĂẮẰẶẲẴ",
            "éèẹẻẽêếềệểễ",
            "ÉÈẸẺẼÊẾỀỆỂỄ",
            "óòọỏõôốồộổỗơớờợởỡ",
            "ÓÒỌỎÕÔỐỒỘỔỖƠỚỜỢỞỠ",
            "úùụủũưứừựửữ",
            "ÚÙỤỦŨƯỨỪỰỬỮ",
            "íìịỉĩ",
            "ÍÌỊỈĨ",
            "đ",
            "Đ",
            "ýỳỵỷỹ",
            "ÝỲỴỶỸ"
        };

        private const string PasswordSigns = "ABCDEFGHYKLMNOPQRSTUVWXYZabcdefghiklmnopqrstuvwxyz123456789~!@#$%*+?";
        private const string PasswordSignsSimple = "ABCDEFGHYKLMNPQRSTUVWXYZ123456789";
                
        public static string FormatIdString(string value)
        {
            for (int i = 1; i < VietnameseSigns.Length; i++)
            {
                for (int j = 0; j < VietnameseSigns[i].Length; j++)
                    value = value.Replace(VietnameseSigns[i][j], VietnameseSigns[0][i - 1]);
            }
            value = value.Trim();
            value = value.Replace("  ", " ");
            value = value.Replace(" ", string.Empty);
            value = value.Replace("-", string.Empty);
            value = value.Replace("_", string.Empty);
            value = value.Replace(":", string.Empty);
            value = value.Replace(";", string.Empty);
            value = value.Replace("(", string.Empty);
            value = value.Replace(")", string.Empty);
            value = value.Replace("/", string.Empty);
            value = value.Replace("+", string.Empty);
            value = value.Replace("?", string.Empty);
            value = value.Replace("%", string.Empty);
            value = value.Replace("#", string.Empty);
            value = value.Replace("&", string.Empty);
            value = value.Replace("<", string.Empty);
            value = value.Replace(">", string.Empty);
            value = value.Replace(@"\", string.Empty);
            value = value.Replace("\"", string.Empty);
            value = value.Replace("'", string.Empty);
            value = value.Replace("`", string.Empty);
            value = value.Replace("$", string.Empty);
            value = value.Replace(".", string.Empty);
            value = value.Replace(",", string.Empty);
            value = value.Replace("=", string.Empty);
            value = value.Replace("@", string.Empty);
            value = value.Replace("{", string.Empty);
            value = value.Replace("}", string.Empty);
            value = value.Replace("|", string.Empty);
            value = value.Replace("^", string.Empty);
            value = value.Replace("~", string.Empty);
            value = value.Replace("[", string.Empty);
            value = value.Replace("]", string.Empty);

            //return HttpUtility.HtmlEncode(urlString.ToLower());
            return HttpUtility.HtmlEncode(value);
        }

        public static string FormatString(int count, string value)
        {
            if (value != null)
            {
                value = Regex.Replace(value, "<[^>]*>", "", RegexOptions.IgnoreCase | RegexOptions.Singleline);
                if (value.Length > count)
                {
                    value = value.Substring(0, count);
                    if (value.LastIndexOf(" ") > 0)
                        value = value.Substring(0, value.LastIndexOf(" ")) + "...";
                }
            }
            return value;
        }

        public static string GetPathBackEnd(string fileName)
        {
            if (!string.IsNullOrEmpty(fileName))
            {
                return Path.Combine(Globals.BackEndUrl,Globals.BackEndUrl.EndsWith("/") ? fileName.Replace("~/", string.Empty) : fileName.Replace("~", string.Empty));
            }
            else
            {
                return string.Empty;
            }
        }

        public static string GetPathFrontEnd(string fileName)
        {
            if (!string.IsNullOrEmpty(fileName))
            {
                return Path.Combine(
                    Globals.FrontEndUrl,
                    Globals.FrontEndUrl.EndsWith("/")
                        ? fileName.Replace("~/", string.Empty)
                        : fileName.Replace("~", string.Empty));
            }
            else
            {
                return string.Empty;
            }
        }

        public static int GetWeekOfMonth(DateTime date)
        {
            int NumberWeek = 1;            
            int DateOfWeek = (int)date.DayOfWeek != 0 ? (int)date.DayOfWeek : 7;
            int Weekend = 7 - DateOfWeek;
            date = date.AddDays(Weekend);
            while(date.Day - 7 > 1)
            {
                if (date.Day - 7 > 1)
                {
                    date = date.AddDays(-7);
                    NumberWeek++;
                }
            }
            return NumberWeek > 4 ? 4 : NumberWeek;
        }

        public static bool Validate(string text, string pattern)
        {
            Regex regex = new Regex(pattern, RegexOptions.Singleline | RegexOptions.IgnoreCase);
            return regex.IsMatch(text);
        }

        public static bool ValidateEmail(string email)
        {
            return Validate(email, @"^\w+([-+.]\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*$");
        }

        public static bool ValidateUrl(string value)
        {
            return Validate(value, @"^http://([\w-]+\.)+[\w-]+(/[\w- ./?%&=]*)?$");
        }

        public static string GenerateNewPassword(int passwordLength)
        {
            StringBuilder sbNewPassword = new StringBuilder();
            if(passwordLength < 6)
                passwordLength = 6;

            Random random = new Random();
            int randomNumber, index;
            char[] passwordChars = PasswordSigns.ToCharArray();
            for (int i = 0; i < passwordLength; i++)
            {
                randomNumber = random.Next(0, PasswordSigns.Length);
                index = randomNumber % PasswordSigns.Length;
                sbNewPassword.Append(passwordChars[index]);

            }
            return sbNewPassword.ToString();
        }

        public static string GenerateNewPasswordSimple(int passwordLength)
        {
            StringBuilder sbNewPassword = new StringBuilder();
            if (passwordLength < 6)
                passwordLength = 6;

            Random random = new Random();
            int randomNumber, index;
            char[] passwordChars = PasswordSignsSimple.ToCharArray();
            for (int i = 0; i < passwordLength; i++)
            {
                randomNumber = random.Next(0, PasswordSignsSimple.Length);
                index = randomNumber % PasswordSignsSimple.Length;
                sbNewPassword.Append(passwordChars[index]);

            }
            return sbNewPassword.ToString();
        }
    }
}
