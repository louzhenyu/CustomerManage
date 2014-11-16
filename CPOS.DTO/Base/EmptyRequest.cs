/*
 * Author		:Jack.Chen
 * EMail		:jack.chen@jitmarketing.cn
 * Company		:JIT
 * Create On	:2014/3/27 14:54:13
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
    /// 空请求
    /// <remarks>
    /// <para>请求中只包含公共参数,接口参数无数据项的</para>
    /// </remarks>
    /// </summary>
    public class EmptyRequest:APIRequest<EmptyRequestParameter>
    {
        #region 构造函数
        /// <summary>
        /// 构造函数 
        /// </summary>
        public EmptyRequest()
        {
        }
        #endregion
    }
}
