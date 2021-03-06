﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spring.Net.Example.IBLL
{
    public interface IService<T>
    {
        /// <summary>  
        /// 添加实体  
        /// </summary>  
        /// <param name="entity"></param>  
        int Add(T entity);

        /// <summary>  
        /// 修改实体  
        /// </summary>  
        /// <param name="entity"></param>  
        void Update(T entity);

        /// <summary>  
        /// 保存或修改实体  
        /// </summary>  
        /// <param name="customer"></param>  
        void SaveOrUpdate(IList<T> list);

        /// <summary>  
        /// 删除实体  
        /// </summary>  
        /// <param name="entity"></param>  
        void Delete(T entity);

        /// <summary>  
        /// 按条件删除  
        /// </summary>  
        /// <param name="sqlWhere">删除条件</param>  
        void Delete(string sqlWhere);

        /// <summary>  
        /// 根据ID得到实体  
        /// </summary>  
        /// <param name="id"></param>  
        /// <returns></returns>  
        T Get(int id);

        /// <summary>  
        /// 根据ID得到实体  
        /// </summary>  
        /// <param name="id"></param>  
        /// <returns></returns>  
        T Load(int id);

        /// <summary>  
        /// 得到所有实体  
        /// </summary>  
        /// <returns></returns>  
        IList<T> LoadAll();

        /// <summary>  
        /// 按条件排序得到前N条记录  
        /// </summary>  
        /// <param name="top">获取条数</param>  
        /// <param name="field">排序字段</param>  
        /// <param order="field">排序方式，升序asc,降序desc</param>  
        /// <returns></returns>  
        IList<T> QueryTop(int top, string field, string order);

        /// <summary>  
        /// 根据条件得到实体  
        /// </summary>  
        /// <param name="sqlWhere">查询条件</param>  
        /// <returns></returns>  
        IList<T> Where(string sqlWhere);

        /// <summary>  
        /// 得到统计数量  
        /// </summary>  
        /// <param name="strWhere">查询条件</param>  
        /// <returns></returns>  
        int GetRecordCount(string strWhere);

        /// <summary>  
        /// 分页获取数据列表  
        /// </summary>  
        /// <param name="PageSize">每页获取数据条数</param>  
        /// <param name="PageIndex">当前页是第几页</param>  
        /// <param name="strWhere">查询条件</param>  
        /// <returns></returns>  
        IList<T> GetPageList(int PageSize, int PageIndex, string strWhere);
    }
}
