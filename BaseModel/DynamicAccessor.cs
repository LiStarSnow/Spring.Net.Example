using BaseModel.SqlAttribute;
using Cis.DbLight.TableMetadata;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Threading.Tasks;

namespace BaseModel
{
    /// <summary>
    /// Model动态对象
    /// 动态编译相关功能
    /// </summary>
    /// <typeparam name="TResult"></typeparam>
    public class DynamicAccessor<TResult>
    {
        private Action<object, string, object> _setValueDelegate;
        private Func<object, string, object> _getValueDelegate;
        private Func<IDataRecord, IList<string>, TResult> _getModelByIDataRecordDelegate;
        private Func<DataRow, IList<string>, TResult> _getModelByDataRowDelegate;
        private Func<IDictionary<string, object>, TResult> _getModelByIDictionaryDelegate;
        private Type _type;

        /// <summary>
        /// 实例化
        /// </summary>
        public DynamicAccessor()
        {
            _type = typeof(TResult);
            ProMap = GetProMap();
            //ColumnProMap = GetColumnProMap();

            _getValueDelegate = GenerateGetValue();
            _setValueDelegate = GenerateSetValue();
            _getModelByDataRowDelegate = GenerateGetModelByDataRow();
            _getModelByIDataRecordDelegate = GenerateGetModelByIDataRecord();
            _getModelByIDictionaryDelegate = GenerateGetModelByIDictionary();
        }

        /// <summary>
        /// 属性-名称 映射列表
        /// </summary>
        public Dictionary<string, string> ProMap { get; set; }

        /// <summary>
        /// 属性-Column 映射列表
        /// </summary>
        //public Dictionary<string, ColumnAttribute> ColumnProMap { get; set; }

        /// <summary>
        /// 获取指定对象的指定属性的值
        /// </summary>
        /// <param name="instance">要获取属性值的对象</param>
        /// <param name="memberName">要获取的属性的名称</param>
        /// <returns></returns>
        public object GetValue(object instance, string memberName)
        {
            return _getValueDelegate(instance, ProMap[memberName.ToLower()]);
        }

        /// <summary>
        /// 为指定对象的指定属性设置值。
        /// </summary>
        /// <param name="instance">要设置属性的对象</param>
        /// <param name="memberName">要设置的属性名</param>
        /// <param name="newValue">要设置的值</param>
        public void SetValue(object instance, string memberName, object newValue)
        {
            _setValueDelegate(instance, ProMap[memberName.ToLower()], newValue == DBNull.Value ? null : newValue);
        }

        /// <summary>
        /// 根据IDataRecord GetModel
        /// </summary>
        /// <param name="dr"></param>
        /// <returns></returns>
        public TResult GetModel(IDataRecord dr, IList<string> columns)
        {
            return _getModelByIDataRecordDelegate(dr, columns);
        }

        /// <summary>
        /// 根据DataRow  GetModel
        /// </summary>
        /// <param name="dr"></param>
        /// <returns></returns>
        public TResult GetModel(DataRow dr, IList<string> columns)
        {
            return _getModelByDataRowDelegate(dr, columns);
        }

        /// <summary>
        /// IDictionary GetModel
        /// </summary>
        /// <param name="dic"></param>
        /// <returns></returns>
        public TResult GetModel(IDictionary<string, object> dic)
        {
            return _getModelByIDictionaryDelegate(dic);
        }

