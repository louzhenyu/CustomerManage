/*
 * Author		:Jack.Chen
 * EMail		:jack.chen@jitmarketing.cn
 * Company		:JIT
 * Create On	:2014/3/27 14:53:18
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
    /// 空请求参数 
    /// </summary>
    public class EmptyRequestParameter:IAPIRequestParameter
    {
        #region 构造函数
        /// <summary>
        /// 构造函数 
        /// </summary>
        public EmptyRequestParameter()
        {
        }
        #endregion

        #region IAPIRequestParameter 成员
        /// <summary>
        /// 执行验证
        /// </summary>
        public virtual void Validate()
        {
            //do nothing
        }
        #endregion
    }

    public class PageQueryRequestParameter : EmptyRequestParameter
    {
        /// <summary>
        /// 页大小
        /// </summary>
        public int PageSize { get; set; }
        /// <summary>
        /// 页索引
        /// </summary>
        public int PageIndex { get; set; }
    }
}
