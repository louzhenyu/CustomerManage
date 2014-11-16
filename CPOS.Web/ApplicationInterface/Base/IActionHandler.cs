/*
 * Author		:Jack.Chen
 * EMail		:jack.chen@jitmarketing.cn
 * Company		:JIT
 * Create On	:2014/3/27 14:18:24
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
using JIT.CPOS.DTO.ValueObject;

namespace JIT.CPOS.Web.ApplicationInterface.Base
{
    /// <summary>
    /// 接口请求处理器 
    /// </summary>
    public interface IActionHandler
    {
        /// <summary>
        /// 处理接口请求
        /// </summary>
        /// <param name="pRequestJSON">请求JSON</param>
        /// <returns>返回响应结果</returns>
        IAPIResponseData ProcessAction(string pRequestJSON);
    }
}
