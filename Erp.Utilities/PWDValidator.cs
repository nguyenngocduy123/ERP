using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Erp.Utilities
{
    public class PWDValidator
    {
        static string CheckPassword2(string uId, string uPwd)
        {
            string ReturnData = "";

            if (uPwd.IndexOf(uId) > -1)
            {
                ReturnData = "Password không được phép chứa USERID";
            }

            return ReturnData;
        }



        public static string DataValidCheck(string Passwd, String RePasswd)
        {
            string ReturnData = "";

            //if (UserID.Length < 6)  //id =>ID는 6자 이상
            //{
            //    ReturnData = "Chiều dài UserID ít nhất là 8 ký tự";
            //    return ReturnData;
            //}

            //if (UserID.Length < 4) //이름은 4자 이상
            //{
            //    ReturnData = "Chiều dài UserID ít nhất là 4 ký tự";
            //    return ReturnData;
            //}


            if (Passwd.Length < 8)  //pw 8자 이상
            {
                ReturnData = "Chiều dài Password ít nhất là 8 ký tự";
                return ReturnData;
            }

            //영문자+숫자 조합은 필수

            string ChrPattern = @"^[a-zA-Z]*$";
            string NumPattern = @"^[0-9]*$";
            bool result = false;

            if (!(System.Text.RegularExpressions.Regex.IsMatch(Passwd, ChrPattern)) && !(System.Text.RegularExpressions.Regex.IsMatch(Passwd, NumPattern)))
            {
                result = true;
            }

            if (!result)
            {

                ReturnData = "Password chỉ chấp nhận các ký tự là chữ cái và số";
                return ReturnData;
            }

            //이어지는 숫자 4개이상 사용금지

            string temp = "";
            string strValidation = "0123456789";
            string strValidation2 = "9876543210";

            for (int i = 4; i <= Passwd.Length; i++)
            {

                temp = Passwd.Substring(i - 4, 4);

                if (strValidation.IndexOf(temp) > -1)
                {
                    result = false;
                    break;
                }

                if (strValidation2.IndexOf(temp) > -1)
                {
                    result = false;
                    break;
                }
            }

            string temp2 = Passwd.Substring(0, 1);
            int con = 1;

            if (!result)
            {
                ReturnData = "Không được dùng quá 3 ký số tăng hoặc giảm liên tiếp";
                return ReturnData;
            }

            //동일문자 4개이상 사용금지
            for (int j = 1; j < Passwd.Length; j++)
            {

                temp = Passwd.Substring(j, 1);

                if (temp == temp2)
                {
                    con++;
                }
                else
                {
                    con = 1;
                    temp2 = temp;
                }

                if (con >= 4)
                {
                    result = false;
                    break;
                }
            }


            if (!result)
            {
                ReturnData = "Không được dùng một ký số quá 3 lần liên tiếp";
                return ReturnData;
            }

            if (!Passwd.Equals(""))
            {
                if (Passwd.Equals(""))
                {
                    ReturnData = "Vui lòng nhập Password";
                    return ReturnData;
                }

                if (RePasswd.Equals(""))
                {
                    ReturnData = "Vui lòng nhập Nhập lại Password";
                    return ReturnData;
                }

                if (Passwd != RePasswd)
                {
                    ReturnData = "Password và Nhập lại Password không khớp";
                    return ReturnData;
                }


                //ReturnData = CheckPassword2(UserID, Passwd);
                if (!ReturnData.Equals("")) return ReturnData;


                //2014.02.11 특수문자가 반듯이 1개 필요
                //~,!,@,#,$,%,&,*,-,=,_,+,[,],{,}
                ReturnData = CheckSpecialCharacterPwd(Passwd);
                if (!ReturnData.Equals("")) return ReturnData;
            }
            return ReturnData;
        }

        //2014.02.11 특수문자가 반듯이 1개 필요
        //~,!,@,#,$,%,&,*,-,=,_,+,[,],{,}
        static string CheckSpecialCharacterPwd(string uPwd)
        {
            string ReturnData = "";
            string[] saCheckString = "~,!,@,#,$,%,&,*,-,=,_,+,[,],{,}".Split(',');
            ReturnData = "Password phải chứa ít nhất một ký tự đặc biệt. ~,!,@,#,$,%,&,*,-,=,_,+,[,],{,}";

            for (int i = 0; i <= saCheckString.GetUpperBound(0) - 1; i++)
            {
                if (uPwd.IndexOf(saCheckString[i]) > -1)
                {
                    ReturnData = "";
                    break;
                }
            }

            return ReturnData;
        }
    }
}
