using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spring.Net.Example.Dao.Common
{
    public sealed class BatchOracleHelper
    {
        public static OracleParameter[] GetOracleParameters(Dictionary<string, object[]> columnRowData)
        {
            IList<OracleParameter> dbParams = new List<OracleParameter>();
            foreach (var pair in columnRowData)
            {
                dbParams.Add(GetOracleParameter(pair));
            }

            return dbParams.ToArray();
        }

        public static OracleParameter GetOracleParameter(KeyValuePair<string, object[]> pair)
        {
            var dbType = GetOracleDbType(pair.Value);
            OracleParameter parameter = new OracleParameter(string.Format("{0}", pair.Key), dbType);

            parameter.Direction = ParameterDirection.Input;
            parameter.OracleDbTypeEx = dbType;

            parameter.Value = GetBatchParamValues(pair.Value);

            return parameter;
        }

        /// <summary>
        /// 根据数据类型获取OracleDbType
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        private static OracleDbType GetOracleDbType(object value)
        {
            OracleDbType dataType = OracleDbType.Varchar2;
            Type type = value.GetType();
            if (type.IsEnum)
            {
                return OracleDbType.Char;
            }

            if (value is string)
            {
                dataType = OracleDbType.Varchar2;
            }
            else if (value is DateTime)
            {
                dataType = OracleDbType.TimeStamp;
            }
            else if (value is int || value is short)
            {
                dataType = OracleDbType.Int32;
            }
            else if (value is long)
            {
                dataType = OracleDbType.Int64;
            }
            else if (value is decimal || value is double || value is float)
            {
                dataType = OracleDbType.Decimal;
            }
            else if (value is Guid)
            {
                dataType = OracleDbType.Varchar2;
            }
            else if (value is bool)
            {
                dataType = OracleDbType.Byte;
            }
            else if (value is byte || value is byte[])
            {
                dataType = OracleDbType.Clob;
            }
            else if (value is char)
            {
                dataType = OracleDbType.Char;
            }

            return dataType;
        }

        private static OracleDbType GetOracleDbType(object[] values)
        {
            object value = new object();
            foreach (var o in values)
            {
                if (o != null)
                {
                    value = o;
                    break;
                }
            }
            return GetOracleDbType(value);
        }

        private static object[] GetBatchParamValues(object[] arry)
        {
            object value = new object();
            foreach (var o in arry)
            {
                if (o != null)
                {
                    value = o;
                    break;
                }
            }
            var type = value.GetType();

            if (type.IsEnum)
            {
                object[] result = new object[arry.Length];
                for (int i = 0; i < arry.Length; i++)
                {
                    // result[i] = (char)(int)arry[i];
                    result[i] = Convert.ToString((int)arry[i], 16);
                }
                return result;
            }
            else if (type == typeof(bool))
            {
                object[] result = new object[arry.Length];
                for (int i = 0; i < arry.Length; i++)
                {
                    bool v = (bool)arry[i];
                    if (v)
                        result[i] = "1";
                    else
                        result[i] = "0";
                }
                return result;
            }
            return arry;
        }

    }
}
