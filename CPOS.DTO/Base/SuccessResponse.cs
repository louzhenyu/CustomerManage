/*
 * Author		:Jack.Chen
 * EMail		:jack.chen@jitmarketing.cn
 * Company		:JIT
 * Create On	:2014/3/28 15:41:48
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
    /// 成功的响应 
    /// </summary>
    public class SuccessResponse<T>:APIResponse<T>
        where T : IAPIResponseData
    {
        #region 构造函数
        /// <summary>
        /// 构造函数 
        /// </summary>
        public SuccessResponse()
        {
            this.ResultCode = ERROR_CODES.SUCCESS;
            this.Message = "OK";
        }
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="pResponseData">响应数据</param>
        public SuccessResponse(T pResponseData)
        {
            this.ResultCode = ERROR_CODES.SUCCESS;
            this.Message = "OK";
            this.Data = pResponseData;
        }
        #endregion
    }
}
