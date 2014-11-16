/*
 * Author		:Jack.Chen
 * EMail		:jack.chen@jitmarketing.cn
 * Company		:JIT
 * Create On	:2014/4/18 17:18:37
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
using System.Text;
using JIT.CPOS.DTO.Base;

namespace JIT.CPOS.DTO.Module.HtmlApp.Config.Response
{
    /// <summary>
    /// GetConfigRD 
    /// </summary>
    public class GetConfigRD : IAPIResponseData
    {
        #region 构造函数
        /// <summary>
        /// 构造函数 
        /// </summary>
        public GetConfigRD()
        {
        }
        #endregion

        /// <summary>
        /// 配置文件内容
        /// </summary>
        public string ConfigContent { get; set; }
    }
}
