/*
 * Author		:Jack.Chen
 * EMail		:jack.chen@jitmarketing.cn
 * Company		:JIT
 * Create On	:2014/3/27 14:50:53
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

namespace JIT.CPOS.DTO.Base
{
    /// <summary>
    /// 空成功响应 
    /// </summary>
    public class EmptyResponse:APIResponse<EmptyResponseData>
    {
        /// <summary>
        /// 构造函数 
        /// </summary>
        public EmptyResponse()
        {
            this.ResultCode = ERROR_CODES.SUCCESS;
        }
    }
}
