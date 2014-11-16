/*
 * Author		:Jack.Chen
 * EMail		:jack.chen@jitmarketing.cn
 * Company		:JIT
 * Create On	:2014/3/26 14:59:08
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
    /// API响应 
    /// </summary>
    public class APIResponse<T>
        where T:IAPIResponseData
    {
        #region 构造函数
        /// <summary>
        /// 构造函数 
        /// </summary>
        public APIResponse()
        {
        }
        /// <summary>
        /// 构造函数 
        /// </summary>
        /// <param name="pResponseData">响应数据对象</param>
        public APIResponse(T pResponseData)
        {
            this.Data = pResponseData;
        }
        #endregion

        #region 响应结果
        /// <summary>
        /// 结果码
        /// </summary>
        public int ResultCode { get; set; }

        /// <summary>
        /// 描述信息
        /// </summary>
        public string Message { get; set; }

        /// <summary>
        /// 是否成功
        /// </summary>
        public bool IsSuccess
        {
            get { return this.ResultCode < 100; }
        }
        #endregion

        #region 响应数据
        /// <summary>
        /// 响应数据
        /// </summary>
        public T Data { get; set; }
        #endregion
    }
}
