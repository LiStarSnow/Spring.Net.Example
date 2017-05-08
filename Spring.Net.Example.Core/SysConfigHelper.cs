using System;
using System.Collections.Generic;
using Spring.Net.Example.Model.Dto.Config;

namespace Spring.Net.Example.Core
{
    /// <summary>
    /// 系统配置帮助
    /// </summary>
    public sealed class SysConfigHelper
    {
        private static SysConfigHelper _instance = null;
        private static readonly object _obj = new object();

        /// <summary>
        /// 单例,通过它访问系统配置
        /// </summary>
        public static SysConfigHelper Instance
        {
            get
            {
                if (_instance == null)
                {
                    lock (_obj)
                    {
                        if (_instance == null)
                        {
                            _instance = new SysConfigHelper();
                        }
                    }
                }
                return _instance;
            }
        }

        /// <summary>
        /// 系统核心配置信息
        /// </summary>
        private static Dictionary<string, ParamResult> ParamConfigs = new Dictionary<string, ParamResult>();

        /// <summary>
        /// 更新核心配置信息
        /// </summary>
        /// <param name="configs"></param>
        public static void RefreshSysConfig(Dictionary<string, ParamResult> configs)
        {
            ParamConfigs = configs;
        }
        /// <summary>
        /// 获取配置值
        /// </summary>
        /// <param name="key">配置Key</param>
        /// <returns></returns>
        public string GetValue(string key)
        {
            lock (ParamConfigs)
            {
                return ParamConfigs[key].Value;
            }
        }

        public int GetInt(string key)
        {
            lock (ParamConfigs)
            {
                return int.Parse(ParamConfigs[key].Value);
            }
        }

        public bool GetBool(string key)
        {
            lock (ParamConfigs)
            {
                return bool.Parse(ParamConfigs[key].Value);
            }
        }

        public decimal GetDecimal(string key)
        {
            lock (ParamConfigs)
            {
                return decimal.Parse(ParamConfigs[key].Value);
            }
        }

        public double GetDouble(string key)
        {
            lock (ParamConfigs)
            {
                return Convert.ToDouble(ParamConfigs[key].Value);
            }
        }

        public DateTime GetDate(string key)
        {
            lock (ParamConfigs)
            {
                return DateTime.Parse(ParamConfigs[key].Value);
            }
        }

        /// <summary>
        /// 更新客户端配置
        /// </summary>
        /// <param name="clientConfig"></param>
        public void ResetClientConfig(Dictionary<string, object> clientConfig)
        {
            foreach (var key in ParamConfigs)
            {
                if (key.Value.IsClientUse == "1")
                {
                    switch (key.Value.ValueType)
                    {
                        case "1":
                            clientConfig.Add(key.Key, key.Value.Value);
                            break;
                        case "2":
                            clientConfig.Add(key.Key, int.Parse(key.Value.Value));
                            break;
                        case "3":
                            clientConfig.Add(key.Key, key.Value.Value == "true");
                            break;
                        case "4":
                            clientConfig.Add(key.Key, double.Parse(key.Value.Value));
                            break;
                    }
                }
            }
        }
    }
}
