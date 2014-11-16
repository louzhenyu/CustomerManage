/*
 * Author		:Jack.Chen
 * EMail		:jack.chen@jitmarketing.cn
 * Company		:JIT
 * Create On	:2014/3/24 15:52:22
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

using JIT.CPOS.DTO.ValueObject;

namespace JIT.CPOS.DTO.Base
{
    /// <summary>
    /// API调用异常 
    /// </summary>
    public class APIException:Exception
    {
        #region 构造函数
        /// <summary>
        /// 构造函数 
        /// </summary>
        public APIException()
        {
        }
        /// <summary>
        /// 构造函数 
        /// </summary>
        /// <param name="pErrorMessage">错误信息</param>
        public APIException(string pErrorMessage):base(pErrorMessage)
        {
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="pErrorCode">错误码</param>
        /// <param name="pErrorMessage">错误信息</param>
        public APIException(int pErrorCode, string pErrorMessage)
            : base(pErrorMessage)
        {
            this.ErrorCode = pErrorCode;
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="pErrorCode">错误码</param>
        /// <param name="pErrorMessage">错误信息</param>
        /// <param name="pInnerException">内部异常</param>
        public APIException(int pErrorCode, string pErrorMessage, Exception pInnerException)
            :base(pErrorMessage,pInnerException)
        {
            this.ErrorCode = pErrorCode;
        }
        #endregion

        #region 属性集
        /// <summary>
        /// 所属模块
        /// </summary>
        public Modules Module { get; set; }

        /// <summary>
        /// 错误码
        /// </summary>
        public int ErrorCode { get; set; }
        #endregion
    }
}
