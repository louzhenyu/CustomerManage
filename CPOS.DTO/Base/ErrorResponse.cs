/*
 * Author		:Jack.Chen
 * EMail		:jack.chen@jitmarketing.cn
 * Company		:JIT
 * Create On	:2014/3/27 14:48:09
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
    /// 接口处理失败的响应 
    /// </summary>
    public class ErrorResponse:APIResponse<EmptyResponseData>
    {
        /// <summary>
        /// 构造函数 
        /// </summary>
        public ErrorResponse() { base.ResultCode = ERROR_CODES.DEFAULT_ERROR; }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="pErrorCode">错误码</param>
        /// <param name="pErrorMessage">错误信息</param>
        public ErrorResponse(int pErrorCode, string pErrorMessage) 
        {
            base.ResultCode = pErrorCode;
            base.Message = pErrorMessage;
        }
    }
}
