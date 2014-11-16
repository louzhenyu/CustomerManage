using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using JIT.CPOS.DTO.Base;

namespace JIT.CPOS.DTO.Module.VIP.Evaluation.Response
{
    public class GetEvaluationListRD : IAPIResponseData
    {
        /// <summary>
        /// 评论总数
        /// </summary>
        public int Count { get; set; }
        /// <summary>
        /// 评论信息列表
        /// </summary>
        public EvaluationInfo[] EvaluationList { get; set; }
    }

    public class EvaluationInfo
    {
        /// <summary>
        /// 标识
        /// </summary>
        public string EvaluationID { get; set; }
        /// <summary>
        /// 评价内容
        /// </summary>
        public string Content { get; set; }
        /// <summary>
        /// 星级
        /// </summary>
        public string StarLevel { get; set; }
        /// <summary>
        /// 会员ID
        /// </summary>
        public string MemberID { get; set; }
        /// <summary>
        /// 会员名称
        /// </summary>
        public string MemberName { get; set; }
        /// <summary>
        /// 评论时间
        /// </summary>
        public string EvaluationTime { get; set; }

    }
}
