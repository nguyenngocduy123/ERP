
using System;

namespace Erp.Domain
{
    public static class UrlCommon
    {
        public const string C_System = "System";
        public const string U_UserManager = "api/ssp/v01/user";
        public const string U_UserManager_CheckLogin = "login";
        public const string U_UserManager_ChangePassword = "ChangePassword";
        public const string P_Product_Getlist = "GetlistProduct";
        public const string DiscountBy_productID = "GetDiscountByID";
        public const string List_Invoice = "GetList_Invoice";
        public const string Save_Invoice = "Save_Invoice";
        public const string Getdetail_Invoice = "Getdetail_Invoice";

        public const string GetCustomerbyPhone = "GetCustomerbyPhone";


        public static DateTime EndOfDay(this DateTime date)
        {
            return new DateTime(date.Year, date.Month, date.Day, 23, 59, 59, 999);
        }

        public static DateTime StartOfDay(this DateTime date)
        {
            return new DateTime(date.Year, date.Month, date.Day, 0, 0, 0, 0);
        }


        public static long NVL_NUM_LONG_NEW(object str)
        {
            if ((str != System.DBNull.Value) && (str != null))
            {
                if (str.ToString().Trim().Equals("") == false)
                {
                    return long.Parse(str.ToString());
                }
                else
                {
                    return 0;
                }
            }
            else
            {
                return 0;
            }
        }

        public static double NVL_NUM_DOUBLE_NEW(object str)
        {
            if ((str != System.DBNull.Value) && (str != null))
            {
                if (str.ToString().Trim().Equals("") == false)
                {
                    return double.Parse(str.ToString());
                }
                else
                {
                    return 0;
                }
            }
            else
            {
                return 0;
            }
        }


        public static decimal NVL_NUM_DECIMAL_NEW(object str)
        {
            if ((str != System.DBNull.Value) && (str != null))
            {
                if (str.ToString().Trim().Equals("") == false)
                {
                    return decimal.Parse(str.ToString());
                }
                else
                {
                    return 0;
                }
            }
            else
            {
                return 0;
            }
        }


        public static int NVL_NUM_NT_NEW(object str)
        {
            if ((str != System.DBNull.Value) && (str != null))
            {
                if (str.Equals("") == false)
                {
                    return int.Parse(str.ToString());
                }
                else
                {
                    return 0;
                }
            }
            else
            {
                return 0;
            }
        }


        public static int NVL_NUM_INT(string str)
        {
            if (str.Trim() != "")
            {
                return int.Parse(str);
            }
            else
            {
                return 0;
            }
        }



        public static String NVL_NUM_STRING(Object str)
        {
            if (str != System.DBNull.Value)
            {
                return str.ToString();
            }
            else
            {
                return "";
            }
        }

        public static DateTime NVL_DATETIME(Object str)
        {
            if (str != System.DBNull.Value)
            {
                if (str.ToString().Trim() != "")
                {
                    return Convert.ToDateTime(str);
                }
            }
            return Convert.ToDateTime("01/01/0001");
        }

        public static String NVL_DATETIME_STR(Object str)
        {
            if (str != System.DBNull.Value)
            {
                if (str.ToString().Trim() != "")
                {
                    return Convert.ToDateTime(str).ToString("dd/MM/yyyy");
                }
            }
            return "";
        }

    }
}