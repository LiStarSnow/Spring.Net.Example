namespace BaseModel.Enum
{
    public enum SqlGetType
    {
        /// <summary>
        /// 用运算符连接
        /// </summary>
        FromFormula = 0,
        /// <summary>
        /// sql来自Dictionary 用 or 连接
        /// </summary>
        FromDictionaryForOr = 1,
        /// <summary>
        /// sql来自Dictionary 用 and 连接
        /// </summary>
        FromDictionaryForAnd = 2
    }
}
