/*
 * Author		:Jack.Chen
 * EMail		:jack.chen@jitmarketing.cn
 * Company		:JIT
 * Create On	:2014/3/26 14:57:01
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
    /// API请求参数接口
    /// </summary>
    public interface IAPIRequestParameter
    {
        /// <summary>
        /// 验证API请求参数是否合法
        /// </summary>
        void Validate();
    }
}
