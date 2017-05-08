using System.Collections.Generic;
using System.Data;

namespace BaseModel
{
    /// <summary>
    /// 数据源转为Model 作者:苗建龙
    /// </summary>
    public class DeserializeObject
    {
        /// <summary>
        /// DataRow to Model
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="row"></param>
        /// <returns></returns>
        public static T CreateItem<T>(DataRow row) where T : class
        {
            return BaseModel<T>.DynamicAccessor.GetModel(row, GetColumns(row.Table.Columns));
        }

        /// <summary>
        /// DataRow to Model
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="row"></param>
        /// <param name="columns"></param>
        /// <returns></returns>
        public static T CreateItem<T>(DataRow row, IList<string> columns) where T : class
        {
            return BaseModel<T>.DynamicAccessor.GetModel(row, columns);
        }

        /// <summary>
        /// IDataRecord to Model
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="row"></param>
        /// <returns></returns>
        public static T CreateItem<T>(IDataRecord dr) where T : class
        {
            return BaseModel<T>.DynamicAccessor.GetModel(dr, GetColumns(dr));
        }

        /// <summary>
        /// IDataRecord to Model
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="dr"></param>
        /// <param name="columns"></param>
        /// <returns></returns>
        public static T CreateItem<T>(IDataRecord dr, IList<string> columns) where T : class
        {
            return BaseModel<T>.DynamicAccessor.GetModel(dr, columns);
        }

        /// <summary>
        /// IDataRecord to Model
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="row"></param>
        /// <returns></returns>
        public static T CreateItem<T>(IDictionary<string, object> dic) where T : class
        {
            return BaseModel<T>.DynamicAccessor.GetModel(dic);
        }

        /// <summary>
        /// 列明忽略大小写 保持效率
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="table"></param>
        /// <returns></returns>
        public static IList<T> ConvertTo<T>(DataTable table) where T : class
        {
            List<string> cols = GetColumns(table.Columns);
            IList<T> list = new List<T>();
            foreach (DataRow row in table.Rows)
            {
                list.Add(CreateItem<T>(row, cols));
            }
            return list;
        }

        /// <summary>
        /// DataColumnCollection 转List<string> 忽略大小写
        /// </summary>
        /// <param name="cols"></param>
        /// <returns></returns>
        private static List<string> GetColumns(DataColumnCollection cols)
        {
            List<string> columnNames = new List<string>();
            for (int i = 0; i < cols.Count; i++)
            {
                //.ToUpper()可忽略大小写转换
                columnNames.Add(cols[i].ColumnName.ToUpper());
            }
            return columnNames;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="record"></param>
        /// <returns></returns>
        private static List<string> GetColumns(IDataRecord record)
        {
            List<string> columnNames = new List<string>();
            for (int i = 0; i < record.FieldCount; i++)
            {
                //.ToUpper()可忽略大小写转换
                columnNames.Add(record.GetName(i).ToUpper());
            }
            return columnNames;
        }
    }
}
