/*
 * Author		:Jack.Chen
 * EMail		:jack.chen@jitmarketing.cn
 * Company		:JIT
 * Create On	:2012/8/14 17:43:09
 * Description	:
 * 1st Modified On	:
 * 1st Modified By	:
 * 1st Modified Desc:
 * 1st Modifier EMail	:
 * 2st Modified On	:
 * 2st Modified By	:
 * 2st Modifier EMail	:
 * 2st Modified Desc:
 */

using System;
using System.Collections.Generic;
using System.Configuration;
using System.Text;


using JIT.Utility.DataAccess;

namespace JIT.CPOS.BS.DataAccess.Base
{
    /// <summary>
    /// 配置信息 
    /// </summary>
    internal static class ConfigInfo
    {
        /// <summary>
        /// 当前的数据库连接字符串管理者
        /// </summary>
        public static readonly DefaultConnectionStringManager CURRENT_CONNECTION_STRING_MANAGER = null;

        /// <summary>
        /// 静态构造函数
        /// </summary>
        static ConfigInfo()
        {
            CURRENT_CONNECTION_STRING_MANAGER = new DefaultConnectionStringManager();
            CURRENT_CONNECTION_STRING_MANAGER.Add(new ConnectionString() { Value = ConfigurationManager.AppSettings["Conn"] }, true);
            //CURRENT_CONNECTION_STRING_MANAGER.Add(new ConnectionString() { Value = "Server=222.73.180.225;Database=CMAP;user id=cmap;password=cmap2012@jit;Trusted_Connection=false;Min Pool Size=5" }, true);
        }
    }
}
