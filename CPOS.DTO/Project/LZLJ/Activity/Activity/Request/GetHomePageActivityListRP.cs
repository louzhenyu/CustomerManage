/*
 * Author		:Jack.Chen
 * EMail		:jack.chen@jitmarketing.cn
 * Company		:JIT
 * Create On	:2014/4/22 11:29:23
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

namespace JIT.CPOS.DTO.Project.LZLJ.Activity.Activity.Request
{
    /// <summary>
    /// GetHomePageActivityRP 
    /// </summary>
    public class GetHomePageActivityListRP : IAPIRequestParameter
    {
        #region 构造函数
        /// <summary>
        /// 构造函数 
        /// </summary>
        public GetHomePageActivityListRP()
        {
        }
        #endregion

        public int? PageIndex { get; set; }
        public int? PageSize { get; set; }

        #region IAPIRequestParameter 成员

        public void Validate()
        {
        }

        #endregion
    }
}
