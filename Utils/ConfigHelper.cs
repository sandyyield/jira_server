using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Jira_server.Utils
{
    public class ConfigHelper
    {
        /// <summary>
        /// The configuration
        /// </summary>
        private static IConfigurationRoot _configuration;

        /// <summary>
        /// Initializes a new instance of the <see cref="ConfigHelper"/> class.
        /// </summary>
        public ConfigHelper()
        {
            if (_configuration == null)
            {
                IConfigurationBuilder builder = new ConfigurationBuilder().AddJsonFile("appsettings.json");
                _configuration = builder.Build();
            }
        }

        /// <summary>
        /// Gets the configuration.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <returns></returns>
        public string GetConfig(string key)
        {
            return _configuration[key];
        }

        /// <summary>
        ///获取字符串配置
        /// </summary>
        /// <param name="key">The key.</param>
        /// <returns></returns>
        public static string GetConfigToString(string key)
        {
            try
            {
                return new ConfigHelper().GetConfig(key);
            }
            catch
            {
                return "";
            }
        }
        /// <summary>
        /// 查询配置，返回整数
        /// </summary>
        /// <param name="key">键名</param>
        /// <returns></returns>
        public static int GetConfigToInt(string key)
        {
            try
            {
                string configValue = new ConfigHelper().GetConfig(key);
                if (configValue == null)
                {
                    return int.MinValue;
                }
                return Convert.ToInt32(configValue);
            }
            catch
            {
                return int.MinValue;
            }
        }
        /// <summary>
        /// 查询配置，返回整数
        /// </summary>
        /// <param name="key">键名</param>
        /// <returns></returns>
        public static bool GetConfigToBool(string key)
        {
            try
            {
                string configValue = new ConfigHelper().GetConfig(key);
                if (configValue == null)
                {
                    return false;
                }
                return Convert.ToBoolean(configValue);
            }
            catch
            {
                return false;
            }
        }
    }
}