        /// <summary>
        /// 设置属性小写对应列表 用于做映射
        /// 属性名和数据列名不一定要一致  列名保证全部大写 否则会影响性能
        /// </summary>
        /// <returns></returns>
        private Dictionary<string, string> GetProMap()
        {
            Dictionary<string, string> rel = new Dictionary<string, string>();
            foreach (var propertyInfo in _type.GetProperties())
            {
                //rel.Add(propertyInfo.Name.ToLower(), propertyInfo.Name);
                //rel.Add(propertyInfo.Name.ToLower(), GetDbColumnName(propertyInfo));
                var sa = propertyInfo.GetCustomAttributes(true).OfType<SqlSelectAttribute>().FirstOrDefault();
                if (sa != null)
                {
                    rel.Add(propertyInfo.Name.ToLower(), sa.ColName);
                }
                else
                {
                    var sa2 = propertyInfo.GetCustomAttributes(true).OfType<ColumnAttribute>().FirstOrDefault();
                    if (sa2 != null)
                    {
                        rel.Add(propertyInfo.Name.ToLower(), sa2.ColumnName);
                    }
                    else
                    {
                        rel.Add(propertyInfo.Name.ToLower(), propertyInfo.Name);
                    }
                }
            }

            return rel;
        }

        /// <summary>
        /// 设置属性小写对应列表 用于与Column做映射
        /// 属性名和数据列名不一定要一致  列名保证全部大写 否则会影响性能
        /// </summary>
        /// <returns></returns>
        //private Dictionary<string, ColumnAttribute> GetColumnProMap()
        //{
        //    //Dictionary<string, ColumnAttribute> dict = new Dictionary<string, ColumnAttribute>();
        //    //foreach (var propertyInfo in _type.GetProperties())
        //    //{
        //    //    var attribute = propertyInfo.GetCustomAttributes(true).OfType<ColumnAttribute>().FirstOrDefault();
        //    //    if (attribute != null)
        //    //    {
        //    //        dict.Add(propertyInfo.Name.ToLower(), attribute);
        //    //    }
        //    //    else
        //    //    {
        //    //        dict.Add(propertyInfo.Name.ToLower(), null);
        //    //    }
        //    //}

        //    return dict;
        //}

        /// <summary>
        /// 构建读取值函数
        /// </summary>
        /// <returns></returns>
        private Func<object, string, object> GenerateGetValue()
        {
            var instance = Expression.Parameter(typeof(object), "instance");
            var memberName = Expression.Parameter(typeof(string), "memberName");
            var nameHash = Expression.Variable(typeof(int), "nameHash");
            var calHash = Expression.Assign(nameHash, Expression.Call(memberName, typeof(object).GetMethod("GetHashCode")));
            var cases = new List<SwitchCase>();
            foreach (var propertyInfo in _type.GetProperties())
            {
                if (propertyInfo.CanRead)
                {
                    var property = Expression.Property(Expression.Convert(instance, _type), propertyInfo.Name);
                    var propertyHash = Expression.Constant(propertyInfo.Name.GetHashCode(), typeof(int));

                    cases.Add(Expression.SwitchCase(Expression.Convert(property, typeof(object)), propertyHash));
                }
            }
            var switchEx = Expression.Switch(nameHash, Expression.Constant(null), cases.ToArray());
            var methodBody = Expression.Block(typeof(object), new[] { nameHash }, calHash, switchEx);

            return Expression.Lambda<Func<object, string, object>>(methodBody, instance, memberName).Compile();
        }

        /// <summary>
        /// 构建设置属性值函数
        /// </summary>
        /// <returns></returns/
        private Action<object, string, object> GenerateSetValue()
        {
            var instance = Expression.Parameter(typeof(object), "instance");
            var memberName = Expression.Parameter(typeof(string), "memberName");
            var newValue = Expression.Parameter(typeof(object), "newValue");
            var nameHash = Expression.Variable(typeof(int), "nameHash");
            var calHash = Expression.Assign(nameHash, Expression.Call(memberName, typeof(object).GetMethod("GetHashCode")));
            var cases = new List<SwitchCase>();
            foreach (var propertyInfo in _type.GetProperties())
            {
                // 属性必须可写
                //if (!(propertyInfo.CanWrite && IsSimpleType(propertyInfo.PropertyType)))
                if (!propertyInfo.CanWrite)
                {
                    continue;
                }
                var property = Expression.Property(Expression.Convert(instance, _type), propertyInfo.Name);
                var setValue = Expression.Assign(property, Expression.Convert(newValue, propertyInfo.PropertyType));
                var propertyHash = Expression.Constant(propertyInfo.Name.GetHashCode(), typeof(int));

                cases.Add(Expression.SwitchCase(Expression.Convert(setValue, typeof(object)), propertyHash));
            }
            var switchEx = Expression.Switch(nameHash, Expression.Constant(null), cases.ToArray());
            var methodBody = Expression.Block(new[] { nameHash }, calHash, switchEx);

            return Expression.Lambda<Action<object, string, object>>(methodBody, instance, memberName, newValue).Compile();
        }

