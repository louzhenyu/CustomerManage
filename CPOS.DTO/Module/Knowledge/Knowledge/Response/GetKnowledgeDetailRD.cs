using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using JIT.CPOS.DTO.Base;

namespace JIT.CPOS.DTO.Module.Knowledge.Knowledge.Response
{
    public class GetKnowledgeDetailRD : IAPIResponseData
    {
        public KnowledgeInfo KnowledgeInfo { get; set; }
    }

    public class KnowledgeInfo
    {
        /// <summary>
        /// 文章ID
        /// </summary>
        public Guid? ID { get; set; }
        /// <summary>
        /// 标题
        /// </summary>
        public string Title { get; set; }
        /// <summary>
        /// 描述
        /// </summary>
        public string Description { get; set; }
        /// <summary>
        /// 内容
        /// </summary>
        public string Content { get; set; }
        /// <summary>
        /// 作者
        /// </summary>
        public string Author { get; set; }
        /// <summary>
        /// 图片地址
        /// </summary>
        public string ImageUrl { get; set; }
        /// <summary>
        /// 赞数
        /// </summary>
        public int? PraiseCount { get; set; }
        /// <summary>
        /// 点击数
        /// </summary>
        public int? ClickCount { get; set; }
        /// <summary>
        /// 评论数
        /// </summary>
        public int? EvaluateCount { get; set; }
        /// <summary>
        /// 收藏数
        /// </summary>
        public int? KeepCount { get; set; }
        /// <summary>
        /// 踩数
        /// </summary>
        public int? TreadCount { get; set; }
        /// <summary>
        /// 分享的数量
        /// </summary>
        public int? ShareCount { get; set; }

    }
}
