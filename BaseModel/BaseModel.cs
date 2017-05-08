using BaseModel.Enum;
using BaseModel.SqlAttribute;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace BaseModel
{
    public abstract class BaseModel<TResult> : IBaseModel
    {
        /// <summary>
        /// 动态对象访问器
        /// </summary>
        public static readonly DynamicAccessor<TResult> DynamicAccessor = new DynamicAccessor<TResult>();

        /// <summary>
        /// select查询列
        /// </summary>
        public static string DbSelect = GetSelectStr();
        private static string GetSelectStr()
        {
            StringBuilder sb = new StringBuilder();
            Dictionary<string, string> rel = new Dictionary<string, string>();
            foreach (var propertyInfo in typeof(TResult).GetProperties())
            {
                var ca = propertyInfo.GetCustomAttributes(true).OfType<SqlSelectAttribute>().FirstOrDefault();
                if (ca != null)
                {
                    var tableAlias = string.IsNullOrEmpty(ca.TableAlias) ? "" : (ca.TableAlias + ".");
                    var colName = (ca.ColName ?? propertyInfo.Name);
                    if (string.IsNullOrEmpty(ca.Format))
                    {
                        sb.Append(" " + tableAlias + colName + ",");
                    }
                    else
                    {
                        sb.AppendFormat(" " + tableAlias + ca.Format + ",", colName);
                    }
                }
            }
            if (sb.Length > 0)
            {
                sb.Remove(sb.Length - 1, 1);
            }
            sb.Append(" ");
            return sb.ToString();
        }

        /// <summary>
        /// sql查询信息
        /// </summary>
        [Newtonsoft.Json.JsonIgnore]
        public DbInfo DbInfo { get; set; }

        /// <summary>
        /// 设置查询sql参数
        /// </summary>
        public void SetDbInfo()
        {
            DbInfo = new DbInfo();

            var appendList = new List<PropertyInfo>();

            var allList = typeof(TResult).GetProperties().Where(m =>
            {
                var whereAttr = m.GetCustomAttributes(true).OfType<SqlWhereAttribute>().FirstOrDefault();
                object value = this.GetValue(m.Name);
                return whereAttr != null && IsAppend(whereAttr, value);
            });
            var sigleList = allList.Where(m =>
            {
                var whereAttr = m.GetCustomAttributes(true).OfType<SqlWhereAttribute>().FirstOrDefault();
                return whereAttr.IsSingle;
            });
            if (sigleList.Count() > 0)
            {
                var requireList = allList.Where(m =>
                {
                    var whereAttr = m.GetCustomAttributes(true).OfType<SqlWhereAttribute>().FirstOrDefault();
                    return whereAttr.IsRequire;
                });
                appendList = sigleList.Union(requireList).ToList();
            }
            else
            {
                SetSql();
                appendList = allList.ToList();
            }

            foreach (var propertyInfo in appendList)
            {
                object value = this.GetValue(propertyInfo.Name);
                var whereAttr = propertyInfo.GetCustomAttributes(true).OfType<SqlWhereAttribute>().FirstOrDefault();
                AppendWhere(whereAttr, propertyInfo, value);

                AppendJoin(propertyInfo, (whereAttr.TableAlias ?? "") + propertyInfo.Name);
            }
            SetRequireSql();
        }

        /// <summary>
        /// 判断where条件是否需要添加
        /// </summary>
        /// <param name="whereAttr"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        private bool IsAppend(SqlWhereAttribute whereAttr, object value)
        {
            var formula = whereAttr.Formula;
            var isAppend = false;
            switch (whereAttr.ConditionType)
            {
                case ConditionType.IsNullOrEmpty:
                    if (value != null)
                    {
                        if (value is string && value.ToString().Trim() != "")
                        {
                            if (formula == "in" || formula == "not in")
                            {
                                string[] list = value.ToString().Split(new char[] { whereAttr.InSplit }, StringSplitOptions.RemoveEmptyEntries);
                                if (list.Length > 0)
                                {
                                    isAppend = true;
                                }
                            }
                            else
                            {
                                isAppend = true;
                            }
                        }
                        else if (value is IEnumerable<object>)
                        {
                            var tempList = new List<object>();
                            foreach (var item in (value as IEnumerable<object>))
                            {
                                string tempStr = (item ?? "").ToString();
                                if (!string.IsNullOrEmpty(tempStr) && tempStr != (whereAttr.Values ?? "").ToString())
                                {
                                    tempList.Add(item);
                                }
                            }
                            var count = tempList.Count;
                            if (count > 0)
                            {
                                isAppend = true;
                            }
                        }
                        else if (value is int || value is long || value is decimal || value is System.Enum)
                        {
                            if (Convert.ToDecimal(value) != 0)
                            {
                                isAppend = true;
                            }
                        }
                        else if (value is DateTime)
                        {
                            if ((Convert.ToDateTime(value)) != DateTime.MinValue)
                            {
                                isAppend = true;
                            }
                        }
                    }
                    break;
                default:
                    break;
            }
            return isAppend;
        }

        /// <summary>
        /// 添加where条件
        /// </summary>
        /// <param name="whereAttr"></param>
        /// <param name="propertyInfo"></param>
        /// <param name="value"></param>
        private void AppendWhere(SqlWhereAttribute whereAttr, PropertyInfo propertyInfo, object value)
        {
            var tableAlias = string.IsNullOrEmpty(whereAttr.TableAlias) ? "" : (whereAttr.TableAlias + ".");
            var colName = tableAlias + (whereAttr.ColName ?? propertyInfo.Name);
            var paramName = (whereAttr.TableAlias ?? "") + propertyInfo.Name;
            var formula = whereAttr.Formula.ToLower().Trim();

            object newValue = null;
            if (value is string && (formula == "in" || formula == "not in"))
            {
                string[] list = value.ToString().Split(new char[] { whereAttr.InSplit }, StringSplitOptions.RemoveEmptyEntries);
                if (list.Length == 1)
                {
                    formula = formula == "in" ? "=" : "<>";
                    value = list.First();
                }
                else
                {
                    value = list;
                }
            }
            else if (value is IEnumerable<object>)
            {
                var tempList = new List<object>();
                foreach (var item in (value as IEnumerable<object>))
                {
                    string tempStr = (item ?? "").ToString();
                    if (!string.IsNullOrEmpty(tempStr) && tempStr != (whereAttr.Values ?? "").ToString())
                    {
                        tempList.Add(item);
                    }
                }
                if (tempList.Count == 1)
                {
                    formula = "=";
                    value = tempList.First();
                }
                else
                {
                    value = tempList;
                }
            }
            newValue = value;

            switch (formula)
            {
                case "in":
                case "not in":
                    if (string.IsNullOrEmpty(whereAttr.TmpTableName))
                    {
                        DbInfo.WhereJoin.Add(string.Format(
                            " inner join (select column_value from table(PKG_Tools_Str.fun_Str_Split(:{0},'{1}'))) {0}_ on {0}_.column_value {2} {3} ",
                            paramName,
                            whereAttr.InSplit,
                            formula == "in" ? "=" : "<>",
                            colName));
                        if (value is IEnumerable)
                        {
                            newValue = String.Join(whereAttr.InSplit.ToString(), (value as IEnumerable<object>).ToArray());
                        }
                    }
                    else
                    {
                        if (string.IsNullOrEmpty(whereAttr.TmpTableColName))
                        {
                            throw new Exception("where 使用临时表方案必须指定临时表列名");
                        }
                        DbInfo.Where.Add(string.Format(
                            "{0}(select 1 from Global_Temp_Search where {1}={2})",
                            formula == "in" ? "exists" : "not exists",
                            whereAttr.TmpTableColName,
                            colName));
                        DbInfo.TmpTables.Add(new TmpTable()
                        {
                            TableName = whereAttr.TmpTableName,
                            TableColName = whereAttr.TmpTableColName,
                            Values = value as IEnumerable<object>
                        });
                        newValue = null;
                    }
                    break;
                default:
                    if (string.IsNullOrEmpty(whereAttr.ColName))
                    {
                        break;
                    }
                    if (value is DateTime && !string.IsNullOrEmpty(whereAttr.DateTimeFormat))
                    {
                        var dateStr = ((DateTime)value).ToString(whereAttr.DateTimeFormat);
                        DbInfo.Where.Add(String.Format("{0} {1} '{2}'", colName, formula, dateStr));
                        newValue = null;
                    }
                    else if (value is string && !string.IsNullOrEmpty(whereAttr.DateTimeFormat))
                    {
                        var dateStr = DateTime.ParseExact(
                                value.ToString(),
                                whereAttr.DateTimeFormat,
                                System.Globalization.CultureInfo.CurrentCulture
                            ).ToString(whereAttr.DateTimeFormat);
                        DbInfo.Where.Add(String.Format("{0} {1} '{2}'", colName, formula, dateStr));
                        newValue = null;
                    }
                    else
                    {
                        DbInfo.Where.Add(String.Format("{0} {1} :{2}", colName, formula, paramName));
                    }
                    break;
            }
            if (newValue != null)
            {
                DbInfo.Params.Add(paramName, newValue);
            }
        }

        /// <summary>
        /// 添加join
        /// </summary>
        /// <param name="propertyInfo"></param>
        /// <param name="paramName"></param>
        private void AppendJoin(PropertyInfo propertyInfo, string paramName)
        {
            var joinAttr = propertyInfo.GetCustomAttributes(true).OfType<SqlJoinAttribute>().FirstOrDefault();

            if (joinAttr != null && !string.IsNullOrEmpty(joinAttr.JoinString))
            {
                this.DbInfo.FromJoin.Add(string.Format(joinAttr.JoinString, paramName));
            }
        }

        /// <summary>
        /// 特殊条件添加
        /// </summary>
        public virtual void SetSql() { }

        /// <summary>
        /// 必须添加的特殊条件
        /// </summary>
        public virtual void SetRequireSql() { }

        /// <summary>
        /// 根据属性名称获取值 动态编译方法
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public object GetValue(string name)
        {
            return DynamicAccessor.GetValue(this, name);
        }

        /// <summary>
        /// 根据属性名称设置值 动态编译方法
        /// </summary>
        /// <param name="name"></param>
        /// <param name="value"></param>
        public void SetValue(string name, object value)
        {
            DynamicAccessor.SetValue(this, name, value);
        }
    }

    /// <summary>
    /// 临时表信息
    /// </summary>
    public class TmpTable
    {
        public string TableName { get; set; }

        public string TableColName { get; set; }

        public IEnumerable<object> Values { get; set; }
    }

    /// <summary>
    /// sql查询信息
    /// </summary>
    public class DbInfo
    {
        public List<string> Where { get; set; } = new List<string>();

        public List<string> FromJoin { get; set; } = new List<string>();

        public List<string> WhereJoin { get; set; } = new List<string>();

        public string WhereStr
        {
            get
            {
                return string.Join(" and ", Where);
            }
        }

        public string JoinStr
        {
            get
            {
                return string.Join(" ", FromJoin) + " " + string.Join(" ", WhereJoin);
            }
        }

        public IDictionary<string, object> Params { get; set; } = new Dictionary<string, object>();

        public List<TmpTable> TmpTables { get; set; } = new List<TmpTable>();
    }
}
