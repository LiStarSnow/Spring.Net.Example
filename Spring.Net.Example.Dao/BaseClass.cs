using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OracleClient;
using System.Linq;
using System.Reflection;
using System.Text;

namespace Spring.Net.Example.Dao
{

	public static class BaseClass
	{
		/// <summary>
		/// 拆分字符串1,2,3,4,5
		/// </summary>
		/// <param name="strid">1,2,3,4,5</param>
		/// <returns></returns>
		public static string StrArr(string strid)
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
		/// 获取字符串的字节长度
		/// </summary>
		/// <param name="str"></param>
		/// <returns></returns>
		public static int GetLength(this string str)
		{
			return Encoding.Default.GetByteCount(str);
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

		#region 获取日期环比(日期类型：201301)
		public static string ChangeToLinkRelativeRatio(string date)
		{
			try
			{
				string changed = "";
				string startY = date.Substring(0, 4);
				string startM = date.Substring(4, 2);
				if (startM == "01")
				{
					changed = (Convert.ToInt32(startY) - 1).ToString() + "12";
				}
				else
				{
					changed = (Convert.ToInt32(date) - 1).ToString();
				}
				return changed;
			}
			catch
			{
				return null;
			}
		}

		#endregion 获取日期环比(日期类型：201301)

		/// <summary>
		/// 将指定字符串转换为插入DB用的字符串
		/// </summary>
		/// <param name="value">指定字符串</param>
		/// <returns>转换后字符串</returns>
		public static string FormatValue(object value)
		{
			string strValue = string.Empty;

			// 空字符串时返回空
			if (value == DBNull.Value || value == null)
			{
				return "''";
			}

			strValue = value.ToString().Trim().Replace("'", "''");

			return string.Format("'{0}'", strValue);
		}

		/// <summary>
		/// 分页检索
		/// </summary>
		/// <param name="pageSize">页大小</param>
		/// <param name="pageIndex">当前第几页</param>
		/// <param name="strSQL">SQL查询语句</param>
		/// <param name="totalCount">满足查询条件的总记录数</param>
		/// <param name="cmdParams">查询参数</param>
		/// <returns></returns>
		public static DataSet QueryWithPageInfo(int pageSize, int pageIndex, string strSQL, out int totalCount, params OracleParameter[] cmdParams)
		{
			totalCount = 0;
			try
			{
				DataSet dsResult = QueryWithPageInfo(pageSize, pageIndex, strSQL, cmdParams);

				if (null != dsResult && dsResult.Tables.Count > 0)
				{
					totalCount = Convert.ToInt32(dsResult.Tables[0].Rows[dsResult.Tables[0].Rows.Count - 1]["COUNT"]);
					dsResult.Tables[0].Rows.RemoveAt(dsResult.Tables[0].Rows.Count - 1);
				}

				return dsResult;
			}
			catch { return null; }
		}

		/// <summary>
		/// 分页检索
		/// </summary>
		/// <param name="pageSize">每页记录数</param>
		/// <param name="pageIndex">当前页数</param>
		/// <param name="strSQL">查询用SQL文</param>
		/// <returns>查询结果</returns>
		public static DataSet QueryWithPageInfo(int pageSize, int pageIndex, string strSQL)
		{
			int intStart = 0;
			int intEnd = 0;
			string strFormat = string.Empty;
			string strSQLWithPageInfo = string.Empty;
			DataSet dsResult;
			DataSet dsCount;

			intStart = (pageIndex - 1) * pageSize + 1;
			intEnd = pageIndex * pageSize;

			strFormat = "SELECT * FROM ( SELECT ROWNUM ROWNO, T.* FROM ( {0} ) T ) WHERE ROWNO BETWEEN {1} AND {2}";
			strSQLWithPageInfo = string.Format(strFormat, strSQL, intStart, intEnd);
			dsResult = OracleHelper.Query(strSQLWithPageInfo);

			strFormat = "SELECT COUNT(1) COUNT FROM ( {0} )";
			strSQLWithPageInfo = string.Format(strFormat, strSQL);
			dsCount = OracleHelper.Query(strSQLWithPageInfo);

			dsResult.Merge(dsCount);

			return dsResult;
		}

		/// <summary>
		/// 分页检索
		/// </summary>
		/// <param name="pageSize">每页记录数</param>
		/// <param name="pageIndex">当前页数</param>
		/// <param name="strSQL">查询用SQL文</param>
		/// <returns>查询结果</returns>
		public static DataSet QueryWithPageInfo(int pageSize, int pageIndex, string strSQL, params OracleParameter[] cmdParams)
		{
			int intStart = 0;
			int intEnd = 0;
			string strFormat = string.Empty;
			string strSQLWithPageInfo = string.Empty;
			DataSet dsResult;
			DataSet dsCount;

			intStart = (pageIndex - 1) * pageSize + 1;
			intEnd = pageIndex * pageSize;

			strFormat = "SELECT * FROM ( SELECT ROWNUM ROWNO, T.* FROM ( {0} ) T ) WHERE ROWNO BETWEEN {1} AND {2}";
			strSQLWithPageInfo = string.Format(strFormat, strSQL, intStart, intEnd);
			dsResult = OracleHelper.Query(strSQLWithPageInfo, cmdParams);

			strFormat = "SELECT COUNT(1) COUNT FROM ( {0} )";
			strSQLWithPageInfo = string.Format(strFormat, strSQL);
			dsCount = OracleHelper.Query(strSQLWithPageInfo, cmdParams);

			dsResult.Merge(dsCount);

			return dsResult;
		}

		/// <summary>
		/// 查询
		/// gaohan20140226add
		/// 增加字符串类型，适应存储过称
		/// </summary>
		/// <param name="cmdType">字符串类型（sql语句、存储过程）</param>
		/// <param name="strSQL">查询用SQL文</param>
		/// <param name="strSQL">参数数组</param>
		/// <returns>查询结果</returns>
		public static DataSet Query(CommandType cmdType, string strSQL, params OracleParameter[] commandParameters)
		{
			string strFormat = string.Empty;
			string strSQLWithPageInfo = string.Empty;
			DataSet dsResult;

			dsResult = OracleHelper.Query(cmdType, strSQL, commandParameters);

			return dsResult;
		}
		/// <summary>
		/// 非分页检索
		/// </summary>
		/// <param name="strSQL"></param>
		/// <returns></returns>
		public static DataSet QueryWithNoPageInfo(string strSQL)
		{
			string strFormat = string.Empty;

			DataSet dsResult;
			DataSet dsCount;

			dsResult = OracleHelper.Query(strSQL);

			strFormat = string.Format("select count(1) COUNT from ({0})", strSQL);
			dsCount = OracleHelper.Query(strFormat);

			dsResult.Merge(dsCount);

			return dsResult;
		}

		/// <summary>
		/// DataSet转换为List
		/// </summary>
		/// <typeparam name="T">模板</typeparam>
		/// <param name="dataSet">待转换数据</param>
		/// <returns>转换后数据</returns>
		public static List<T> ConvertToList<T>(DataSet dataSet) where T : class
		{
			List<T> buffer = new List<T>();

			// 遍历所有行
			for (int i = 0; i < dataSet.Tables[0].Rows.Count; i++)
			{
				DataRow row = dataSet.Tables[0].Rows[i];

				T t = Activator.CreateInstance<T>();

				// 遍历所属性
				foreach (PropertyInfo property in typeof(T).GetProperties())
				{
					// 该属性包含在集合中
					if (dataSet.Tables[0].Columns.Contains(property.Name))
					{
						// 该属性值不是空
						if (row[property.Name] != DBNull.Value && row[property.Name] != null)
						{
							// 整型
							if (property.PropertyType.Equals(typeof(int)) || property.PropertyType.Equals(typeof(int?)))
							{
								property.SetValue(t, Convert.ToInt32(row[property.Name]), null);
							}
							else if (property.PropertyType.Equals(typeof(decimal)) || property.PropertyType.Equals(typeof(decimal?)))
							{
								property.SetValue(t, Convert.ToDecimal(row[property.Name]), null);
							}
							else if (property.PropertyType.Equals(typeof(double)) || property.PropertyType.Equals(typeof(double?)))
							{
								property.SetValue(t, Convert.ToDouble(row[property.Name]), null);
							}
							else if (property.PropertyType.Equals(typeof(DateTime)) || property.PropertyType.Equals(typeof(DateTime?)))
							{
								property.SetValue(t, Convert.ToDateTime(row[property.Name]), null);
							}
							else if (property.PropertyType.Equals(typeof(string)))
							{
								property.SetValue(t, row[property.Name].ToString(), null);
							}
							else if (property.PropertyType.Equals(typeof(byte[])))
							{
								property.SetValue(t, (byte[])row[property.Name], null);
							}
							else
							{
								property.SetValue(t, row[property.Name], null);
							}
						}
					}
				}
				buffer.Add(t);
			}

			return buffer;
		}

		/// <summary>
		/// DataTable转换为List
		/// </summary>
		/// <typeparam name="T">模板</typeparam>
		/// <param name="dataSet">待转换数据</param>
		/// <returns>转换后数据</returns>
		public static List<T> ConvertDataTableToList<T>(DataTable dataTable)
		{
			List<T> buffer = new List<T>();

			// 遍历所有行
			for (int i = 0; i < dataTable.Rows.Count; i++)
			{
				DataRow row = dataTable.Rows[i];

				T t = Activator.CreateInstance<T>();

				// 遍历所属性
				foreach (PropertyInfo property in typeof(T).GetProperties())
				{
					// 该属性包含在集合中
					if (dataTable.Columns.Contains(property.Name))
					{
						// 该属性值不是空
						if (row[property.Name] != DBNull.Value && row[property.Name] != null)
						{
							// 整型
							if (property.PropertyType.Equals(typeof(int)))
							{
								property.SetValue(t, Convert.ToInt32(row[property.Name]), null);
							}
							else if (property.PropertyType.Equals(typeof(decimal)))
							{
								property.SetValue(t, Convert.ToDecimal(row[property.Name]), null);
							}
							else if (property.PropertyType.Equals(typeof(double)))
							{
								property.SetValue(t, Convert.ToDouble(row[property.Name]), null);
							}
							else if (property.PropertyType.Equals(typeof(DateTime)))
							{
								property.SetValue(t, Convert.ToDateTime(row[property.Name]), null);
							}
							else if (property.PropertyType.Equals(typeof(string)))
							{
								property.SetValue(t, row[property.Name].ToString(), null);
							}
							else if (property.PropertyType.Equals(typeof(byte[])))
							{
								property.SetValue(t, (byte[])row[property.Name], null);
							}
							else
							{
								property.SetValue(t, row[property.Name], null);
							}
						}
					}
				}
				buffer.Add(t);
			}

			return buffer;
		}

		//查询数据返回List
		/// <summary>
		/// 查询数据返回List
		/// </summary>
		/// <typeparam name="T">实体类型</typeparam>
		/// <param name="strSQL">查询SQL</param>
		/// <returns></returns>
		public static List<T> Query<T>(string strSQL) where T:class
		{
			List<T> result = new List<T>();
			try
			{
				DataSet dsResult = QueryWithNoPageInfo(strSQL);

				if (null != dsResult)
				{
					dsResult.Tables[0].Rows.RemoveAt(dsResult.Tables[0].Rows.Count - 1);

					result = ConvertToList<T>(dsResult);
				}

				return result;
			}
			catch
			{
				return null;
			}
		}
	}
}
