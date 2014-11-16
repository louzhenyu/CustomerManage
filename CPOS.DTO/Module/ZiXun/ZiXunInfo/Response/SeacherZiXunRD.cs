using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using JIT.CPOS.DTO.Base;

namespace JIT.CPOS.DTO.Module.ZiXun.ZiXunInfo.Response
{
    public class SeacherZiXunRD : IAPIResponseData
    {
        /// <summary>
        /// 分类咨询列表
        /// </summary>
        public IList<LNewsListByTypeEntity> LNewsListByTypeList { get; set; }
    }

    public class LNewsListByTypeEntity
    {
        /// <summary>
        /// 咨询分类
        /// </summary>
        public string LNewsType { get; set; }
        /// <summary>
        /// 咨询列表
        /// </summary>
        public IList<LNewsEntity> LNewsList { get; set; }
    }

    public class LNewsEntity
    {
        /// <summary>
        /// 咨询标题
        /// </summary>
        public string NewsTitle { get; set; }
        /// <summary>
        /// 咨询内容
        /// </summary>
        public string Content { get; set; }
        /// <summary>
        /// 咨询分类名称
        /// </summary>
        public string NewsTypeName { get; set; }
    }
}
