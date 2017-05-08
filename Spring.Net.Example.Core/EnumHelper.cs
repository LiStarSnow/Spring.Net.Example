/* ***********************************************
 * author :  苗建龙
 * function: 枚举处理工具类
 * history:  created by 苗建龙 2015/7/8 15:12:18 
 * ***********************************************/
namespace Spring.Net.Core
{
    using Model.Dto.Shared;
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// 枚举处理工具类
    /// </summary>
    public class EnumHelper
    {
        /// <summary>
        /// 枚举转 List
        /// </summary>
        /// <param name="enumType">枚举Type</param>
        /// <returns>List</returns>
        public static List<TextValue> ToTextValueList(Type enumType, string textFormat = null)
        {
            List<TextValue> list = new List<TextValue>();

            foreach (var i in Enum.GetValues(enumType))
            {
                var text = Enum.GetName(enumType, i);
                list.Add(new TextValue()
                {
                    Text = string.IsNullOrEmpty(textFormat) ? text : string.Format(textFormat, text),
                    Value = i
                });
            }

            return list;
        }
    }
}
