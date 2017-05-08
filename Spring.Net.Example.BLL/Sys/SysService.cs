
namespace Spring.Net.Example.BLL.Sys
{
    using System;
    using System.Collections.Generic;
    using Model.Dto.Config;
    using System.Linq;
    using BaseDto;
    using Log;
    using IDao.Sys;

    /// <summary>
    /// 系统配置服务
    /// </summary>
    public class SysService
    {
        /// <summary>
        /// 功能权限配置
        /// </summary>
        public IFunctionConfigDao FunctionConfigDao { get; set; }

        /// <summary>
        /// 获取地区系统配置信息
        /// </summary>
        /// <returns></returns>
        public Response<List<ParamResult>> GetFunctionParams(string appKey = null)
        {
            var result = new Response<List<ParamResult>>();
            try
            {
                var res = FunctionConfigDao.GetParamConfig();

                result.Result = (from func in res
                                 select new ParamResult
                                 {
                                     Id = func.ID.ToString(),
                                     GroupName = func.GROUP_NAME,
                                     Name = func.NAME,
                                     Remark = func.REMARK,
                                     Value = func.VALUE,
                                     Key = func.KEY,
                                     IsEdit = func.IS_EDIT,
                                     Type = func.CONTROL_TYPE,
                                     Verify = func.VERIFY,
                                     IsClientUse = func.IS_CLIENT_USE,
                                     Page = func.GROUP_SORT,
                                     ValueType = func.VALUE_TYPE
                                 }).ToList();

            }
            //catch (DataAccessExcepton ex)
            //{
            //    result.ErrMsg = ex.Message;
            //}
            catch (Exception ex)
            {
                result.ErrMsg = "获取系统配置信息失败！";
                Logger.LogException(ex);
            }
            return result;

        }

        /// <summary>
        /// 防止多线程下配置重复
        /// </summary>
        private readonly static object locker = new object();

        /// <summary>
        /// 保存系统配置
        /// </summary>
        /// <param name="operId">操作人</param>
        /// <param name="paramConfigs">配置项</param>
        /// <returns></returns>
        public Response<bool> SaveFunctionParams(string operId, List<ParamResult> paramConfigs, string appKey = null)
        {
            var result = new Response<bool>();
            try
            {
                appKey = string.IsNullOrEmpty(appKey) ? Global.CurrentApp.AppKey[0] : appKey;
                lock (locker)
                {
                    //var inserts = paramConfigs.Where(x => functionConfigDao.ConfigIsEdit(x.Key, appKey)).Select(x => new Model.Table.ConfigAreaFunc
                    //{
                    //    Key = x.Key,
                    //    Value = x.Value,
                    //    AppKey = appKey
                    //}).ToList();
                    //using (var tran = new DbLight.TransactionScope(functionConfigDao.QuerySession))
                    //{
                    //    functionConfigDao.BatchDelete(inserts, " key=:0 and sys_app_key=:1", x => x.Key, x => x.AppKey);
                    //    functionConfigDao.BatchInsert(inserts, t => t.Key, t => t.Value, t => t.AppKey);
                    //    tran.Complete();
                    //}
                }
                //更新系统配置信息
                RefreshConfigValue();
            }
            catch (Exception ex)
            {
                result.ErrMsg = "保存系统配置信息失败！";
                Logger.LogException(ex);
            }
            return result;

        }

        /// <summary>
        /// 更新系统核心配置信息
        /// </summary>
        public void RefreshConfigValue()
        {
            var dic = new Dictionary<string, ParamResult>();
            var res = FunctionConfigDao.GetAllParamConfig().ToList();
            var paramResultList = (from func in res
                                   select new ParamResult
                                   {
                                       Id = func.ID.ToString(),
                                       GroupName = func.GROUP_NAME,
                                       Name = func.NAME,
                                       Remark = func.REMARK,
                                       Value = func.VALUE,
                                       Key = func.KEY,
                                       IsEdit = func.IS_EDIT,
                                       Type = func.CONTROL_TYPE,
                                       Verify = func.VERIFY,
                                       IsClientUse = func.IS_CLIENT_USE,
                                       Page = func.GROUP_SORT,
                                       ValueType = func.VALUE_TYPE
                                   }).ToList();

            paramResultList.ForEach(t =>
              {
                  dic.Add(t.Key, t);
              });
            Core.SysConfigHelper.RefreshSysConfig(dic);
        }
    }
}