        /// <summary>
        /// 获得属性的赋值表达式
        /// </summary>
        /// <param name="instance"></param>
        /// <param name="pro"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        private Expression GetSetValueExpression(Expression instance, PropertyInfo pro, Expression getValue)
        {
            bool isNullable = IsNullableType(pro.PropertyType);
            string convertFn = GetConvertFn(pro.PropertyType);

            var tempGetValue = getValue;
            if (pro.PropertyType.IsEnum)
            {
                //枚举特殊处理
                tempGetValue = Expression.Call
                (
                    typeof(Convert).GetMethod("ChangeType", new Type[] { typeof(object), typeof(Type) }),
                    getValue,
                    Expression.Constant(pro.PropertyType.GetEnumUnderlyingType())
                );
            }

            Expression convertValue;
            if (string.IsNullOrEmpty(convertFn))
            {
                convertValue = Expression.Convert(tempGetValue, pro.PropertyType);
            }
            else
            {
                //强制转换
                Type convertsionType = null;
                if (isNullable)
                {
                    convertsionType = new NullableConverter(pro.PropertyType).UnderlyingType;
                    convertValue = Expression.Convert
                    (
                        Expression.Call
                        (
                            typeof(Convert).GetMethod("ChangeType", new Type[] { typeof(object), typeof(Type) }),
                            tempGetValue,
                            Expression.Constant(convertsionType)
                        ),
                        pro.PropertyType
                    );
                }
                else
                {
                    convertsionType = pro.PropertyType;
                    convertValue = Expression.Call
                    (
                        typeof(Convert).GetMethod(convertFn, new Type[] { typeof(object) }),
                        tempGetValue
                    );
                }
            }

            Expression nullAbleExpression;
            if (isNullable)
            {
                nullAbleExpression = Expression.Assign(Expression.Property(instance, pro.Name), Expression.Convert(Expression.Constant(null), pro.PropertyType));
            }
            else
            {
#if DEBUG
                nullAbleExpression = Expression.Assign(Expression.Property(instance, pro.Name), Expression.Convert(Expression.Constant(null), pro.PropertyType));
#else
                nullAbleExpression = Expression.Empty();
#endif
            }

            Expression setValue = Expression.IfThenElse
            (
                Expression.Equal(getValue, Expression.Constant(DBNull.Value)),
                nullAbleExpression,
                Expression.Assign(Expression.Property(instance, pro.Name), convertValue)
            );

            return setValue;
        }

        /// <summary>
        /// DataRow转model 忽略大小写 保持效率
        /// </summary>
        /// <returns></returns>
        private Func<DataRow, IList<string>, TResult> GenerateGetModelByDataRow()
        {
            var variable = new List<ParameterExpression>();
            var expres = new List<Expression>();

            var read = Expression.Parameter(typeof(DataRow), "read");
            var columnNames = Expression.Parameter(typeof(IList<string>), "columnNames");

            var instance = Expression.Variable(_type, "instance");
            var index = Expression.Variable(typeof(int), "index");
            variable.Add(instance);
            variable.Add(index);

            expres.Add(Expression.Assign(instance, Expression.New(_type)));

            PropertyInfo indexerInfo = typeof(DataRow).GetProperty("Item", new[] { typeof(int) });
            foreach (var propertyInfo in _type.GetProperties())
            {
                if (!(propertyInfo.CanWrite && IsSimpleType(propertyInfo.PropertyType)))
                {
                    continue;
                }
                //数据库返回列全部大小 忽略大小写赋值
                string colName = ProMap[propertyInfo.Name.ToLower()].ToUpper();
                var callIndex = Expression.Call(columnNames, typeof(IList<string>).GetMethod("IndexOf"), Expression.Constant(colName));
                var assIndex = Expression.Assign(index, callIndex);
                expres.Add(assIndex);

                var getItem = Expression.MakeIndex
                (
                    read,
                    indexerInfo,
                    new[] { index }
                );

                var setValue = GetSetValueExpression(instance, propertyInfo, getItem);

                var ifAssign = Expression.IfThen
                (
                    Expression.NotEqual(index, Expression.Constant(-1)),
                    setValue
                );
                expres.Add(ifAssign);
            }

            LabelTarget returnTarget = Expression.Label(_type);
            expres.Add(Expression.Return(returnTarget, instance, _type));
            expres.Add(Expression.Label(returnTarget, instance));
            var methodBody = Expression.Block(_type, variable, expres);

            return Expression.Lambda<Func<DataRow, IList<string>, TResult>>(methodBody, read, columnNames).Compile();
        }

