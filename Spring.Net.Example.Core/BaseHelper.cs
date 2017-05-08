using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spring.Net.Example.Core
{
    public static class BaseHelper
    {
        /// <summary>
		/// 获取字符串的字节长度
		/// </summary>
		/// <param name="str"></param>
		/// <returns></returns>
		public static int GetLength(this string str)
        {
            return string.IsNullOrEmpty(str) ? 0 : Encoding.Default.GetByteCount(str);
        }

        /// <summary>
        /// 拆分字符串1,2,3,4,5 为 '1','2','3','4','5'
        /// </summary>
        /// <param name="strid">1,2,3,4,5</param>
        /// <returns></returns>
        public static string StrArr(this string strid)
        {
            string StrValue = "";
            if (!string.IsNullOrEmpty(strid))
            {
                string[] strarr = strid.Split(',');

                foreach (string item in strarr)
                {
                    StrValue += "'" + item.Trim() + "',";

                }
                StrValue = StrValue.Substring(0, StrValue.Length - 1);
            }
            return StrValue;
        }

        /// <summary>
        /// 转换list string 为插入'..','...'格式
        /// </summary>
        /// <param name="lists"></param>
        /// <returns></returns>
        public static string DataBaseStr(this IList<string> lists)
        {
            string StrValue = string.Empty;
            if (null != lists && lists.Count() > 0)
            {
                foreach (string str in lists)
                {
                    StrValue += string.Format("'{0}',", str.Replace("'", "''"));
                }

                StrValue = StrValue.Substring(0, StrValue.Length - 1);
            }
            return StrValue;
        }

        /// <summary>
        /// 获取GUID
        /// </summary>
        /// <returns></returns>
        public static string GetNewGuid(this object obj)
        {
            return Guid.NewGuid().ToString().Replace("-", "");
        }

        //判断正则
        /// <summary>
        /// 判断正则
        /// </summary>
        /// <param name="itemValue"></param>
        /// <param name="regExValue"></param>
        /// <returns></returns>
        public static bool IsRegEx(this string itemValue, string regExValue)
        {
            try
            {
                System.Text.RegularExpressions.Regex regex = new System.Text.RegularExpressions.Regex(regExValue);
                if (regex.IsMatch(itemValue)) return true;
                else return false;
            }
            catch (Exception)
            {
                return false;
            }
            finally
            {
            }
        }
    }
}
