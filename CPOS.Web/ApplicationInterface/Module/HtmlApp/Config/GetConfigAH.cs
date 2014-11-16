/*
 * Author		:Jack.Chen
 * EMail		:jack.chen@jitmarketing.cn
 * Company		:JIT
 * Create On	:2014/4/18 17:19:26
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
using System.Linq;
using System.IO;
using System.Text;

using JIT.CPOS.Web.ApplicationInterface.Base;
using JIT.CPOS.DTO.Module.HtmlApp.Config.Response;
using JIT.CPOS.BS.BLL;
using JIT.CPOS.BS.Entity;
using JIT.CPOS.DTO.Base;

namespace JIT.CPOS.Web.ApplicationInterface.Module.HtmlApp.Config
{
    /// <summary>
    /// GetConfigAH 
    /// </summary>
    public class GetConfigAH : BaseActionHandler<EmptyRequestParameter, GetConfigRD>
    {
        #region 构造函数
        /// <summary>
        /// 构造函数 
        /// </summary>
        public GetConfigAH()
        {
        }
        #endregion

        /// <summary>
        /// 配置文件未找到
        /// </summary>
        const int ERROR_CODE_CONFIG_FILE_NO_FOUND = 301;

        protected override GetConfigRD ProcessRequest(APIRequest<EmptyRequestParameter> pRequest)
        {
            var configFilePath = AppDomain.CurrentDomain.BaseDirectory + string.Format(@"\HtmlApps\config\{0}.js", this.CurrentUserInfo.ClientID);
            if (File.Exists(configFilePath))
            {
                GetConfigRD rd = new GetConfigRD();
                rd.ConfigContent = File.ReadAllText(configFilePath);
                return rd;
            }
            else
            {
                throw new APIException("配置文件未找到") { ErrorCode = ERROR_CODE_CONFIG_FILE_NO_FOUND };
            }
        }
    }
}