        /// <summary>
        /// IDataRecord转model 忽略大小写 保持效率
        /// </summary>
        /// <returns></returns>
        private Func<IDataRecord, IList<string>, TResult> GenerateGetModelByIDataRecord()
        {
            //内部快变量
            var variable = new List<ParameterExpression>();
            //表达式
            var expres = new List<Expression>();

            var dr = Expression.Parameter(typeof(IDataRecord), "dr");
            var columnNames = Expression.Parameter(typeof(IList<string>), "columnNames");

            var instance = Expression.Variable(_type, "instance");
            var index = Expression.Variable(typeof(int), "index");
            variable.Add(instance);
            variable.Add(index);

            //使用默认构造函数创建一个对象 var model = new Model();
            expres.Add(Expression.Assign(instance, Expression.New(_type)));

            foreach (var propertyInfo in _type.GetProperties())
            {
                if (!(propertyInfo.CanWrite && IsSimpleType(propertyInfo.PropertyType)))
                {
                    continue;
                }
                //数据库返回列全部大小 忽略大小写赋值
                string colName = ProMap[propertyInfo.Name.ToLower()].ToUpper();

                //给临时变量index赋值
                var callIndex = Expression.Call(columnNames, typeof(IList<string>).GetMethod("IndexOf"), Expression.Constant(colName));
                var assIndex = Expression.Assign(index, callIndex);
                expres.Add(assIndex);

                var getItem = Expression.Call(dr, typeof(IDataRecord).GetMethod("GetValue"), index);
                var setValue = GetSetValueExpression(instance, propertyInfo, getItem);

                var blok = Expression.IfThen
                (
                    Expression.NotEqual(index, Expression.Constant(-1)),
                    setValue
                );

                expres.Add(blok);
            }

            LabelTarget returnTarget = Expression.Label(_type);
            expres.Add(Expression.Return(returnTarget, instance, _type));
            expres.Add(Expression.Label(returnTarget, instance));
            var methodBody = Expression.Block(_type, variable, expres);

            return Expression.Lambda<Func<IDataRecord, IList<string>, TResult>>(methodBody, dr, columnNames).Compile();
        }

