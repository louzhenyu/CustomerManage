/*
 * Author		:Jack.Chen
 * EMail		:jack.chen@jitmarketing.cn
 * Company		:JIT
 * Create On	:2014/4/17 15:28:28
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

namespace JIT.CPOS.DTO.Project.LZLJ.Activity.Activity.Response
{
    /// <summary>
    /// GetHomePageActivityListRD 
    /// </summary>
    public class GetHomePageActivityListRD : IAPIResponseData
    {
        #region 构造函数
        /// <summary>
        /// 构造函数 
        /// </summary>
        public GetHomePageActivityListRD()
        {
        }
        #endregion

        public List<ActivityListItemInfo> Items { get; set; }

    }

    public class ActivityListItemInfo
    {
        /// <summary>
        /// 活动标题
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// 图片连接
        /// </summary>
        public string ImageUrl { get; set; }

        /// <summary>
        /// 跳转连接
        /// </summary>
        public string LinkUrl { get; set; }
    }
}