        /// <summary>
        /// IDataRecord转model 忽略大小写 保持效率
        /// </summary>
        /// <returns></returns>
        private Func<IDictionary<string, object>, TResult> GenerateGetModelByIDictionary()
        {
            //内部快变量
            var variable = new List<ParameterExpression>();
            //表达式
            var expres = new List<Expression>();

            var dic = Expression.Parameter(typeof(IDictionary<string, object>), "dic");
            var key = Expression.Variable(typeof(string), "key");

            var instance = Expression.Variable(_type, "instance");
            variable.Add(instance);
            variable.Add(key);

            //使用默认构造函数创建一个对象 var model = new Model();
            expres.Add(Expression.Assign(instance, Expression.New(_type)));

            PropertyInfo indexerInfo = typeof(IDictionary<string, object>).GetProperty("Item", new[] { typeof(string) });
            foreach (var propertyInfo in _type.GetProperties())
            {
                if (!(propertyInfo.CanWrite && IsSimpleType(propertyInfo.PropertyType)))
                {
                    continue;
                }
                //数据库返回列全部大小 忽略大小写赋值
                string colName = ProMap[propertyInfo.Name.ToLower()];
                var assIndex = Expression.Assign(key, Expression.Constant(colName));
                expres.Add(assIndex);

                var getItem = Expression.MakeIndex
                (
                    dic,
                    indexerInfo,
                    new[] { key }
                );
                var containsKey = Expression.Call(dic, typeof(IDictionary<string, object>).GetMethod("ContainsKey"), Expression.Constant(colName));

                Expression setValue = Expression.IfThen
                (
                    containsKey,
                    Expression.Assign(Expression.Property(instance, propertyInfo.Name), Expression.Convert(getItem, propertyInfo.PropertyType))
                );

                expres.Add(setValue);
            }

            LabelTarget returnTarget = Expression.Label(_type);
            expres.Add(Expression.Return(returnTarget, instance, _type));
            expres.Add(Expression.Label(returnTarget, instance));
            var methodBody = Expression.Block(_type, variable, expres);

            return Expression.Lambda<Func<IDictionary<string, object>, TResult>>(methodBody, dic).Compile();
        }

        /// <summary>
        /// 类型是否支持转换
        /// </summary>
        /// <param name="tempType"></param>
        /// <returns></returns>
        static bool IsSimpleType(Type tempType)
        {
            return (
                        tempType.Equals(typeof(Int16))
                        || tempType.Equals(typeof(int))
                        || tempType.Equals(typeof(Int64))
                        || tempType.Equals(typeof(string))
                        || tempType.Equals(typeof(decimal))
                        || tempType.Equals(typeof(double))
                        || tempType.Equals(typeof(DateTime))
                        || tempType.Equals(typeof(float))
                        || tempType.Equals(typeof(DateTimeOffset))
                        || tempType.Equals(typeof(char))
                        || tempType.Equals(typeof(long))
                        || tempType.Equals(typeof(bool))
                        || tempType.Equals(typeof(Int16?))
                        || tempType.Equals(typeof(int?))
                        || tempType.Equals(typeof(Int64?))
                        || tempType.Equals(typeof(decimal?))
                        || tempType.Equals(typeof(double?))
                        || tempType.Equals(typeof(DateTime?))
                        || tempType.Equals(typeof(float?))
                        || tempType.Equals(typeof(DateTimeOffset?))
                        || tempType.Equals(typeof(char?))
                        || tempType.Equals(typeof(long?))
                        || tempType.Equals(typeof(bool?))
                        || tempType.IsEnum
                   //|| IsTopModelType(tempType)
                   );
        }

        /// <summary>
        /// 判断类型是否为可空类型
        /// </summary>
        /// <param name="theType"></param>
        /// <returns></returns>
        static bool IsNullableType(Type theType)
        {
            return (theType.IsGenericType && theType.
              GetGenericTypeDefinition().Equals
              (typeof(Nullable<>)));
        }

        /// <summary>
        /// 需要强转的类型
        /// </summary>
        /// <param name="theType"></param>
        /// <returns></returns>
        static string GetConvertFn(Type theType)
        {
            string convertTo = null;

            string typeFullName = theType.FullName;
            if (IsNullableType(theType))
            {
                typeFullName = theType.GenericTypeArguments[0].FullName;
            }
            switch (typeFullName)
            {
                case "System.Int16":
                    convertTo = "ToInt16";
                    break;
                case "System.Int32":
                    convertTo = "ToInt32";
                    break;
                case "System.Int64":
                    convertTo = "ToInt64";
                    break;
                case "System.Boolean":
                    convertTo = "ToBoolean";
                    break;
                case "System.String":
                    convertTo = "ToString";
                    break;
                default:
                    break;
            }
            return convertTo;
        }
    }
}
